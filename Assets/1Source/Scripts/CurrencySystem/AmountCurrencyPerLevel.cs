using System;
using Scripts.GameConfigSystem;
using Scripts.ShootingSystem.PlayerWeaponSystem;
using VContainer;

namespace Scripts.CurrencySystem
{
    public class AmountCurrencyPerLevel : IDisposable
    {
        private readonly PlayerWeapon _weapon;
        private readonly GameConfig _gameConfig;

        public uint LevelReward { get; private set; }

        public uint HeadshotBonus { get; private set; }

        [Inject]
        public AmountCurrencyPerLevel(PlayerWeapon weapon, GameConfig gameConfig)
        {
            _weapon = weapon;
            _gameConfig = gameConfig;
            LevelReward = _gameConfig.LevelReward;

            _weapon.Headshot += OnHeadshot;
        }

        public void Dispose()
        {
            _weapon.Headshot -= OnHeadshot;
        }

        public uint GetAmount()
        {
            return LevelReward + HeadshotBonus;
        }

        private void OnHeadshot()
        {
            HeadshotBonus += (uint)(LevelReward * _gameConfig.HeadshotBonusMultiplier);
        }
    }
}