using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Scripts.EnemySystem
{
    public class EnemyRunAlgorithm : EnemyAlgorithm
    {
        public CancellationTokenSource CancellationTokenSource { get; private set; } = new();

        [SerializeField] private Waypoint[] _waypoints;

        private void OnDestroy()
        {
            CancellationTokenSource.Cancel();
        }

        public override async UniTask Run()
        {
            Animator.SetTrigger(RunTrigger);

            foreach (Waypoint waypoint in _waypoints)
            {
                Mover.Rotate(waypoint.Position, CancellationTokenSource);
                await Mover.Move(waypoint, CancellationTokenSource);
            }

            Animator.SetTrigger(IdleTrigger);
        }

        public override void Stop()
        {
            base.Stop();
            CancellationTokenSource.Cancel();
        }
    }
}
