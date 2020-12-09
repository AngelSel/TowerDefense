using System;
using System.Collections;
using System.Collections.Generic;
using GameObjectsConfigs;
using Managers;
using Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects
{
    public class EnemySpawn : MonoBehaviour
    {
        public event Func<string,PoolItem> OnGetItemFromPool = null;
        public event Action<bool> OnSpawnEnd = null; 

        private WaveStruct _currentWave = default;
        private Coroutine _coroutine = default;
        private ScoreManager _scoreManager = default;

        public void Starting()
        {
            if (_coroutine == null) _coroutine = StartCoroutine(SpawnTime());
        }

        internal void InitScore(ScoreManager scoreManager)
        {
            _scoreManager = scoreManager;
        }
    

        public void InitSpawner(WaveStruct wave)
        {
            _currentWave = wave;

        }

        private IEnumerator SpawnTime()
        {
            var waiter = new WaitForSeconds(_currentWave._spawnTime);
            var counter = 0;
            Vector3 position = default;
            float offset = default;
            List<EnemyObject> enemyObjects = new List<EnemyObject>(3);
            List<ParticleObjects> enemyParticles = new List<ParticleObjects>(3);
            var spawnAmount = _currentWave._amountOfBigEnemies + _currentWave._amountOfSmallEnemies +
                              _currentWave._amountOfMiddleEnemies;
            while (true)
            {
                yield return waiter;
                PoolItem item = null;
                PoolItem particleItem = default;

                if (counter < _currentWave._amountOfSmallEnemies)
                {
                    item = OnGetItemFromPool.Invoke("SmallEnemy");
                    particleItem = OnGetItemFromPool.Invoke("SmallEnemyParticle");
                }
                else if(_currentWave._amountOfSmallEnemies <= counter && counter < spawnAmount - _currentWave._amountOfBigEnemies)
                {
                    item = OnGetItemFromPool.Invoke("MiddleEnemy");
                    particleItem = OnGetItemFromPool.Invoke("MiddleEnemyParticle");
                }
                else if (spawnAmount - _currentWave._amountOfBigEnemies <= counter && counter < spawnAmount)
                {
                    item = OnGetItemFromPool.Invoke("BigEnemy");
                    particleItem = OnGetItemFromPool.Invoke("BigEnemyParticle");
                    PoolItem smallEnemy = default;
                    PoolItem smallParticle = default;
                    enemyObjects.Clear();
                    enemyParticles.Clear();
                    for (var i = 0; i < 2; i++)
                    {
                        smallEnemy = OnGetItemFromPool.Invoke("SmallEnemy");
                        smallEnemy.gameObject.SetActive(false);
                        enemyObjects.Add(smallEnemy as EnemyObject);
                        smallParticle = OnGetItemFromPool.Invoke("SmallEnemyParticle");
                        enemyParticles.Add(smallParticle as ParticleObjects);
                    }
                }

                position = transform.position;
                offset = Random.Range(-0.3f, 0.3f);
                position.x += offset;
                position.y -= offset;
                
                if (item != null && item.PoolTag.Equals("BigEnemy"))
                {
                    for (var i = 0; i < 2; i++)
                    {
                        enemyObjects[i].Init(_scoreManager,enemyParticles[i]);
                    }

                    (item as CompositeEnemy)?.Init(enemyObjects,_scoreManager,particleItem as ParticleObjects);
                    item.transform.position = position;
                    (item as CompositeEnemy)?.Recreate(position);
                }
                else
                {
                    (item as EnemyObject)?.Init(_scoreManager, particleItem as ParticleObjects);
                    item.transform.position = position;
                    (item as EnemyObject)?.Recreate(position);
                }

                counter++;
            
                if (counter >= spawnAmount)
                {
                    yield return new WaitForSeconds(_currentWave._nextWaveTime);
                    OnSpawnEnd.Invoke(true);
                    yield break;
                }
            }
        }

    }
}
