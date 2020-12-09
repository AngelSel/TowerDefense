using DG.Tweening;
using GameObjectsConfigs;
using Pool;
using UnityEngine;
using UnityEngine.UI;

namespace Objects
{
    public class EnemyObject : PoolItem
    {
        [SerializeField] internal EnemyConfig _enemyConfig = default;
        [SerializeField] private Slider _healthBar = default;

        private int currentHealth = default;
        private ScoreManager _scoreManager = default;
        private ParticleObjects _currentParticles = default;
        
        internal void Init(ScoreManager scoreManager,ParticleObjects particleObjects)
        {
            _scoreManager = scoreManager;
            _currentParticles = particleObjects;
        }

        internal void Recreate(Vector3 pos)
        {
            currentHealth = _enemyConfig.Health;
            pos.z = pos.z - 12f; 
            transform.DOJump(pos, 0.2f, 20, 25f).OnComplete(ReturnToPool);
            _healthBar.value = (float)currentHealth / _enemyConfig.Health;
            _currentParticles.StopParticle();
        }

        internal void GetDamage(int damage)
        {
            _scoreManager.AddScore(damage);
            currentHealth -= damage;
            _healthBar.value = (float)currentHealth / _enemyConfig.Health;
            
            if (currentHealth < 1)
            {
                _currentParticles.transform.position = transform.position;
                _currentParticles.PlayParticle();

                ReturnToPool();
                
                DOTween.Kill(transform);
                currentHealth = _enemyConfig.Health;
            }
            _scoreManager.PrintScore();
        }
        
        private void  OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("GameOverLine"))
            {
                ReturnToPool();
                DOTween.Kill(transform);
            }
        }
    }
}
