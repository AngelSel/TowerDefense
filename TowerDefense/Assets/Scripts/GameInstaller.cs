using Managers;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    [SerializeField] private PoolManager _poolManager = default;
    [SerializeField] private LevelManager _levelManager = default;
    [SerializeField] private UIManager _uiManager = default;

   public override void InstallBindings()
    {
        Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IScoreManager>().To<ScoreManager>().AsSingle();
        
        Container.BindInstance(_uiManager).AsSingle();
        Container.QueueForInject(_uiManager);
        
        Container.BindInstance(_poolManager).AsSingle();
        Container.QueueForInject(_poolManager);

        Container.BindInstance(_levelManager).AsSingle();
        Container.QueueForInject(_levelManager);

    }
}
