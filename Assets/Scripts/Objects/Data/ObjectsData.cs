using UnityEngine;

[CreateAssetMenu()]
public class ObjectsData : ScriptableObject
{
    public GameObject[] prefabs;
    public HeightRange[] heightRanges;
}

[System.Serializable]
public class HeightRange
{
    public float minHeight;
    public float maxHeight;
}