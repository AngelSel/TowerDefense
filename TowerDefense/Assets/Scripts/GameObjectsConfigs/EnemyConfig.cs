using UnityEngine;

namespace GameObjectsConfigs
{
    [CreateAssetMenu(menuName = "Configs/Enemy Config",fileName = "EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private int health = default;
        [SerializeField] private float jumpHeight = default;
        [SerializeField] private int jumpAmount = default;
        [SerializeField] private float duration = default;

        public int Health => health;
        public float JumpHeight => jumpHeight;
        public int JumpAmount => jumpAmount;
        public float Duration => duration;
    }
}
