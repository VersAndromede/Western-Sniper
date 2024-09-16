using System;
using UnityEngine;

namespace Scripts.EnemySystem
{
    public class BulletCatcher : MonoBehaviour
    {
        [field: SerializeField] public bool IsDamagingPlayer { get; private set; }

        public event Action Catched;

        public void Catch()
        {
            Catched?.Invoke();
        }
    }
}
