using System;
using GameObjectsConfigs;
using Pool;
using UnityEngine;

namespace Objects
{
    public class BulletObject : PoolItem
    {
        private ParticleObjects _explosionPrefab = default;
        
        [SerializeField] private BulletConfig _bulletConfig = default;
        [SerializeField] private Rigidbody _rbBullet = default;

        internal void Init(ParticleObjects particle, Vector3 aimTarget)
        {
            _explosionPrefab = particle;
            _explosionPrefab.StopParticle();
        }
        
        private void FixedUpdate()
        {
            _rbBullet.velocity = transform.TransformDirection(new Vector3(0, 0, _bulletConfig.Speed));
        }

        internal void CollisionWithEnemy()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _bulletConfig.Radius);
            foreach (var collider in colliders)
            {
                if (collider.tag.Equals("Enemy"))
                {
                    var item = collider.gameObject.GetComponent<EnemyObject>();
                    item.GetDamage(_bulletConfig.Damage);

                    if (!_bulletConfig.name.Equals("Bull3"))
                    {
                        _explosionPrefab.transform.position = transform.position;
                        _explosionPrefab.PlayParticle();
                        ReturnToPool();
                    }

                }
                else if (collider.tag.Equals("CompositeEnemy"))
                {
                    var item = collider.gameObject.GetComponent<CompositeEnemy>();
                    item.GetDamage(_bulletConfig.Damage);
                    
                    if (!_bulletConfig.name.Equals("Bull3"))
                    {
                        _explosionPrefab.transform.position = transform.position;
                        _explosionPrefab.PlayParticle();
                        ReturnToPool();
                    }
                }
                else if (collider.tag.Equals("Platform"))
                {
                    if (!_bulletConfig.name.Equals("Bull3"))
                    {
                        _explosionPrefab.transform.position = transform.position;
                        _explosionPrefab.PlayParticle();
                        ReturnToPool();
                    }
                }
            }
        }
        
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.tag.Equals("Platform"))
            {
                CollisionWithEnemy();
            }
            else if (collider.tag.Equals("Enemy"))
            {
                CollisionWithEnemy();

            }
        }

        private void OnBecameInvisible()
        {
            ReturnToPool();
        }
    }
}
