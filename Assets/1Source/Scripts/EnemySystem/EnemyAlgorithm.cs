using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scripts.EnemySystem
{
    public abstract class EnemyAlgorithm : MonoBehaviour
    {
        protected const string IdleTrigger = "Idle";
        protected const string RunTrigger = "Run";
        protected const string ShootingTrigger = "Shooting";

        [SerializeField] protected EnemyMover Mover;

        protected EnemyAnimatorContainer Animator { get; private set; }

        private void Start()
        {
            OnStart();
        }

        public abstract UniTask Run();

        public virtual void OnStart()
        {
            Animator = GetComponentInChildren<EnemyAnimatorContainer>();
        }

        public virtual void Stop()
        {
            Animator.SetTrigger(IdleTrigger);
        }
    }
}
