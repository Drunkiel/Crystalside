using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isGamePlaying;
    public static bool isGamePaused;

    public MissionController _missionController;

    public void StartGame()
    {
        //Check if everything is alright
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

        _missionController.unityEvent.Invoke();
    }

    public void EndGame()
    {
        isGamePlaying = false;
        isGamePaused = false;

        //Set new player position
        Transform player = GameObject.Find("Player").transform;
        player.position = new Vector2(0, 1102);
        player.rotation = Quaternion.identity;
        Physics.SyncTransforms();

        //Setting everything to normal
        _missionController.ResetMap();
    }
}
