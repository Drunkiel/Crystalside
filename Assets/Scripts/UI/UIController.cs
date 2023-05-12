using UnityEngine;

public class UIController : MonoBehaviour
{
    public bool isUIOpen;
    public GameObject UI;
    
    public void OpenCloseUI()
    {
        isUIOpen = !isUIOpen;
        UI.SetActive(isUIOpen);

        CameraController.ChangeLockState(isUIOpen);
    }
}
