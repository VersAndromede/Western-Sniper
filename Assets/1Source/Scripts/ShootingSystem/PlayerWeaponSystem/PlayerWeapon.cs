﻿using System;
using Cysharp.Threading.Tasks;
using Scripts.GameConfigSystem;
using Scripts.ShootingSystem.AmmunitionSystem;
using Scripts.ShootingSystem.ShotHandlerSystem;
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

        public event Action BulletInserted;

        public event Action Reloading;

        public event Action Reloaded;

        public event Action Headshot;

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
                {
                    Headshot?.Invoke();
                    weaponShotPoint.Enemy.PlayHeadshot();
                }

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
            BulletInserted?.Invoke();
        }

        private async UniTask ReloadAll()
        {
            float delay = _config.PreReloadTime + _config.ReloadTime;

            await UniTask.WaitForSeconds(_config.BulletInsertionTime);
            Reloading?.Invoke();
            await UniTask.WaitForSeconds(delay);

            _ammunition.RelaodAll();
            _canShoot = true;
            Reloaded?.Invoke();
        }

        private bool IsCanShoot()
        {
            return _canShoot && _ammunition.IsEmpty == false;
        }
    }
}