using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
