using UnityEngine;

public class SetPlayerSpawn : MonoBehaviour
{
    public GameObject ship;
    public Transform player;
    private int distance = 300;

    public LayerMask layerMask;

    private Vector3 PickRandomLocation()
    {
        bool sideX()
        {
            if (Random.Range(0, 2) == 0) return false;
            else return true;
        }

        int newSidePos = Random.Range(0, distance);
        int multiplier()
        {
            if (Random.Range(0, 2) == 0) return -1;
            else return 1;
        }

        RaycastHit hit;

        if (sideX())
        {
            Physics.Raycast(new Vector3(500 * multiplier(), 100, 0), transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask);
            return new Vector3(hit.point.x, hit.point.y + 3.5f, 0);
        }
        else
        {
            Physics.Raycast(new Vector3(0, 100, 500 * multiplier()), transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask);
            return new Vector3(0, hit.point.y + 3.5f, hit.point.z);
        }
    }

    private Quaternion SetRotation(Vector3 position)
    {
        int rotationY = 0;

        if (position.x < 0 && position.x != 0) rotationY = 90;
        else if(position.x != 0) rotationY = -90;

        if (position.z < 0) rotationY = 0;
        else if (position.z != 0) rotationY = 180;

        return Quaternion.Euler(0, rotationY, 0);
    }

    public void SetNewLocation()
    {
        Vector3 position = PickRandomLocation();
        Quaternion rotation = SetRotation(position);

        ship.transform.position = position;
        ship.transform.rotation = rotation;

        player.position = ship.transform.GetChild(0).position;
        player.rotation = rotation;
    }
}
