using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public ObjectsData _objectsData;

    public LayerMask layerMask;

    private const int mapSize = 660;
    private float objectSize = 1;

    public int numberOfObjects;
    private int pickedObject;

    #region Spawning random objects

    public void SpawnRandomObjects()
    {
        //Destroy old objects
        DestroyObjectsIfExists();

        //Setting new number of objects
        numberOfObjects = SetNumberOfObjects();

        //Spawning new objects
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 pickedPosition = GetPosition();
            GameObject newObject = PickObjectBasedOnHeight(pickedPosition.y);
            if (newObject != null)
            {
                newObject.transform.localScale = new Vector3(objectSize, objectSize, objectSize);
                Instantiate(newObject, pickedPosition, NewRotation(_objectsData.objects[pickedObject].rotateZ), transform);
            }
            else numberOfObjects -= 1;
        }
    }

    private void DestroyObjectsIfExists()
    {
        if (transform.childCount > 0)
        {
            List<GameObject> allObjects = new List<GameObject>();

            for (int i = 0; i < transform.childCount; i++)
            {
                allObjects.Add(transform.GetChild(i).gameObject);
            }

            foreach (GameObject singleObject in allObjects)
            {
                Destroy(singleObject);
            }
        }
    }

    private int SetNumberOfObjects()
    {
        int allObjectsToSpawn = 0;

        foreach (var biome in _objectsData.biomes)
        {
            biome.numberOfObjectsToSpawn = Random.Range(biome.minObjectsToSpawn, biome.minObjectsToSpawn * 4);
            allObjectsToSpawn += biome.numberOfObjectsToSpawn;
        }

        return allObjectsToSpawn;
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

    #endregion

    #region Spawning Structures

    public void SpawnStructures()
    {
        for (int i = 0; i < _objectsData.structures.Length; i++)
        {
            Vector3 pickedPosition = GetPosition(false);
            Instantiate(_objectsData.structures[i].prefab, pickedPosition, NewRotation(), transform);
        }
    }

    #endregion

    private Vector3 GetPosition(bool isRandom = true)
    {
        //Random position
        float x = 0;
        float z = 0;

        if (isRandom)
        {
            x = Random.Range(-mapSize, mapSize + 1);
            z = Random.Range(-mapSize, mapSize + 1);
        }

        //Send raycast
        RaycastHit hit;
        if (!Physics.Raycast(new Vector3(x, 100, z), transform.TransformDirection(Vector3.down), out hit, 150f, layerMask)
            || Vector3.Distance(hit.point, Vector3.zero) > mapSize) return GetPosition(isRandom);

        return new Vector3(x, 100 - hit.distance - 0.1f, z);
    }

    private Quaternion NewRotation(bool rotateZ = false)
    {
        if (rotateZ) return Quaternion.Euler(-90f, Random.Range(0, 359f), 0f);

        return Quaternion.Euler(0f, Random.Range(0, 359f), 0f);
    }
}