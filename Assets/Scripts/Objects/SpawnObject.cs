using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public ObjectsData _objectsData;
    public Transform parent;

    public LayerMask layerMask;

    private const int mapSize = 600;
    private float objectSize = 1;

    public int numberOfObjects;
    private int pickedObject;

    public void SpawnRandomObjects()
    {
        //Destroy old objects
        DestroyObjectsIfExists();

        numberOfObjects = SetNumberOfObjects();

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

    private int SetNumberOfObjects()
    {
        int seed = MapGenerator.seed;
        int[] numbers = new int[9];

        for (int i = 0; i < 9; i++)
        {
            numbers[i] = seed % 10;
            seed /= 10; 
        }

        int allObjectsToSpawn = 0;

        foreach (var biome in _objectsData.biomes)
        {
            biome.spawnableObjects = (numbers[biome.defineObjectsSeed[0]] + numbers[biome.defineObjectsSeed[1]] + numbers[biome.defineObjectsSeed[2]]) * numbers[numbers.Length - 1];
            allObjectsToSpawn += biome.spawnableObjects;
        }

        return allObjectsToSpawn;
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
        if (!Physics.Raycast(new Vector3(x, 100, z), transform.TransformDirection(Vector3.down), out hit, 150f, layerMask)
            && Vector3.Distance(hit.point, Vector3.zero) > mapSize) return RandomPosition();
        for (int i = 0; i < _objectsData.biomes.Length; i++)
        {
            if (!(hit.point.y > _objectsData.biomes[i].minHeight && hit.point.y <= _objectsData.biomes[i].maxHeight) && _objectsData.biomes[i].spawnableObjects > 0) return RandomPosition();
            else _objectsData.biomes[i].spawnableObjects -= 1;
        }

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