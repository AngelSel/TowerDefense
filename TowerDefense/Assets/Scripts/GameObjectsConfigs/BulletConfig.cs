using UnityEngine;

namespace GameObjectsConfigs
{
    [CreateAssetMenu(menuName = "Configs/Bullet Config",fileName = "BulletConfig")]
    public class BulletConfig : ScriptableObject
    {
        //[SerializeField] private GameObject bulletPrefab = default;
        [SerializeField] private float _speed = default;
        [SerializeField] private int _damage = default;
        [SerializeField] private float _radius = default;


        //public GameObject BulletPrefab => bulletPrefab;
        public float Speed => _speed;
        public int Damage => _damage;
        public float Radius => _radius;
    }
}
