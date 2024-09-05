using Cysharp.Threading.Tasks;
using Scripts.GameConfigSystem;
using Scripts.ShootingSystem.AmmunitionSystem;
using Scripts.ShootingSystem.ShotHandlerSystem;
using System;
using UnityEngine;

namespace Scripts.ShootingSystem.PlayerWeaponSystem
{
    public class PlayerWeapon
    {
        private Ammunition _ammunition;
        private PlayerWeaponConfig _config;
        private bool _canShoot = true;

        public bool IsEmpty => _ammunition.IsEmpty;

        public event Action<Vector3> ShotFired;

        public void Init(Ammunition ammunition, GameConfig gameConfig)
        {
            _ammunition = ammunition;
            _config = gameConfig.PlayerWeaponConfig;
        }

        public void ShootEnemy(WeaponShotPoint weaponShotPoint)
        {
            Shoot(weaponShotPoint.Position, () =>
            {
                if (weaponShotPoint.IsHeadshot)
                    weaponShotPoint.Enemy.PlayHeadshot();

                uint damage = GetDamage(weaponShotPoint.IsHeadshot);
                weaponShotPoint.Enemy.TakeDamage(damage);
            });
        }

        public void Shoot(Vector3 hitPoint, Action successfulCallback = null)
        {
            if (IsCanShoot())
            {
                _canShoot = false;
                _ammunition.Remove();
                ShotFired?.Invoke(hitPoint);
                successfulCallback?.Invoke();

                if (_ammunition.IsEmpty)
                    ReloadAll();
                else
                    Relaod();
            }
        }

        private uint GetDamage(bool isHeadshot)
        {
            if (isHeadshot)
                return _config.Damage * _config.HeadshotMultiplier;

            return _config.Damage;
        }

        private async UniTask Relaod()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_config.BulletInsertionTime));
            _canShoot = true;
        }

        private async UniTask ReloadAll()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_config.PreReloadTime));
            await UniTask.Delay(TimeSpan.FromSeconds(_config.ReloadTime));
            _ammunition.RelaodAll();
            _canShoot = true;
        }

        private bool IsCanShoot()
        {
            return _canShoot && _ammunition.IsEmpty == false;
        }
    }
}