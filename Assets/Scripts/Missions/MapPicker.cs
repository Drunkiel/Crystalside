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

        //Picking some of the maps
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
/*            cards[i].transform.GetChild(3).GetComponent<TMP_Text>().text = _mapDatas[i].mapInformations;*/

            switch (_mapDatas[i].mapDangerLevel)
            {
                case MapDangerLevel.Easy:
                    cards[i].transform.GetChild(4).GetChild(0).GetComponent<Image>().color = new Color32(111, 255, 75, 255);
                    break;
                case MapDangerLevel.Medium:
                    cards[i].transform.GetChild(4).GetChild(0).GetComponent<Image>().color = new Color32(255, 220, 75, 255);
                    break;
                case MapDangerLevel.Hard:
                    cards[i].transform.GetChild(4).GetChild(0).GetComponent<Image>().color = new Color32(255, 90, 75, 255);
                    break;
                case MapDangerLevel.EpicHard:
                    cards[i].transform.GetChild(4).GetChild(0).GetComponent<Image>().color = new Color32(255, 75, 150, 255);
                    break;
            }
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

        _mapGenerator.noiseData = _mapDatas[i]._noiseData;
        _mapGenerator.terrainData = _mapDatas[i]._terrainData;
        _mapGenerator.textureData = _mapDatas[i]._textureData;
        _spawnObject._objectsData = _mapDatas[i]._objectsData;

        CardsActivity(false);
        isMapPicked = true;
        _mapGenerator.DrawMapInEditor();
        _spawnObject.SpawnRandomObjects();
    }
}
