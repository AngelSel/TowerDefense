using System.Collections.Generic;
using Pool;
using UnityEngine;

namespace GameObjectsConfigs
{
    [CreateAssetMenu(menuName = "Configs/Pool Config",fileName = "PoolConfig")]
    public class PoolConfig : ScriptableObject
    {
        [SerializeField] private List<PoolData> _gamePoolsData = default;

        public List<PoolData> GamePoolsData => _gamePoolsData;
    }
}
