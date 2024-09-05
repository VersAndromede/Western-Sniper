using Scripts.GameConfigSystem;
using System;
using VContainer;

namespace Scripts.ShootingSystem.AmmunitionSystem
{
    public class Ammunition
    {
        [Inject]
        public Ammunition(GameConfig gameConfig)
        {
            MaxCount = gameConfig.PlayerWeaponConfig.AmmunitionCount;
            RelaodAll();
        }

        public uint MaxCount { get; private set; }

        public uint Count { get; private set; }

        public bool IsEmpty => Count == 0;

        public event Action Changed;

        public event Action Over;

        public void RelaodAll()
        {
            Count = MaxCount;
            Changed?.Invoke();
        }

        public void Remove()
        {
            if (IsEmpty)
                return;

            Count--;
            Changed?.Invoke();

            if (IsEmpty)
                Over?.Invoke();
        }
    }
}