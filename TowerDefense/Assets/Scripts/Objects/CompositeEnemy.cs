using System.Collections.Generic;
using DG.Tweening;
using Managers;

namespace Objects
{
    public class CompositeEnemy : EnemyObject
    {
        private List<EnemyObject> _compositeEnemies = default;
        private List<ParticleObjects> _compositeParticles = default;

        internal void Init(List<EnemyObject> enemyObjects, List<ParticleObjects> compositeParticles, ScoreManager scoreManager, ParticleObjects particleObjects)
        {
            base.scoreManager = scoreManager;
            _compositeEnemies = enemyObjects;
            _compositeParticles = compositeParticles;
            for (var i = 0; i < 3; i++)
            {
                enemyObjects[i].Init(base.scoreManager,compositeParticles[i]);
            }
            foreach (var enemy in _compositeEnemies) enemy.gameObject.SetActive(false);
            currentParticles = particleObjects;
        }
        
        internal override void GetDamage(int damage)
        {
            scoreManager.AddScore(damage);
            currentHealth -= damage;
            _healthBar.value = (float) currentHealth / _enemyConfig.Health;

            if (currentHealth < 1)
            {
                var position = transform.position;
                currentParticles.transform.position = position;
                currentParticles.PlayParticle();
                
                foreach (var enemy in _compositeEnemies)
                {
                    enemy.gameObject.SetActive(true);
                    enemy.transform.position = position;
                    enemy.Recreate(position,true);
                }

                ReturnToPool();

                DOTween.Kill(transform);
                currentHealth = _enemyConfig.Health;
            }
        }
    }
}
