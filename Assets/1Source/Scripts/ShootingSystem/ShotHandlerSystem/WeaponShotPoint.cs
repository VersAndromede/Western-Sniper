using Scripts.EnemySystem;
using UnityEngine;

namespace Scripts.ShootingSystem.ShotHandlerSystem
{
    public struct WeaponShotPoint
    {
        public Vector3 Position;

        public Enemy Enemy { get; private set; }

        public EnemyPartType EnemyPartType { get; private set; }

        public bool IsHeadshot { get; private set; }

        public void SetEnemy(Enemy enemy, EnemyPartType enemyPartType)
        {
            Enemy = enemy;
            EnemyPartType = enemyPartType;

            if (enemyPartType == EnemyPartType.Head)
                IsHeadshot = true;
        }
    }
}