using Modules.SavingsSystem;
using Scripts.GameConfigSystem;
using Scripts.ShopSystem;
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
            builder.RegisterInstance(saveData.ShopItemsData);

            PriceFace priceFace = new(_gameConfig, saveData.FacePriceIndex);
            builder.RegisterInstance(priceFace);

            SceneManager.LoadScene($"Level {saveData.Level.Number}");
        }
    }
}