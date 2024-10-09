using Cysharp.Threading.Tasks;
using Scripts.GameConfigSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Scripts.LevelSystem
{
    public class LevelLoader : MonoBehaviour
    {
        private const string SceneName = "Level";

        private GameConfig _gameConfig;
        private Level _level;

        [Inject]
        private void Construct(Level level, GameConfig gameConfig)
        {
            _level = level;
            _gameConfig = gameConfig;
        }

        public void LoadNext()
        {
            uint levels = _gameConfig.LocationConfig.LevelNumbers;
            uint locations = (uint)_gameConfig.LocationConfig.LocationIcons.Length;
            uint maxLevel = levels * locations;

            _level.SetNextLevel(maxLevel);
            _level.SetNextLocation(levels);
            LoadScene();
        }

        private async UniTask LoadScene()
        {
            string sceneName = $"{SceneName} {_level.Number}";

            await UniTask.WaitForSeconds(_gameConfig.LoadLevelDelay, cancellationToken: destroyCancellationToken);
            SceneManager.LoadScene(sceneName);
        }
    }
}