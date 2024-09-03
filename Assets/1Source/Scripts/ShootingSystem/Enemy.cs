using UnityEngine;

namespace Scripts.EnemySystem.Body
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyEffector _effector;

        public void Die(Vector3 hitPoint)
        {
            _effector.PlayDeth(hitPoint);
        }
    }
}
