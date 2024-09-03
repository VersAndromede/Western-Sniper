using UnityEngine;

namespace Sctripts.Temp
{
    public class Debugger : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate;
        [SerializeField] private float _targetTimeScale;

        private void Update()
        {
            Application.targetFrameRate = _targetFrameRate;
            Time.timeScale = _targetTimeScale;
        }
    }
}
