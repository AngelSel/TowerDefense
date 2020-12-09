﻿using System.Collections;
using Pool;
using UnityEngine;

namespace Objects
{
    public class ParticleObjects : PoolItem
    {
        [SerializeField] private ParticleSystem _thisParticle = default;
        public ParticleSystem ThisParticle => _thisParticle;
        private Coroutine _coroutine = default;

        internal void PlayParticle()
        {
            _thisParticle.Play();
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