using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Scripts.Utilities
{
    public static class ValueEffectorUtility
    {
        public static async UniTask Animate<T>(float duration, AnimationCurve curve, CancellationToken cancellationToken, Func<float, T> valueReceiver, Action<T> newValueCallback, Action completedCallback = null, WaitFrame waitFrame = WaitFrame.Default)
        {
            float progress = 0;

            while (progress < duration)
            {
                float lerpFactor = curve.Evaluate(progress / duration);
                T newValue = valueReceiver(lerpFactor);
                newValueCallback?.Invoke(newValue);
                progress += Time.deltaTime;
                await GetWait(waitFrame, cancellationToken);
            }

            completedCallback?.Invoke();
        }

        private static UniTask GetWait(WaitFrame waitFrame, CancellationToken cancellationToken)
        {
            switch (waitFrame)
            {
                case WaitFrame.Default:
                    return UniTask.NextFrame(cancellationToken);
                case WaitFrame.Fixed:
                    return UniTask.WaitForFixedUpdate(cancellationToken);
                default:
                    throw new ArgumentException();
            }
        }
    }
}