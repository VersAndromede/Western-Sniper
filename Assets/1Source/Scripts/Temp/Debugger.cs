using TMPro;
using UnityEngine;

namespace Scripts.Temp
{
    public class Debugger : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate;
        [SerializeField] private float _targetTimeScale;
        [SerializeField] private bool _enabledCustomTimeScale;
        [SerializeField] private bool _enabledDebugger;
        [SerializeField] private TMP_Text _log;

        private void Start()
        {
            Application.logMessageReceived += OnLogMessageReceived;
        }

        private void Update()
        {
            Application.targetFrameRate = _targetFrameRate;

            if (_enabledCustomTimeScale)
                Time.timeScale = _targetTimeScale;
        }

        private void OnDestroy()
        {
            Application.logMessageReceived -= OnLogMessageReceived;
        }

        private void OnLogMessageReceived(string condition, string stackTrace, LogType type)
        {
            if (_enabledDebugger && _log != null)
                _log.text = $"{type}: {condition}\n";
        }
    }
}
