using UnityEngine;

namespace Scripts.EnemySystem
{
    public class EnemyPart : MonoBehaviour
    {
        [field: SerializeField] public EnemyPartType Type { get; private set; }
    }
}
