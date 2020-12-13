using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjectsConfigs
{
   [Serializable]
    public struct WaveStruct
    {
        public int _amountOfSmallEnemies;
        public float smallSpawnTime;
        public int _amountOfMiddleEnemies;
        public float middleSpawnTime;
        public int _amountOfBigEnemies;
        public float _bigSpawnTime;
        public float _nextWaveTime;
    }
    
    [CreateAssetMenu(menuName = nameof(GameObjectsConfigs) + "/" + nameof(SpawnWaveConfig),fileName = nameof(SpawnWaveConfig))]
    public class SpawnWaveConfig : ScriptableObject
    {
        
        [SerializeField] private List<WaveStruct> m_wavesConfig = default;
        public List<WaveStruct> MWavesConfig => m_wavesConfig;
    }
}
