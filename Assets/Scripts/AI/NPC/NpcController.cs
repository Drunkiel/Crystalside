using UnityEngine;

public class NpcController : MonoBehaviour
{
    public bool isNPCStopped;
    public float speed;
    [SerializeField] private float cooldown;
    [SerializeField] private bool onTheGround;
    public LayerMask layerMask;
    private Vector3 newPosition;

    public Rigidbody rgBody;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = GetNewPosition();
    }

    // Update is called once per frame
    void Update()
    {
        onTheGround = Physics.Raycast(transform.position, Vector3.down, 3.5f, layerMask);

        float distance = Vector3.Distance(transform.position, newPosition);
        if (distance <= 3)
        {
            if (cooldown <= 0)
            {
                newPosition = GetNewPosition();
                cooldown = Random.Range(5, 10);
            }
            else
            {
                StopNPC();
                cooldown -= Time.deltaTime;
            }
        }

        if (!isNPCStopped && onTheGround && rgBody.velocity.x < 0.5f && rgBody.velocity.z < 0.5f) JumpNPC();
    }

    private void FixedUpdate()
    {
        if (!isNPCStopped) MoveNPC();
    }

    private void MoveNPC()
    {
        //Rotate to new position
        Vector3 direction = newPosition - transform.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

        //Move forward
        Vector3 movement = transform.forward * speed * Time.deltaTime;
        rgBody.AddForce(movement);
    }

    private void JumpNPC()
    {
        if (rgBody.velocity.y < 3) rgBody.AddForce(Vector3.up * speed);
    }

    private Vector3 GetNewPosition()
    {
        float x = transform.position.x + Random.Range(0f, 30f);
        float z = transform.position.x + Random.Range(0f, 30f);

        RaycastHit hit;
        if (Physics.Raycast(new Vector3(x, 100, z), transform.TransformDirection(Vector3.down), out hit, 150f, layerMask))
        {
            isNPCStopped = false;
            return new Vector3(hit.point.x, hit.point.y + 2, hit.point.z);
        }

        return transform.position;
    }

    private void StopNPC()
    {
        isNPCStopped = true;
        rgBody.velocity = Vector3.zero;
    }
}
