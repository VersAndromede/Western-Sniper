using UnityEngine;

namespace Scripts.ShootingSystem.AmmunitionSystem
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