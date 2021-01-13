using System;
using System.Collections;
using System.Xml.Schema;
using DG.Tweening;
using GameObjectsConfigs;
using Lean.Touch;
using Pool;
using UnityEngine;

namespace Objects
{
    public class GunObject : MonoBehaviour
    {
        [SerializeField] private Transform _gunTransform = default;
        [SerializeField] private Transform _gunPrefab = default;
        [SerializeField]private GunConfig _gunConfig = default;
        [SerializeField] private BulletConfig _bulletConfig = default;
        [SerializeField] private Transform _aimTarget = default;
        
        private Coroutine _coroutine = default;
        private bool _isActive = false;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                if(_isActive)
                    _coroutine = StartCoroutine(SpawnTime());
                else
                {
                    StopCoroutine(_coroutine);
                }
            }
        }

        public event Func<string,PoolItem> OnFiring = null;
 
        private void FixedUpdate()
        {
            _gunPrefab.LookAt(_aimTarget);
            if (_gunPrefab.tag.Equals("LaserGun"))
            {
                Vector3 rot = _gunPrefab.rotation.eulerAngles;
                rot.x = 0;
                _gunPrefab.rotation = Quaternion.Euler(rot);
            }
        }

        private IEnumerator SpawnTime()
        {
            var waiter = new WaitForSeconds(_gunConfig.FireRate);
            while (true)
            {
                yield return waiter;

                var item = OnFiring.Invoke(_bulletConfig.name);
                var particle = OnFiring.Invoke(_bulletConfig.name + "Particle");

                (item as BulletObject)?.Init(particle as ParticleObjects, _aimTarget.position);

                item.transform.position = _gunTransform.transform.position;
                item.transform.rotation = _gunPrefab.rotation;
 
            }
        }

    }
}
