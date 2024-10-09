using ExplodingBarrelSystem;
using Scripts.EnemySystem;
using System;
using UnityEngine;

namespace Scripts.ShootingSystem.ShotHandlerSystem
{
    public class HitChecker
    {
        public bool IsHitOnEnemy(LayerMask layerMask, out WeaponShotPoint weaponShotPoint)
        {
            weaponShotPoint = default;

            if (IsHit(layerMask, out RaycastHit hit))
            {
                weaponShotPoint.Position = hit.point;

                if (hit.collider.TryGetComponent(out EnemyPart findedPart))
                {
                    weaponShotPoint.SetEnemy(findedPart.GetComponentInParent<Enemy>(), findedPart.Type);
                    return true;
                }
            }

            return false;
        }

        public bool IsHitOnExplodingBarrel(LayerMask layerMask, out ExplodingBarrel explodingBarrel)
        {
            if (IsHit(layerMask, out RaycastHit hit))
                if (hit.collider.TryGetComponent(out explodingBarrel))
                    return true;

            explodingBarrel = null;
            return false;
        }

        private bool IsHit(LayerMask layerMask, out RaycastHit hit)
        {
            float screenCenterX = Screen.width / 2f;
            float screenCenterY = Screen.height / 2f;

            Vector3 screenCenter = new Vector2(screenCenterX, screenCenterY);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);

            return Physics.Raycast(ray, out hit, 1000, layerMask);
        }
    }
}