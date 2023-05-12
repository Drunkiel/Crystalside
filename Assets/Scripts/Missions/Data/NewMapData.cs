using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class NewMapData : ScriptableObject
{
    public Sprite mapImage;
    public string mapName;
    public string mapDescription;

    public NoiseData _noiseData;
    public TerrainData _terrainData;
    public TextureData _textureData;
    public ObjectsData _objectsData;
}
