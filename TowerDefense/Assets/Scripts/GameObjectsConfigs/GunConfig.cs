using UnityEngine;

namespace GameObjectsConfigs
{
    [CreateAssetMenu(menuName = "Configs/Gun Config",fileName = "GunConfig")]
    public class GunConfig : ScriptableObject
    {
        //[SerializeField] private GameObject gunPrefab = default;
        [SerializeField] private float fireRate = default;

        //public GameObject GunPrefab => gunPrefab;
        public float FireRate => fireRate;
    }
}