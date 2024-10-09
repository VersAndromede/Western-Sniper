using Modules.SavingsSystem;
using Scripts.GameConfigSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Scripts.Root
{
    public class MainEntryPoint : LifetimeScope
    {
        [SerializeField] private GameConfig _gameConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            SaveSystem saveSystem = new SaveSystem();
            SaveData saveData = saveSystem.Load();

            builder.RegisterInstance(_gameConfig);
            builder.RegisterInstance(saveSystem);
            builder.RegisterInstance(saveData.Level);

            SceneManager.LoadScene($"Level {saveData.Level.Number}");
        }
    }
}