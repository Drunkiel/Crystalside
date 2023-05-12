using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    public GameObject[] cards;

    public NewMapData[] _mapDatas;
    List<int> pickedMaps = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        SetMapsInCards();
    }

    private List<int> DrawMaps()
    {
        List<int> newPickedMaps = new List<int>();

        for (int i = 0; i < cards.Length; i++)
        {
            int randomNumber = Random.Range(0, _mapDatas.Length);
            if (!newPickedMaps.Contains(randomNumber)) newPickedMaps.Add(randomNumber);
            else DrawMaps();
        }

        return newPickedMaps;
    }

    public void SetMapsInCards()
    {
        pickedMaps = DrawMaps();

        for (int i = 0; i < pickedMaps.Count; i++)
        {
            cards[i].transform.GetChild(1).GetComponent<Image>().sprite = _mapDatas[i].mapImage;
            cards[i].transform.GetChild(2).GetComponent<TMP_Text>().text = _mapDatas[i].mapName;
            cards[i].transform.GetChild(3).GetComponent<TMP_Text>().text = _mapDatas[i].mapDescription;
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

        _mapGenerator.DrawMapInEditor();
        _spawnObject.SpawnRandomObjects();
    }
}
