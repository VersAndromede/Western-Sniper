using Scripts.EnemySystem.Body;
using System;
using UnityEngine;

namespace Scripts.ShootingSystem
{
    public class PlayerWeapon
    {
        public event Action ShotFired;

        public void Shoot(Enemy enemy, Vector3 hitPoint)
        {
            ShotFired?.Invoke();
            enemy.Die(hitPoint);
        }
    }
}