using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    public MapPicker _mapPicker;

    public Toggle[] toggles;
    public UnityEvent unityEvent;

    void Start()
    {
        _mapPicker.SetMapsInCards();
    }

    public void PickMap(int i)
    {
        _mapPicker.PickMap(i);
        toggles[0].isOn = true;
    }

    public void StartGame()
    {
        //Check if something is not checked
        for (int i = 0; i < toggles.Length; i++)
        {
            if (!toggles[i].isOn) return;
        }

        unityEvent.Invoke();
    }

    public void EndGame()
    {
        //Set new player position
        Transform player = GameObject.Find("Player").transform;
        player.position = new Vector2(0, 1110);
        player.rotation = Quaternion.identity;

        //Setting everything to normal
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].isOn = false;
            _mapPicker.SetMapsInCards();
        }
    }
}
