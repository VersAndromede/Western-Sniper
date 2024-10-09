using Scripts.GameConfigSystem;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Scripts.LevelSystem
{
    public class LevelsView : MonoBehaviour
    {
        [SerializeField] private Image _locationIcon;
        [SerializeField] private LevelView[] _levelViews;

        [Inject]
        private void Construct(GameConfig gameConfig, Level level)
        {
            UpdateView(gameConfig.LocationConfig, level);
        }

        private void UpdateView(LocationConfig config, Level level)
        {
            uint lastLevelIndex = config.LevelNumbers - 1;
            uint currentLevelIndex = level.Number % config.LevelNumbers - 1;
            uint currentLocationIndex = level.Location - 1;

            _locationIcon.sprite = config.LocationIcons[currentLocationIndex];

            for (int i = 0; i < config.LevelNumbers; i++)
            {
                if (i == lastLevelIndex)
                    _levelViews[i].SetBossColor();

                if (i <= currentLevelIndex)
                    _levelViews[i].SetCompletedColor();
            }
        }
    }
}