using System.Collections.Generic;
using UnityEngine;

public class SpawnBones : MonoBehaviour
{
    public GameObject bonePrefab;
    private int numberOfBones;
    private int boneRotation;
    public LayerMask layerMask;

    List<Transform> boneTransforms = new List<Transform>();

    private void Start()
    {
        numberOfBones = Random.Range(3, 8);
        bool orTurn = false;
        if (Random.Range(0, 2) == 0) orTurn = true;

        bool turnLeft = false;
        if (orTurn && Random.Range(0, 2) == 0) turnLeft = true;

        Vector3 spawnPosition = transform.position;
        Quaternion mainRotation = transform.rotation;

        for (int i = 0; i < numberOfBones; i++)
        {
            spawnPosition += mainRotation * Vector3.back * 40;
            mainRotation = Quaternion.Euler(0, 10 * (turnLeft ? 1 : -1), 0) * mainRotation;
            SpawnSkeletonBones(orTurn, turnLeft, spawnPosition, mainRotation);
        }

        Destroy(GetComponent<SpawnBones>());
    }

    private void SpawnSkeletonBones(bool orTurn, bool turnLeft, Vector3 spawnPosition, Quaternion mainRotation)
    {
        if (orTurn)
        {
            if (turnLeft) boneRotation -= 10 - boneTransforms.Count;
            else boneRotation += 10 - boneTransforms.Count;
        }

        RaycastHit hit;
        Physics.Raycast(new Vector3(spawnPosition.x, 100, spawnPosition.z), transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask);

        Quaternion boneRotationQuaternion = Quaternion.Euler(-90, 0, mainRotation.eulerAngles.y - boneRotation);
        GameObject newBone = Instantiate(bonePrefab, new Vector3(spawnPosition.x, hit.point.y - Random.Range(0, 6), spawnPosition.z), boneRotationQuaternion, transform);
        boneTransforms.Add(newBone.transform);
    }
}
