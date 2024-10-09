using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.EnemySystem
{
    public class EnemyEffector : MonoBehaviour
    {
        private const string HeadshotTrigger = "Headshot";

        [SerializeField] private DieEffect _dieEffect;
        [SerializeField] private Animator _headshotTextAnimator;
        [SerializeField] private Vector2 _randomXForce;
        [SerializeField] private Vector2 _randomYForce;
        [SerializeField] private Vector2 _randomZForce;
        [SerializeField] private Vector2 _randomXHatTorque;
        [SerializeField] private float _multiplierForForceToHat;

        private List<Rigidbody> _rigidbodies;

        public bool HatFlewOff { get; private set; }

        public Rigidbody Body { get; private set; }

        private void Start()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>(true).ToList();
            Body = FindEnemyPart(EnemyPartType.Body);

            if (TryGetComponent(out Rigidbody mainRigidbody))
                _rigidbodies.Remove(mainRigidbody);

            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = true;

                if (rigidbody.TryGetComponent(out EnemyPart _) == false)
                    rigidbody.gameObject.AddComponent<EnemyPart>();
            }
        }

        public void PlayDeth()
        {
            _dieEffect.Play();
        }

        public void ThrowHat()
        {
            Rigidbody hat = FindEnemyPart(EnemyPartType.Hat);
            hat.transform.SetParent(null);
            hat.isKinematic = false;
            AddForce(hat, _multiplierForForceToHat);
            AddTorque(hat);
            HatFlewOff = true;
        }

        public void PlayHeadshot()
        {
            _headshotTextAnimator.SetTrigger(HeadshotTrigger);
        }

        public void AddBodyForce()
        {
            AddForce(Body);
        }

        private void AddForce(Rigidbody rigidbody, float multiplier = 1)
        {
            float xForce = Random.Range(_randomXForce.x, _randomXForce.y);
            float yForce = Random.Range(_randomYForce.x, _randomYForce.y);
            float zForce = Random.Range(_randomZForce.x, _randomZForce.y);

            Vector3 force = new Vector3(xForce, yForce, zForce) * multiplier;
            rigidbody.AddForce(force, ForceMode.Impulse);
        }

        private void AddTorque(Rigidbody rigidbody)
        {
            float xTorque = Random.Range(_randomXHatTorque.x, _randomXHatTorque.y);
            Vector3 torque = new Vector3(xTorque, 0, 0);
            rigidbody.AddTorque(torque, ForceMode.Impulse);
        }

        private Rigidbody FindEnemyPart(EnemyPartType type)
        {
            foreach (Rigidbody rigidbody in _rigidbodies)
                if (rigidbody.TryGetComponent(out EnemyPart part))
                    if (part.Type == type)
                        return rigidbody;

            throw new ArgumentException();
        }
    }
}
