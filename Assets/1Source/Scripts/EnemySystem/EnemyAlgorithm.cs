using UnityEngine;

namespace Scripts.EnemySystem
{
    public abstract class EnemyAlgorithm : MonoBehaviour
    {
        public abstract void Run();

        public abstract void Stop();
    }
}
