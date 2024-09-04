using System;
using UnityEngine;

namespace Scripts.HealthSystem
{
    public class Health
    {
        public Health(int count)
        {
            MaxCount = count;
            Count = count;
        }

        public int MaxCount { get; private set; }

        public int Count { get; private set; }

        public event Action Changed;

        public event Action Over;

        public void TakeDamage(uint count)
        {
            if (Count == 0)
                return;

            Count -= (int)count;
            Count = Mathf.Clamp(Count, 0, MaxCount);
            Changed?.Invoke();

            if (Count == 0)
                Over?.Invoke();
        }
    }
}