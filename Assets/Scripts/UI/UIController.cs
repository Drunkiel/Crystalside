using UnityEngine;

public class UIController : MonoBehaviour
{
    public bool isUIOpen;
    public GameObject UI;

    public void OpenCloseUI(bool unPause = true)
    {
        isUIOpen = !isUIOpen;
        UI.SetActive(isUIOpen);
        if (unPause)
        {
            GameController.isGamePaused = isUIOpen;
            CameraController.ChangeLockState(isUIOpen);
        }
    }
}
