using UnityEngine;

namespace Scripts.Temp
{
    public class Debugger : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate;
        [SerializeField] private float _targetTimeScale;
        [SerializeField] private bool _enabledCustomTimeScale;

        private void Update()
        {
            Application.targetFrameRate = _targetFrameRate;

            if (_enabledCustomTimeScale)
                Time.timeScale = _targetTimeScale;
        }
    }
}
