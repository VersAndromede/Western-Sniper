using Modules.SavingsSystem;
using UnityEditor;
using UnityEngine;

public class PlayerPrefsConfigWindow : EditorWindow
{
    private readonly SaveSystem _saveSystem = new();

    private string _data;

    [MenuItem("Window/Custom/PlayerPrefs Config")]
    public static void ShowWindow()
    {
        GetWindow<PlayerPrefsConfigWindow>("PlayerPrefs Config");
    }

    private void OnGUI()
    {
        GUILayout.Label("PlayerPrefs Config", EditorStyles.boldLabel);
        GUILayout.Space(10);

        GUILayout.Label("Data (JSON):", EditorStyles.label);
        _data = EditorGUILayout.TextArea(_data, GUILayout.Height(550));
        GUILayout.Space(10);

        if (GUILayout.Button("Get", GUILayout.Height(30)))
        {
            _data = PlayerPrefs.GetString(SaveSystem.SaveDataPrefsKey, "");
            AssetDatabase.SaveAssets();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Set", GUILayout.Height(30)))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(_data);

            if (saveData != null)
            {
                _saveSystem.Save(data =>
                {
                    data = saveData;
                });

                PlayerPrefs.SetString(SaveSystem.SaveDataPrefsKey, _data);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.LogError("Invalid JSON format.");
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        GUILayout.Space(20);
    }
}
