using UnityEngine;

namespace GameObjectsConfigs
{
    [CreateAssetMenu(menuName = "Configs/Gun Config",fileName = "GunConfig")]
    public class GunConfig : ScriptableObject
    {
        [SerializeField] private float fireRate = default;
        
        public float FireRate => fireRate;
    }
}