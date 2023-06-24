using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public UIController _UIController;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameController.isGamePaused) PauseGame();
        else if(Input.GetKeyDown(KeyCode.Escape) && GameController.isGamePaused) UnPauseGame();
    }

    private void PauseGame()
    {
        GameController.isGamePlaying = false;
        GameController.isGamePaused = true;
    }

    private void UnPauseGame()
    {
        GameController.isGamePlaying = true;
        GameController.isGamePaused = false;
    }
}
