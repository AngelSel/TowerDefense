using System;
using System.Collections;
using Pool;
using UnityEngine;

namespace Objects
{
    public class ParticleObjects : PoolItem
    {
        [SerializeField] private ParticleSystem _thisParticle = default;
        private Coroutine _coroutine = default;

        internal void PlayParticle()
        {
            _thisParticle.Play();
            if(transform.gameObject.activeInHierarchy)
                _coroutine = StartCoroutine(Returning());
        }

        internal void StopParticle()
        {
            _thisParticle.Stop();
        }

        private IEnumerator Returning()
        {
            yield return new WaitForSeconds(_thisParticle.main.duration);
            ReturnToPool();
        }

    }
}
