using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MapPicker
{
    public static bool isMapPicked;

    public GameObject[] cards;

    public NewMapData[] _mapDatas;
    private List<int> pickedMaps = new List<int>();

    private List<int> DrawMaps()
    {
        List<int> newPickedMaps = new List<int>();
        List<int> notPickedMaps = new List<int>();

        //Setting all maps
        for (int i = 0; i < _mapDatas.Length; i++)
        {
            notPickedMaps.Add(i);
        }

        //Picking random maps
        for (int i = 0; i < cards.Length; i++)
        {
            int randomNumber = Random.Range(0, notPickedMaps.Count);

            newPickedMaps.Add(notPickedMaps[randomNumber]);
            notPickedMaps.RemoveAt(randomNumber);
        }

        return newPickedMaps;
    }

    public void SetMapsInCards()
    {
        pickedMaps = DrawMaps();
        CardsActivity(true);

        for (int i = 0; i < pickedMaps.Count; i++)
        {
            cards[i].transform.GetChild(1).GetComponent<Image>().sprite = _mapDatas[i].mapImage;
            cards[i].transform.GetChild(2).GetComponent<TMP_Text>().text = _mapDatas[i].mapName;
            Color32 newColor = GetDangerColor(_mapDatas[i].mapDangerLevel);
            for (int j = 0; j < (int)_mapDatas[i].mapDangerLevel + 1; j++)
            {
                cards[i].transform.GetChild(4).GetChild(2).GetChild(j).GetComponent<Image>().color = newColor;
            }
        }
    }

    private Color32 GetDangerColor(MapDangerLevel dangerLevel)
    {
        switch (dangerLevel)
        {
            case MapDangerLevel.Easy:
                return new Color32(111, 255, 75, 255);
            case MapDangerLevel.Medium:
                return new Color32(255, 220, 75, 255);
            case MapDangerLevel.Hard:
                return new Color32(255, 90, 75, 255);
            case MapDangerLevel.EpicHard:
                return new Color32(255, 75, 150, 255);
            default:
                return Color.white;
        }
    }

    private void CardsActivity(bool isActive)
    {
        for (int i = 0; i < pickedMaps.Count; i++)
        {
            cards[i].transform.GetChild(5).GetComponent<Button>().interactable = isActive;
        }
    }

    public void PickMap(int i)
    {
        GameObject mapGenerator = GameObject.Find("Map Generator");
        MapGenerator _mapGenerator = mapGenerator.GetComponent<MapGenerator>();
        SpawnObject _spawnObject = mapGenerator.GetComponent<SpawnObject>();

        _mapGenerator.selectedMap = _mapDatas[i];
        _spawnObject._objectsData = _mapDatas[i]._objectsData;

        CardsActivity(false);
        isMapPicked = true;
        _mapGenerator.DrawMapInEditor();
        _spawnObject.SpawnRandomObjects();
    }
}
