using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public UIController _UIController;

    // Update is called once per frame
    void Update()
    {
        if (GameController.isGamePlaying)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !GameController.isGamePaused) PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        GameController.isGamePlaying = false;
        GameController.isGamePaused = true;
        _UIController.OpenCloseUI();
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        GameController.isGamePlaying = true;
        GameController.isGamePaused = false;
        _UIController.OpenCloseUI();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
