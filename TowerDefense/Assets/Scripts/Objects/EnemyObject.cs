using DG.Tweening;
using GameObjectsConfigs;
using Managers;
using Pool;
using UnityEngine;
using UnityEngine.UI;

namespace Objects
{
    public class EnemyObject : PoolItem
    {
        [SerializeField] protected EnemyConfig _enemyConfig = default;
        [SerializeField] protected Slider _healthBar = default;
        [SerializeField] protected TrailRenderer _trail = default;

        protected int currentHealth = default;
        protected ScoreManager scoreManager = default;
        protected ParticleObjects currentParticles = default;
        
        internal void Init(ScoreManager scoreManager,ParticleObjects particleObjects)
        {
            this.scoreManager = scoreManager;
            currentParticles = particleObjects;
        }

        internal void Recreate(Vector3 pos, bool isInner)
        {
            DOTween.Kill(transform);
            _trail.Clear();
            currentHealth = _enemyConfig.Health;
            if (isInner)
            {
                pos.x += Random.Range(-4f, 4f);
                pos.z += Random.Range(-4f, -2f);
                transform.DOMove(pos, 7f).OnComplete(ReturnToPool);
            }
            else
            {
                pos.z -= 12f; 
                transform.DOJump(pos, _enemyConfig.JumpHeight, _enemyConfig.JumpAmount, _enemyConfig.Duration).OnComplete(ReturnToPool);
            }
            _healthBar.value = (float)currentHealth / _enemyConfig.Health;
            currentParticles.StopParticle();
        }

        internal virtual void GetDamage(int damage)
        {
            scoreManager.AddScore(damage);
            currentHealth -= damage;
            _healthBar.value = (float)currentHealth / _enemyConfig.Health;
            
            if (currentHealth < 1)
            {
                currentParticles.transform.position = transform.position;
                currentParticles.PlayParticle();

                ReturnToPool();
                
                DOTween.Kill(transform);
                currentHealth = _enemyConfig.Health;
            }
        }
        
        protected void  OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("GameOverLine"))
            {
                ReturnToPool();
                DOTween.Kill(transform);
            }
        }
    }
}
