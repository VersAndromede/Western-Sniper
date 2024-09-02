using Scripts.EnemySystem.Body;
using System;

namespace Scripts.ShootingSystem
{
    public class PlayerWeapon
    {
        public event Action ShotFired;

        public void Shoot(Enemy enemy)
        {
            ShotFired?.Invoke();
            enemy.Die();
        }
    }
}