using UnityEngine;

namespace ExplodingBarrelSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class SubjectToExplosion : MonoBehaviour
    {
        public Rigidbody Rigidbody { get; private set; }

        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        public void AddForce(Vector3 force, Rigidbody rigidbody = null)
        {
            rigidbody ??= Rigidbody;
            rigidbody.AddForce(force, ForceMode.Impulse);
        }

        public void AddTorque(Vector3 torque, Rigidbody rigidbody = null)
        {
            rigidbody ??= Rigidbody;
            rigidbody.AddTorque(torque, ForceMode.Impulse);
        }
    }
}