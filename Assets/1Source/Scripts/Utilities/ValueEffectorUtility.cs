using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Scripts.Utilities
{
    public static class ValueEffectorUtility
    {
        public static async UniTask Animate<T>(float duration, AnimationCurve curve, CancellationToken cancellationToken, Func<float, T> valueReceiver, Action<T> newValueCallback, Action completedCallback)
        {
            float progress = 0;

            while (progress < duration)
            {
                float lerpFactor = curve.Evaluate(progress / duration);
                T newValue = valueReceiver(lerpFactor);
                newValueCallback?.Invoke(newValue);
                progress += Time.deltaTime;
                await UniTask.NextFrame(cancellationToken);
            }

            completedCallback?.Invoke();
        }
    }
}