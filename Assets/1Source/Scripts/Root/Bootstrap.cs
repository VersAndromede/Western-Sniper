#if UNITY_EDITOR == false && UNITY_ANDROID == false
using Agava.YandexGames;
#endif
using System.Collections;
using UnityEngine;

namespace Scripts.Root
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LevelEntryPoint _levelEntryPoint;

        private IEnumerator Start()
        {
#if UNITY_EDITOR == false && UNITY_ANDROID == false
            yield return YandexGamesSdk.Initialize();
            YandexGamesSdk.GameReady();
#endif
            _levelEntryPoint.Build();
            yield return null;
        }
    }
}