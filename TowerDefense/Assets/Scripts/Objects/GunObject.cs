using System;
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
        
        private float nextShotTime = default;

        private bool isActive = false;

        public bool IsActive
        {
            get => isActive;
            set => isActive = value;
        }

        private void OnEnable()
        {
            LeanTouch.OnFingerUpdate += FingerOn;
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerUpdate -= FingerOn;
        }

        public event Func<string,PoolItem> OnFiring = null;
        private void Start()
        {
            nextShotTime = 0f;
        }
        
        private void FingerOn(LeanFinger finger)
        {
            if (isActive)
            {
                _gunPrefab.LookAt(_aimTarget);
                if (Time.time > nextShotTime )
                {
                    var item = OnFiring.Invoke(_bulletConfig.name);
                    var particle = OnFiring.Invoke(_bulletConfig.name + "Particle");
                    
                    (item as BulletObject)?.Init(particle as ParticleObjects);
                    
                    item.transform.position = _gunTransform.transform.position;
                    item.transform.DOMove(_aimTarget.position, _bulletConfig.Speed).OnComplete((item as BulletObject).Print);
                    nextShotTime = Time.time + _gunConfig.FireRate;
                } 
            }
        }
    }
}
