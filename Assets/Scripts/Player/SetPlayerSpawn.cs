using UnityEngine;

public class SetPlayerSpawn : MonoBehaviour
{
    public GameObject ship;
    public Transform player;
    private int distance = 300;

    public LayerMask layerMask;

    // Start is called before the first frame update
    void Awake()
    {
        Vector3 position = PickRandomLocation();
        Quaternion rotation = SetRotation(position);

        ship.transform.position = position;
        ship.transform.rotation = rotation;

        player.position = ship.transform.GetChild(0).position;
        Physics.SyncTransforms();
    }

    private Vector3 PickRandomLocation()
    {
        bool sideX()
        {
            int a = Random.Range(0, 2);
            if (a == 0) return false;
            else return true;
        }

        int newSidePos = Random.Range(0, distance);
        int multiplier()
        {
            int a = Random.Range(0, 2);
            if (a == 0) return -1;
            else return 1;
        }

        RaycastHit hit;

        if (sideX())
        {
            Physics.Raycast(new Vector3(500 * multiplier(), 2, newSidePos * multiplier()), transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask);
            return new Vector3(hit.point.x, 3.5f, 0);
        }
        else
        {
            Physics.Raycast(new Vector3(newSidePos * multiplier(), 2, 500 * multiplier()), transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask);
            return new Vector3(0, 3.5f, hit.point.z);
        }
    }

    private Quaternion SetRotation(Vector3 position)
    {
        int y = 0;

        if (position.x < 0 && position.x != 0) y = 90;
        else if(position.x != 0) y = -90;

        if (position.z < 0) y = 0;
        else if (position.z != 0) y = 180;

        return Quaternion.Euler(0, y, 0);
    }
}
