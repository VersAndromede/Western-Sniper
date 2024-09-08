using UnityEngine;

namespace Scripts.UI
{
    public class AnimatedUI : MonoBehaviour
    {
        private const string HideTrigger = "Hide";
        private const string ShowTrigger = "Show";
        private const string ExitTrigger = "Exit";

        [SerializeField] private Animator _animator;

        public void Hide()
        {
            _animator.SetTrigger(HideTrigger);
        }

        public void Show()
        {
            _animator.SetTrigger(ShowTrigger);
        }

        public void Exit()
        {
            _animator.SetTrigger(ExitTrigger);
        }
    }
}