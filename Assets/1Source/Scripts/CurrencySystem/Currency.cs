using System;

namespace Scripts.CurrencySystem
{
    [Serializable]
    public class Currency
    {
        public uint Count { get; private set; }

        public event Action Changed;

        public void Add(uint count)
        {
            Count += count;
            Changed?.Invoke();
        }

        public bool TryRemove(uint count)
        {
            if (count > Count)
                return false;

            Count -= count;
            Changed?.Invoke();
            return true;
        }
    }
}