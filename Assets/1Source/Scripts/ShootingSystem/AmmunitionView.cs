using UnityEngine;

namespace Scripts.ShootingSystem
{
    public class AmmunitionView : MonoBehaviour
    {
        [SerializeField] private AmmunitionImage[] _ammunitionImages;

        public void UpdateIcons(uint ammunitionCount, uint ammunitionMaxCount)
        {
            if (ammunitionCount == ammunitionMaxCount)
            {
                EnableIcons();
                return;
            }

            _ammunitionImages[ammunitionCount].Disable();
        }

        private void EnableIcons()
        {
            foreach (AmmunitionImage ammunitionImage in _ammunitionImages)
                ammunitionImage.Enable();
        }
    }
}