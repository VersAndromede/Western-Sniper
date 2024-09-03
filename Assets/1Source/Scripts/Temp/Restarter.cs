using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour, IPointerUpHandler
{
    private void Start()
    {
        Application.targetFrameRate = 90;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
