using System;
using UnityEngine;

namespace Objects
{
    public class LevelEndLine : MonoBehaviour
    {
        public event Action<bool> OnGameOver = null;
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Enemy"))
            {
                transform.gameObject.SetActive(false);
                OnGameOver.Invoke(false);
            }
        }
    }
}
