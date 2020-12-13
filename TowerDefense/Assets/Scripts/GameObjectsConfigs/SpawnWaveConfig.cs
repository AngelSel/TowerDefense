using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjectsConfigs
{
   [Serializable]
    public struct WaveStruct
    {
        public int _amountOfSmallEnemies;
        public int _amountOfMiddleEnemies;
        public int _amountOfBigEnemies;
        public float _spawnTime;
        public float _nextWaveTime;
    }
    
    [CreateAssetMenu(menuName = nameof(GameObjectsConfigs) + "/" + nameof(SpawnWaveConfig),fileName = nameof(SpawnWaveConfig))]
    public class SpawnWaveConfig : ScriptableObject
    {
        
        [SerializeField] private List<WaveStruct> m_wavesConfig = default;
        public List<WaveStruct> MWavesConfig => m_wavesConfig;
    }
}
