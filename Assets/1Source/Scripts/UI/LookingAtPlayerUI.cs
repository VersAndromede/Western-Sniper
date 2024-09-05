using UnityEngine;

namespace Scripts.UI
{
    public class LookingAtPlayerUI : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector3 _rotateOffset = new Vector3(0, 180, 0);

        private void OnValidate()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            transform.LookAt(_camera.transform);
            transform.Rotate(_rotateOffset);
        }
    }
}