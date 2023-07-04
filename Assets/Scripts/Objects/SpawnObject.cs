using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public ObjectsData _objectsData;
    public Transform parent;

    public LayerMask layerMask;

    private const int mapSize = 660;
    private float objectSize = 1;

    private int pickedObject;

    public void SpawnRandomObjects()
    {
        //Destroy old objects
        DestroyObjectsIfExists();

        int numberOfObjects = int.Parse(MapGenerator.seed.ToString().Substring(0, 4)) / int.Parse(MapGenerator.seed.ToString().Substring(8, 1));

        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 pickedPosition = RandomPosition();
            GameObject newObject = PickObjectBasedOnHeight(pickedPosition.y);
            if (newObject != null)
            {
                newObject.transform.localScale = new Vector3(objectSize, objectSize, objectSize);
                Instantiate(newObject, pickedPosition, RandomRotation(_objectsData.objects[pickedObject].rotateZ), parent);
            }
        }
    }

    private void DestroyObjectsIfExists()
    {
        if (parent.childCount > 0)
        {
            List<GameObject> allObjects = new List<GameObject>();

            for (int i = 0; i < parent.childCount; i++)
            {
                allObjects.Add(parent.GetChild(i).gameObject);
            }

            foreach (GameObject singleObject in allObjects)
            {
                Destroy(singleObject);
            }
        }
    }

    private Vector3 RandomPosition()
    {
        float x = Random.Range(-mapSize, mapSize + 1);
        float z = Random.Range(-mapSize, mapSize + 1);

        RaycastHit hit;
        if (!Physics.Raycast(new Vector3(x, 100, z), transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask)) return RandomPosition();
        if (Vector3.Distance(hit.point, Vector3.zero) > mapSize) return RandomPosition();

        return new Vector3(x, 100 - hit.distance - 0.1f, z);
    }

    private Quaternion RandomRotation(bool rotateZ)
    {
        if (rotateZ) return Quaternion.Euler(-90f, Random.Range(0, 359f), 0f);

        return Quaternion.Euler(0f, Random.Range(0, 359f), 0f);
    }

    private GameObject PickObjectBasedOnHeight(float height)
    {
        List<int> objectsID = new List<int>();
        objectsID.Clear();

        for (int i = 0; i < _objectsData.objects.Length; i++)
        {
            if (height > _objectsData.objects[i].spawnHeight[0] && height <= _objectsData.objects[i].spawnHeight[1]) objectsID.Add(_objectsData.objects[i].id);
        }

        if (objectsID.Count <= 0) return null;

        pickedObject = objectsID[Random.Range(0, objectsID.Count)];

        if (_objectsData.objects[pickedObject].spawnSize[0] != _objectsData.objects[pickedObject].spawnSize[1])
            objectSize = Random.Range(_objectsData.objects[pickedObject].spawnSize[0], _objectsData.objects[pickedObject].spawnSize[1]);
        return _objectsData.objects[pickedObject].prefab;
    }
}