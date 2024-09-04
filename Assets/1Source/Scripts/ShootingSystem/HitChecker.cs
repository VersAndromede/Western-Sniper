using Scripts.EnemySystem;
using UnityEngine;

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

public class HitChecker
{
    public bool IsHitOnEnemy(out WeaponShotPoint weaponShotPoint)
    {
        weaponShotPoint = default;
        float screenCenterX = Screen.width / 2f;
        float screenCenterY = Screen.height / 2f;

        Vector3 screenCenter = new Vector2(screenCenterX, screenCenterY);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000))
        {
            weaponShotPoint.Position = hit.point;

            if (hit.collider.TryGetComponent(out EnemyPart findedPart))
            {
                if (findedPart.Type == EnemyPartType.Hat)
                    return false;

                weaponShotPoint.SetEnemy(findedPart.GetComponentInParent<Enemy>(), findedPart.Type);
                return true;
            }
        }

        return false;
    }
}