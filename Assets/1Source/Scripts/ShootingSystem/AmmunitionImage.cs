using UnityEngine;

namespace Scripts.ShootingSystem
{
    public class AmmunitionImage : MonoBehaviour
    {
        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable() 
        {
            gameObject.SetActive(false);
        }
    }
}