using GameObjectsConfigs;
using Pool;
using UnityEngine;

namespace Objects
{
    public class BulletObject : PoolItem
    {
        private ParticleObjects _explosionPrefab = default;
        [SerializeField] private BulletConfig _bulletConfig = default;
        public BulletConfig BulletConfig => _bulletConfig;

        internal void Init(ParticleObjects particle)
        {
            _explosionPrefab = particle;
            _explosionPrefab.StopParticle();
        }

        internal void Print()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _bulletConfig.Radius);
            foreach (var collider in colliders)
            {
                if (collider.tag.Equals("Enemy"))
                {
                    var item = collider.gameObject.GetComponent<EnemyObject>();
                    item.GetDamage(_bulletConfig.Damage);
                }
                else if (collider.tag.Equals("CompositeEnemy"))
                {
                    var item = collider.gameObject.GetComponent<CompositeEnemy>();
                    item.GetDamage(_bulletConfig.Damage);
                }
            }

            _explosionPrefab.transform.position = transform.position;
            _explosionPrefab.PlayParticle();
            
            ReturnToPool();
        }
    }
}
