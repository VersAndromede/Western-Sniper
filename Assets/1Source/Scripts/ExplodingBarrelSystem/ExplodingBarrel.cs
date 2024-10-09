using Scripts.EnemySystem;
using Scripts.GameConfigSystem;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace ExplodingBarrelSystem
{
    public class ExplodingBarrel : MonoBehaviour
    {
        [SerializeField] private ExplodingBarrelEffector _effector;

        private GameConfig _gameConfig;

        [Inject]
        public void Init(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }

        public void BlowUp()
        {
            _effector.Animate();
            Collider[] colliders = Physics.OverlapSphere(transform.position, _gameConfig.ExplosionRadius);

            List<SubjectToExplosion> subjectToExplosions = new();

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out SubjectToExplosion subjectToExplosion))
                    subjectToExplosions.Add(subjectToExplosion);

                if (collider.TryGetComponent(out Enemy enemy))
                {
                    enemy.Die();
                    subjectToExplosions.Remove(enemy);
                    AddForce(enemy, _gameConfig.EnemyExplosionForceMultiplier, enemy.Body);
                }
            }

            foreach (SubjectToExplosion subject in subjectToExplosions)
                AddForce(subject);
        }

        private void AddForce(SubjectToExplosion subject, float multiplier = 1, Rigidbody rigidbody = null)
        {
            rigidbody ??= subject.Rigidbody;

            Vector3 direction = (subject.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(subject.transform.position, transform.position);
            float forceMultiplier = Mathf.Clamp01(1 - (distance / _gameConfig.ExplosionRadius));

            float explosionMultiplier = _gameConfig.MaxExplosionForce * forceMultiplier * multiplier;
            Vector3 finalForce = direction * explosionMultiplier;
            finalForce.y = explosionMultiplier;

            rigidbody.isKinematic = false;
            subject.AddForce(finalForce, rigidbody);
            subject.AddTorque(finalForce, rigidbody);
        }
    }
}