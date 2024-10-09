using UnityEngine;
using VContainer;

namespace Scripts.UI
{
    public class GameCanvasActivator : MonoBehaviour
    {
        [SerializeField] private PressedButton _pressedButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _menuCanvas;

        private bool _isActivated;

        [Inject]
        private void Construct()
        {
            _pressedButton.Down += OnDown;
        }

        private void OnDestroy()
        {
            if (_isActivated == false)
                _pressedButton.Down -= OnDown;
        }

        private void OnDown()
        {
            const float FullAlpha = 1f;

            _isActivated = true;

            _pressedButton.Down -= OnDown;

            _canvasGroup.alpha = FullAlpha;
            _menuCanvas.SetActive(false);
        }
    }
}