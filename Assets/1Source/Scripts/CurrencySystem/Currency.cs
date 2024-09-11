using System;

namespace Scripts.CurrencySystem
{
    [Serializable]
    public class Currency
    {
        public int Count { get; private set; }

        public event Action Changed;

        public void Add(uint count)
        {
            Count += (int)count;
            Changed?.Invoke();
        }

        public bool TryRemove(uint count)
        {
            if (count > Count)
                return false;

            Count -= (int)count;
            Changed?.Invoke();
            return true;
        }
    }
}