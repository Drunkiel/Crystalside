using TMPro;
using UnityEngine;

public class MissionInfo : MonoBehaviour
{
    public static MissionInfo instance;

    public string mapName;
    public string dangerLevel;
    public int destinationDistance;
    public int requiredFuel;

    public TMP_Text mapNameText;
    public TMP_Text dangerLevelText;
    public TMP_Text destinationDistanceText;
    public TMP_Text requiredFuelText;

    void Awake()
    {
        instance = this;
    }

    public void UpdateTexts()
    {
        mapNameText.text = mapName;
        dangerLevelText.text = dangerLevel;
        destinationDistanceText.text = destinationDistance + "l.y";
        requiredFuelText.text = requiredFuel + "l";
    }
}
