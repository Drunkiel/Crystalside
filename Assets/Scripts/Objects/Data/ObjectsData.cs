using UnityEngine;

[CreateAssetMenu()]
public class ObjectsData : ScriptableObject
{
    public Object[] objects;
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