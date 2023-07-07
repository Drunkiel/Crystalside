using UnityEngine;

[CreateAssetMenu()]
public class ObjectsData : ScriptableObject
{
    public Object[] objects;
    public Biomes[] biomes;
    public Structure[] structures;
}

[System.Serializable]
public class Object
{
    public int id;
    public GameObject prefab;
    public float[] spawnHeight = new float[2];
    public float[] spawnSize = new float[2];
    public bool rotateZ;
}

[System.Serializable]
public class Biomes
{
    public string name;
    public float minHeight;
    public float maxHeight;
    public int[] defineObjectsSeed = new int[3];
    public int spawnableObjects;
}

[System.Serializable]
public class Structure
{
    public int id;
    public GameObject prefab;
    public float[] spawnHeight = new float[2];
}