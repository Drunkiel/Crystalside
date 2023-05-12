using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public ObjectsData _objectsData;
    public Transform parent;

    public LayerMask layerMask;

    private const int mapSize = 450;

    public void SpawnRandomObjects()
    {
        DestroyObjectsIfExists();

        int numberOfObjects = Random.Range(10, Mathf.RoundToInt(mapSize - mapSize * 0.2f));

        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 pickedPosition = RandomPosition();
            Instantiate(PickObjectBasedOnHeight(pickedPosition.y), pickedPosition, Quaternion.Euler(0f, Random.Range(0, 359f), 0f), parent);
        }
    }

    private void DestroyObjectsIfExists()
    {
        if (parent.childCount > 0)
        {
            List<GameObject> a = new List<GameObject>();

            for (int i = 0; i < parent.childCount; i++)
            {
                a.Add(parent.GetChild(i).gameObject);
            }

            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(a[i]);
            }
        }
    }

    private Vector3 RandomPosition()
    {
        float x = Random.Range(-mapSize, mapSize + 1);
        float z = Random.Range(-mapSize, mapSize + 1);

        RaycastHit hit;
        Physics.Raycast(new Vector3(x, 100, z), transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask);

        return new Vector3(x, 100 - hit.distance - 0.1f, z);
    }

    private GameObject PickObjectBasedOnHeight(float height)
    {
        List<GameObject> objects = new List<GameObject>();
        objects.Clear();

        for (int i = 0; i < _objectsData.heightRanges.Length; i++)
        {
            if (height > _objectsData.heightRanges[i].minHeight && height <= _objectsData.heightRanges[i].maxHeight) objects.Add(_objectsData.prefabs[i]);
        }

        return objects[Random.Range(0, objects.Count)];
    }
}
