using Scripts.UI;
using TMPro;
using UnityEngine;

namespace Scripts.CurrencySystem
{
    public class AmountCurrencyPerLevelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelReward;
        [SerializeField] private TextMeshProUGUI _headshotBonus;

        public void UpdateView(uint levelReward, uint headshotBonus)
        {
            _levelReward.text = GetText(levelReward);
            _headshotBonus.text = GetText(headshotBonus);
        }

        private string GetText(uint value)
        {
            return $"+{value}";
        }
    }
}