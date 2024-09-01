using UnityEngine;

namespace Scripts.CameraSystem
{
    [RequireComponent (typeof(AnimatedUI))]
    public class AnimatedButton : PressedButton
    {
        private AnimatedUI _animator;

        private void Start()
        {
            _animator = GetComponent<AnimatedUI>();  
        }

        public void Hide()
        {
            _animator.Hide();
        }

        public void Show()
        {
            _animator.Show();
        }
    }
}