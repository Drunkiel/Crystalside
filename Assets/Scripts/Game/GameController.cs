using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isGamePlaying;
    public static bool isGamePaused;
    public float timeLeft;

    public MissionController _missionController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        isGamePlaying = true;
        isGamePaused = false;
        //Check if something is not checked
        for (int i = 0; i < _missionController.toggles.Length; i++)
        {
            if (!_missionController.toggles[i].isOn) return;
        }

        _missionController.unityEvent.Invoke();
    }

    public void EndGame()
    {
        isGamePlaying = false;
        isGamePaused = false;

        //Set new player position
        Transform player = GameObject.Find("Player").transform;
        player.position = new Vector2(0, 1110);
        player.rotation = Quaternion.identity;

        //Setting everything to normal
        for (int i = 0; i < _missionController.toggles.Length; i++)
        {
            _missionController.toggles[i].isOn = false;
            _missionController._mapPicker.SetMapsInCards();
        }
    }

    public void SetNewStoper()
    {

    }
}
