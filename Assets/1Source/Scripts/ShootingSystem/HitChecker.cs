using Scripts.EnemySystem.Body;
using UnityEngine;

public class HitChecker
{
    public bool Check(out Enemy enemy)
    {
        enemy = null;

        float screenCenterX = Screen.width / 2f;
        float screenCenterY = Screen.height / 2f;

        Vector3 screenCenter = new Vector2(screenCenterX, screenCenterY);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000))
        {
            if (hit.collider.TryGetComponent(out EnemyPart enemyPart))
            {
                if (enemyPart.Type == EnemyPartType.Hat)
                    return false;

                enemy = enemyPart.GetComponentInParent<Enemy>();
                return true;
            }
        }

        return false;
    }
}