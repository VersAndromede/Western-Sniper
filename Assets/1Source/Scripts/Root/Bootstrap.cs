#if UNITY_EDITOR == false && UNITY_ANDROID == false
using Agava.YandexGames;
#endif
using System.Collections;
using UnityEngine;

namespace Scripts.Root
{
    public class Bootstrap : MonoBehaviour
    {
        private MainEntryPoint _entryPoint;

        private IEnumerator Start()
        {
#if UNITY_EDITOR == false && UNITY_ANDROID == false
            yield return YandexGamesSdk.Initialize();
            YandexGamesSdk.GameReady();
#endif

            _entryPoint = FindAnyObjectByType<MainEntryPoint>();
            _entryPoint.Build();
            yield return null;
        }
    }
}