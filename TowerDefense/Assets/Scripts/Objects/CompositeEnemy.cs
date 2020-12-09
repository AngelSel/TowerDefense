using System.Collections.Generic;
using DG.Tweening;
using GameObjectsConfigs;
using Pool;
using UnityEngine;
using UnityEngine.UI;

namespace Objects
{
    public class CompositeEnemy : PoolItem
    {
        [SerializeField] internal EnemyConfig _enemyConfig = default;
        [SerializeField] private Slider _healthBar = default;

        private int _currentHealth = default;
        private ScoreManager _scoreManager = default;
        private ParticleObjects _currentParticles = default;
        
        private List<EnemyObject> _compositeEnemies = default;


        internal void Init(List<EnemyObject> enemyObjects,ScoreManager scoreManager,ParticleObjects particleObjects)
        {
            _compositeEnemies = enemyObjects;
            foreach (var enemy in _compositeEnemies) enemy.gameObject.SetActive(false);
            _scoreManager = scoreManager;
            _currentParticles = particleObjects;
        }
        
        internal void Recreate(Vector3 pos)
        {
            _currentHealth = _enemyConfig.Health;
            pos.z = pos.z - 12f; 
            transform.DOJump(pos, 0.2f, 20, 25f).OnComplete(ReturnToPool);
            _healthBar.value = (float)_currentHealth / _enemyConfig.Health;
            _currentParticles.StopParticle();
        }
        
        internal void GetDamage(int damage)
        {
            _scoreManager.AddScore(damage);
            _currentHealth -= damage;
            _healthBar.value = (float)_currentHealth / _enemyConfig.Health;
            
            if (_currentHealth < 1)
            {
                _currentParticles.transform.position = transform.position;
                _currentParticles.PlayParticle();
                Vector3 pos = transform.position;
                pos.z = pos.z - 5f;
                pos.y = 0.1f;
                int num = 10;
                foreach (var enemy in _compositeEnemies)
                {
                    enemy.gameObject.SetActive(true);
                    enemy.transform.position = transform.position;
                    pos.x = pos.x - 1f;

                    num = num + 3;
                    //enemy.transform.DOJump(pos, 0.2f, num, 25f).OnComplete(ReturnToPool);
                    enemy.Recreate(pos);
                    
                }
                
                ReturnToPool();
                
                DOTween.Kill(transform);
                _currentHealth = _enemyConfig.Health;
            }
            _scoreManager.PrintScore();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("GameOverLine"))
            {
                ReturnToPool();
                DOTween.Kill(transform);
            }
        }
    }
}
