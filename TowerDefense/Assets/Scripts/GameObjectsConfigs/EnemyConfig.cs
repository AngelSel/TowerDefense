using UnityEngine;

namespace GameObjectsConfigs
{
    [CreateAssetMenu(menuName = "Configs/Enemy Config",fileName = "EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        //[SerializeField] private GameObject enemyPrefab = default;
        [SerializeField] private int speed = default;
        [SerializeField] private int health = default;

        //public GameObject EnemyPrefab => enemyPrefab;
        public int Speed => speed;

        public int Health => health;
    }
}
