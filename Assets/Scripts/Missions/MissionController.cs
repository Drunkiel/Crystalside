using UnityEngine;
using UnityEngine.Events;

public class MissionController : MonoBehaviour
{
    public MapPicker _mapPicker;
    public FuelController _fuelController;

    public UnityEvent unityEvent;

    void Start()
    {
        _mapPicker.SetMapsInCards();
    }

    public void PickMap(int i)
    {
        MissionInfo.instance.mapName = _mapPicker._mapDatas[i].mapName;
        MissionInfo.instance.dangerLevel = _mapPicker._mapDatas[i].mapDangerLevel.ToString();
        MissionInfo.instance.destinationDistance = Random.Range(_mapPicker._mapDatas[i].distance.minDistance, _mapPicker._mapDatas[i].distance.maxDistance + 1);
        MissionInfo.instance.requiredFuel = (MissionInfo.instance.destinationDistance * _fuelController.fuelConsumption);
        MissionInfo.instance.UpdateTexts();
        _mapPicker.PickMap(i);
    }

    public void ResetMap()
    {
        MissionInfo.instance.mapName = "None";
        MissionInfo.instance.dangerLevel = "None";
        MissionInfo.instance.destinationDistance = 0;
        MissionInfo.instance.requiredFuel = 0;
        MissionInfo.instance.UpdateTexts();
        _mapPicker.SetMapsInCards();
    }
}
