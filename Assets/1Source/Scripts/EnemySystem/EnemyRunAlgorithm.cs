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

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        [SerializeField] private Animator _animator;
        [SerializeField] private Waypoint[] _waypoints;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private AnimationCurve _animationCurve;

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
            float distance = Vector3.Distance(currentPosition, waypoint.Position);
            float moveDuration = distance / _moveSpeed;

            await ValueEffectorUtility.Animate(
                moveDuration,
                _animationCurve,
                _cancellationTokenSource.Token,
                lerpFactor => Vector3.LerpUnclamped(currentPosition, waypoint.Position, lerpFactor),
                newPosition => _rigidbody.MovePosition(newPosition),
                waitFrame: WaitFrame.Fixed);
        }

        private void Rotate(Waypoint waypoint)
        {
            const float CorrectiveTurn = 180;

            Vector3 directionToWaypoint = (waypoint.Position - _rigidbody.position).normalized;
            Quaternion currentRotation = _rigidbody.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);

            targetRotation *= Quaternion.Euler(0, CorrectiveTurn, 0);

            float angle = Quaternion.Angle(currentRotation, targetRotation);
            float rotateDuration = angle / _rotateSpeed;

            ValueEffectorUtility.Animate(
                rotateDuration,
                _animationCurve,
                _cancellationTokenSource.Token,
                lerpFactor => Quaternion.Slerp(currentRotation, targetRotation, lerpFactor),
                newRotation => _rigidbody.MoveRotation(newRotation),
                waitFrame: WaitFrame.Fixed);
        }
    }
}
