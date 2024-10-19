using Cysharp.Threading.Tasks;
using Scripts.Utilities;
using System;
using System.Threading;
using UnityEngine;

namespace Scripts.EnemySystem
{
    [Serializable]
    public class EnemyMover
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private AnimationCurve _animationCurve;

        public void Rotate(Vector3 target, CancellationTokenSource cancellationTokenSource)
        {
            const float CorrectiveTurn = 180;

            Vector3 directionToWaypoint = (target - _rigidbody.position).normalized;
            Quaternion currentRotation = _rigidbody.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);

            targetRotation *= Quaternion.Euler(0, CorrectiveTurn, 0);

            float angle = Quaternion.Angle(currentRotation, targetRotation);
            float rotateDuration = angle / _rotateSpeed;

            ValueEffectorUtility.Animate(
                rotateDuration,
                _animationCurve,
                cancellationTokenSource.Token,
                lerpFactor => Quaternion.Slerp(currentRotation, targetRotation, lerpFactor),
                newRotation => _rigidbody.MoveRotation(newRotation),
                waitFrame: WaitFrame.Fixed);
        }

        public async UniTask Move(Waypoint waypoint, CancellationTokenSource cancellationTokenSource)
        {
            Vector3 currentPosition = _rigidbody.position;
            float distance = Vector3.Distance(currentPosition, waypoint.Position);
            float moveDuration = distance / _moveSpeed;

            await ValueEffectorUtility.Animate(
                moveDuration,
                _animationCurve,
                cancellationTokenSource.Token,
                lerpFactor => Vector3.LerpUnclamped(currentPosition, waypoint.Position, lerpFactor),
                newPosition => _rigidbody.MovePosition(newPosition),
                waitFrame: WaitFrame.Fixed);
        }
    }
}
