using UnityEngine;

namespace Scripts.EnemySystem.Body
{
    public class EnemyPart : MonoBehaviour
    {
        [field: SerializeField] public EnemyPartType Type { get; private set; }
    }
}
