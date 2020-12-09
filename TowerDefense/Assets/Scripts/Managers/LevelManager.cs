using System;
using DG.Tweening;
using Pool;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    public event Action<bool> OnNextLevel = null; 
    public event Action<bool> OnGameOverEvent = null; 
    
    [SerializeField] private LevelPrefab _level = default;
    
    private Camera _mainCamera = default;
    private PoolManager _poolManager = default;
    
    private LevelPrefab _currentLevel = null;
    private LevelPrefab _previousLevel = null;
    private Vector3 startPosition = Vector3.zero;
    private int counter = default;
    public PoolManager PoolManager => _poolManager;
    private ScoreManager _scoreManager = default;

    [Inject]
    public void Setup(PoolManager poolManager, Camera camera,IScoreManager scoreManager)
    {
        _poolManager = poolManager;
        _mainCamera = camera;
        _scoreManager = (ScoreManager)scoreManager;
    }

    public void Init()
    {
        startPosition = Vector3.zero;
        CreateLevel();
        counter = 0;
    }

    private void CreateLevel()
    {
        _currentLevel = Instantiate(_level,startPosition,Quaternion.identity);

        foreach (var gun in _currentLevel.Guns)
        {
            gun.OnFiring += GiveItem;
        }
    }

    internal void SetBarrels(bool state)
    {
        foreach (var gun in _currentLevel.Guns)
        {
            gun.IsActive = state;  
        }
    }

    internal void InitEnemySpawn()
    {
        _currentLevel.EnemySpawn.InitSpawner(_level.SpawnWaveConfig.MWavesConfig[counter]);
        _currentLevel.EnemySpawn.OnGetItemFromPool += GiveItem;
        _currentLevel.EnemySpawn.OnSpawnEnd += OnWaveOver;
        counter++;
        if (counter >= _level.SpawnWaveConfig.MWavesConfig.Count)
        {
            counter = 0;
        }
        _currentLevel.EnemySpawn.InitScore(_scoreManager);
        _currentLevel.EnemySpawn.Starting();
        _currentLevel.LevelEnd.OnGameOver += OnGameOver;

    }
    
    private PoolItem GiveItem(string tag)
    {
        return _poolManager.GetItemFromPool(tag);
    }

    private void GoToNextLevelWin()
    {
        Destroy(_previousLevel.gameObject);
        OnNextLevel.Invoke(true);
    }
    
    private void GoToNextLevelLoose()
    {
        counter = 0;
        Destroy(_previousLevel.gameObject);
        OnNextLevel.Invoke(false);
    }

    
    private void OnWaveOver(bool state)
    {
        _level.Aim.SetState(false);
        _previousLevel = _currentLevel;
        foreach (var gun in _currentLevel.Guns)
        {
            gun.OnFiring -= GiveItem;
            gun.IsActive = false;
        }
        startPosition.z = startPosition.z + 16f;
        CreateLevel();
        if(state)
            _mainCamera.transform.DOMove(
                new Vector3(_mainCamera.transform.position.x, _mainCamera.transform.position.y,
                    _mainCamera.transform.position.z + 16f), 2f).OnComplete(GoToNextLevelWin);
        else
            _mainCamera.transform.DOMove(
                new Vector3(_mainCamera.transform.position.x, _mainCamera.transform.position.y,
                    _mainCamera.transform.position.z + 16f), 2f).OnComplete(GoToNextLevelLoose);

    }

    private void OnGameOver(bool b=false)
    {
        OnGameOverEvent.Invoke(false);
        OnWaveOver(false);
    }

}
