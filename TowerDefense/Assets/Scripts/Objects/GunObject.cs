using System;
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
        
        private float _nextShotTime = default;
        private bool _isActive = false;

        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        public event Func<string,PoolItem> OnFiring = null;
        private void Start()
        {
            _nextShotTime = 0f;
        }

        private void FixedUpdate()
        {
            if (_isActive)
            {
                _gunPrefab.LookAt(_aimTarget);
                if(_gunPrefab.tag.Equals("LaserGun"))
                {
                    Vector3 rot = _gunPrefab.rotation.eulerAngles;
                    rot.x = 0;
                    _gunPrefab.rotation = Quaternion.Euler(rot);
                }

                if (Time.time > _nextShotTime)
                {
                    var item = OnFiring.Invoke(_bulletConfig.name);
                    var particle = OnFiring.Invoke(_bulletConfig.name + "Particle");

                    (item as BulletObject)?.Init(particle as ParticleObjects, _aimTarget.position);
                    
                    item.transform.position = _gunTransform.transform.position;
                    item.transform.rotation = _gunPrefab.rotation;
                
                    
                    _nextShotTime = Time.time + _gunConfig.FireRate;
                }
            }
        }

    }
}
