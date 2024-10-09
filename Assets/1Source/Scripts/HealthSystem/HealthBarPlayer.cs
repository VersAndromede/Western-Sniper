using UnityEngine;

namespace Scripts.HealthSystem
{
    public class HealthBarPlayer : HealthBar
    {
        [SerializeField] private GameObject _bar;

        protected override void HandleZeroHealth()
        {
            _bar.SetActive(false);
        }
    }
}