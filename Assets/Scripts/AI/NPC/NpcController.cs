using UnityEngine;

public class NpcController : MonoBehaviour
{
    public bool isNPCStopped;
    public float speed;
    public float cooldown;
    public LayerMask layerMask;
    public Vector3 newPosition;
    public GameObject test;

    public Rigidbody rgBody;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = GetNewPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isNPCStopped) MoveNPC();
        if(Vector3.Distance(transform.position, newPosition) <= 3)
        {
            if (cooldown <= 0)
            {
                newPosition = GetNewPosition();
                cooldown = Random.Range(5, 10);
            }
            else cooldown -= Time.deltaTime;
        }
    }

    private void MoveNPC()
    {
        rgBody.velocity = Vector3.ClampMagnitude(rgBody.velocity, speed);
        rgBody.AddForce((newPosition - transform.position) * speed * Time.deltaTime);
    }

    private Vector3 GetNewPosition()
    {
        float x = transform.position.x + Random.Range(0f, 30f);
        float z = transform.position.x + Random.Range(0f, 30f);

        RaycastHit hit;
        if (Physics.Raycast(new Vector3(x, 100, z), transform.TransformDirection(Vector3.down), out hit, 150f, layerMask))
        {
            Instantiate(test, hit.point, Quaternion.identity);
            return new Vector3(hit.point.x, hit.point.y + 2, hit.point.z);
        }
        else GetNewPosition();

        return transform.position;
    }
}
