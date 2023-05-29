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
}
