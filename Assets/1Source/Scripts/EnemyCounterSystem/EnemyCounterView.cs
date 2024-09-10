using TMPro;
using UnityEngine;

namespace EnemyCounterSystem
{
    public class EnemyCounterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _checkmark;

        public void UpdateCount(int current, int max)
        {
            if (_text != null)
                _text.text = $"{current}/{max}";

            if (current == max)
                _checkmark.SetActive(true);
        }
    }
}