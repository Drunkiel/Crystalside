using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private bool isOptionsMenuOpen;
    public GameObject overwriteUI;

    public UnityEvent unityEvent;
    public SaveLoadController _saveLoadController;

    private void Start()
    {
        if (SaveLoadController.CheckIfSaveExists()) unityEvent.Invoke();
    }

    public void StartGame(bool checkForSave)
    {
        if (checkForSave && SaveLoadController.CheckIfSaveExists()) overwriteUI.SetActive(true);
        else
        {
            _saveLoadController.NewSave();
            SceneManager.LoadScene(1);
        }
    }

    public void Options()
    {
        isOptionsMenuOpen = !isOptionsMenuOpen;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
