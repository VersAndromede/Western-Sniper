using Scripts.EnemySystem;
using System;
using UnityEngine;

namespace Scripts.ShootingSystem
{
    public class PlayerWeapon
    {
        public event Action<Vector3> ShotFired;

        public void Shoot(Enemy enemy, Vector3 hitPoint)
        {
            ShotFired?.Invoke(hitPoint);
            enemy.TakeDamage();
        }
    }
}