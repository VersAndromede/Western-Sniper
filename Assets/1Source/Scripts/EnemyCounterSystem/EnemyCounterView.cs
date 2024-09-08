using TMPro;
using UnityEngine;

namespace EnemyCounterSystem
{
    public class EnemyCounterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void UpdateCount(int current, int max)
        {
            _text.text = $"{current}/{max}";
        }
    }
}