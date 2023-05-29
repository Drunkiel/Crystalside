using UnityEngine;

public enum MapDangerLevel
{
    Easy,
    Medium,
    Hard,
    EpicHard
};

[System.Serializable]
public class Distance
{
    public int minDistance; //1l.y => 100l
    public int maxDistance; //3l.y => 300l
}

[CreateAssetMenu()]
public class NewMapData : ScriptableObject
{
    public Sprite mapImage;
    public string mapName;
    public Distance distance;
    public MapDangerLevel mapDangerLevel;

    public NoiseData _noiseData;
    public TerrainData _terrainData;
    public TextureData _textureData;
    public ObjectsData _objectsData;
}
