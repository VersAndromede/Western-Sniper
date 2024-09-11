using Cysharp.Threading.Tasks;
using Scripts.Utilities;
using System.Threading;
using UnityEngine;

namespace Scripts.EnemySystem
{
    public class EnemyRunAlgorithm : EnemyAlgorithm
    {
        private const string IdleTrigger = "Idle";
        private const string RunTrigger = "Run";

        [SerializeField] private Animator _animator;
        [SerializeField] private Waypoint[] _waypoints;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _moveDuration;
        [SerializeField] private float _rotateDuration;
        [SerializeField] private AnimationCurve _animationCurve;

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
        }

        public override async void Run()
        {
            _animator.SetTrigger(RunTrigger);

            foreach (Waypoint waypoint in _waypoints)
            {
                Rotate(waypoint);
                await Move(waypoint);
            }
            
            _animator.SetTrigger(IdleTrigger);
        }

        public override void Stop()
        {
            _cancellationTokenSource.Cancel();
            Destroy(_rigidbody);
        }

        private async UniTask Move(Waypoint waypoint)
        {
            Vector3 currentPosition = _rigidbody.position;

            await ValueEffectorUtility.Animate(
                _moveDuration,
                _animationCurve,
                _cancellationTokenSource.Token,
                lerpFactor => Vector3.LerpUnclamped(currentPosition, waypoint.Position, lerpFactor),
                newPosition => _rigidbody.MovePosition(newPosition),
                () => _rigidbody.position = waypoint.Position,
                WaitFrame.Fixed);
        }

        private async UniTask Rotate(Waypoint waypoint)
        {
            const float CorrectiveTurn = 180;

            Vector3 directionToWaypoint = (waypoint.Position - _rigidbody.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);

            targetRotation *= Quaternion.Euler(0, CorrectiveTurn, 0);

            await ValueEffectorUtility.Animate(
                _rotateDuration,
                _animationCurve,
                _cancellationTokenSource.Token,
                lerpFactor => Quaternion.Slerp(_rigidbody.rotation, targetRotation, lerpFactor),
                newRotation => _rigidbody.MoveRotation(newRotation),
                () => _rigidbody.rotation = targetRotation,
                WaitFrame.Fixed);
        }
    }
}
