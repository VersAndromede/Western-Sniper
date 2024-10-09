using TMPro;
using UnityEngine;
using VContainer;

namespace Scripts.LevelSystem
{
    public class LevelNumberView : MonoBehaviour
    {
        private const string ContractName = "Contract";

        [SerializeField] private TextMeshProUGUI _text;

        [Inject]
        private void Construct(Level level)
        {
            _text.text = $"{ContractName} {level.Number}";
        }
    }
}