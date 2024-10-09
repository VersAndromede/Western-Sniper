using UnityEngine;

namespace Scripts.Utilities
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyUtility : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    public enum WaitFrame
    {
        Default,
        Fixed,
    }
}