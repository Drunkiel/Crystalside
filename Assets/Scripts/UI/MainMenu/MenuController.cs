using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private bool isOptionsMenuOpen;

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        //Some code here
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
