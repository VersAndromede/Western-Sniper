using Scripts.SceneLoaderSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.UI
{
    public class RestartButton : MonoBehaviour, IPointerClickHandler
    {
        private readonly SceneLoader _sceneLoader = new ();

        public void OnPointerClick(PointerEventData eventData)
        {
            _sceneLoader.Restart();
        }
    }
}