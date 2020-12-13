using GameObjectsConfigs;
using UnityEngine;

namespace Objects
{
    public class LevelPrefab : MonoBehaviour
    {
        [SerializeField] private GunObject[] _guns = default;
        [SerializeField] private EnemySpawn _enemySpawn = default;
        [SerializeField] private SpawnWaveConfig _spawnWaveConfig = default;
        [SerializeField] private LevelEndLine _levelEnd = default;
    
        public GunObject[] Guns  => _guns;
        public EnemySpawn EnemySpawn => _enemySpawn;
        public SpawnWaveConfig SpawnWaveConfig => _spawnWaveConfig;
        public LevelEndLine LevelEnd => _levelEnd;
    }
}
