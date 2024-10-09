using System;
using UnityEngine;

namespace Modules.SavingsSystem
{
    public class SaveSystem
    {
        private const string SaveDataPrefsKey = "SaveDataPrefsKey";

        public void Save(Action<SaveData> dataChanges)
        {
            SaveData saveData = Load();
            dataChanges?.Invoke(saveData);
            string saveDataJson = JsonUtility.ToJson(saveData, true);
            PlayerPrefs.SetString(SaveDataPrefsKey, saveDataJson);
            PlayerPrefs.Save();
        }

        public SaveData Load()
        {
            if (PlayerPrefs.HasKey(SaveDataPrefsKey))
            {
                string saveDataJson = PlayerPrefs.GetString(SaveDataPrefsKey);
                return JsonUtility.FromJson<SaveData>(saveDataJson);
            }

            return new SaveData();
        }
    }
}