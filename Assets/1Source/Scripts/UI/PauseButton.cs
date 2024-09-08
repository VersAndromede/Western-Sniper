using System.PauseSystem;
using UnityEngine;

namespace Scripts.UI
{
    public class PauseButton : MonoBehaviour
    {
        private readonly PauseSetter _pauseSetter = new();

        public void Enable()
        {
            _pauseSetter.Enable();
        }

        public void Disable()
        {
            _pauseSetter.Disable();
        }
    }
}