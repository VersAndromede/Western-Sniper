using UnityEngine;

namespace Scripts.EnemySystem
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimatorContainer : MonoBehaviour
    {
        public Animator Animator { get; private set; }

        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        public void SetTrigger(string trigger)
        {
            Animator.SetTrigger(trigger);
        }
    }
}
