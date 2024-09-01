using UnityEngine;

namespace Scripts.CameraSystem
{
    public class AnimatedUI : MonoBehaviour
    {
        private const string HideTrigger = "Hide";
        private const string ShowTrigger = "Show";

        [SerializeField] private Animator _animator;

        public void Hide()
        {
            _animator.SetTrigger(HideTrigger);
        }

        public void Show()
        {
            _animator.SetTrigger(ShowTrigger);
        }
    }
}