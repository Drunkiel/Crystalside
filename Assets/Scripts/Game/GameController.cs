using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool isGamePlaying;
    public static bool isGamePaused;

    public GameTimer _gameTimer;
    public MissionController _missionController;
    public PlayerController _playerController;

    void Awake()
    {
        _playerController.anim.SetTrigger("TPose");
    }

    void Update()
    {
        if (isGamePlaying && !isGamePaused)
        {
            if (_gameTimer.timeLeft > 0) _gameTimer.UpdateTimer();
            else _playerController._statisticsController.TakeOxygen(5);
        }
    }

    public void StartRun()
    {
        //Check if everything is setted
        if (!MapPicker.isMapPicked)
        {
            SpawnPopUp.instance.AttentionPopUp("Map is not picked", 0);
            return;
        }

        if (_missionController._fuelController.CheckIfEnoughFuel(MissionInfo.instance.requiredFuel))
            _missionController._fuelController.UseFuel(MissionInfo.instance.requiredFuel);
        else return;

        isGamePlaying = true;
        isGamePaused = false;

        _gameTimer.SetNewTimer(1000000);
        _missionController.unityEvent.Invoke();
    }

    public void EndRun()
    {
        isGamePlaying = false;
        isGamePaused = false;

        _playerController.anim.SetTrigger("TPose");

        //Set new player position
        Transform player = GameObject.Find("Player").transform;
        player.position = new Vector2(0, 1102);
        player.rotation = Quaternion.identity;
        StatisticsController _statisticsController = player.GetComponent<PlayerController>()._statisticsController;
        _statisticsController.TakeOxygen(-(100 - _statisticsController.oxygen), true);
        Physics.SyncTransforms();

        //Setting everything to normal
        _missionController.ResetMap();
    }

    public void EndGame()
    {
        EndRun();
        SceneManager.LoadScene(0);
    }
}
