using GameObjectsConfigs;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private GameManagerConfig gameManagerConfig = default;
        private PoolManager _currentPoolManager = default;
        private LevelManager _currentLevelManager = default;
        private ScoreManager _scoreManager = default;
        private UIManager _uiManager = default;

        [Inject]
        public void Setup(PoolManager poolManager, IScoreManager scoreManager,LevelManager levelManager,UIManager uiManager)
        {
            _currentPoolManager = poolManager;
            _scoreManager = (ScoreManager)scoreManager;
            _currentLevelManager = levelManager;
            _uiManager = uiManager;
        }

        private void Start()
        {
            _currentPoolManager.PoolConfig = gameManagerConfig.PoolConfig;
            _currentPoolManager.InitGamePools();
            _currentLevelManager.Init();
            _currentLevelManager.OnNextLevel += GoOnNextLevel;
            _currentLevelManager.OnGameOverEvent += OnGameOver;
            _uiManager.SetPageState(UIManager.PageState.StartGame);
            _uiManager.FireButton.onClick.AddListener(ConfirmStartGame);
            _uiManager.ReplayButton.onClick.AddListener(ConfirmStartGame);
        }
        
        private void ConfirmStartGame()
        {
            _uiManager.SetPageState(UIManager.PageState.None);
            _currentLevelManager.SetBarrels(true);
            _currentLevelManager.InitEnemySpawn();
        }

        private void GoOnNextLevel(bool state)
        {
            _uiManager.SetPageState(state ? UIManager.PageState.StartGame : UIManager.PageState.GameOver);
        }

        private void OnGameOver(bool st = false)
        {
            _scoreManager.InitScore();
        }
    }
}

