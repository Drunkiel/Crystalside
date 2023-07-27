using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private bool onTheGround;
    public LayerMask layerMask;
    private Vector3 newPosition;

    private NpcController _npcController;
    private Rigidbody rgBody;

    // Start is called before the first frame update
    void Start()
    {
        rgBody = GetComponent<Rigidbody>();
        _npcController = GetComponent<NpcController>();

        newPosition = GetNewPosition();
    }

    // Update is called once per frame
    void Update()
    {
        onTheGround = Physics.Raycast(transform.position, Vector3.down, 2f, layerMask);

        if (!_npcController.isNPCStopped && !_npcController.isNPCTalking)
        {
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
            else if (distance > 3) _npcController.isNPCStopped = false;

            if (onTheGround && rgBody.velocity.x < 0.5f && rgBody.velocity.z < 0.5f) JumpNPC();
        }

        if (_npcController.isNPCTalking) RotateTo(GameObject.Find("Player").transform.position);
    }

    private void FixedUpdate()
    {
        if (!_npcController.isNPCStopped && !_npcController.isNPCTalking) MoveNPC();
    }

    public void MoveNPC()
    {
        //Rotate to new position
        RotateTo(newPosition);

        //Move forward
        Vector3 movement = transform.forward * _npcController._info.speed * Time.deltaTime;
        rgBody.AddForce(movement);
    }

    private void RotateTo(Vector3 positionToRotate)
    {
        Vector3 direction = positionToRotate - transform.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }

    private void JumpNPC()
    {
        if (rgBody.velocity.y < 3) rgBody.AddForce(Vector3.up * _npcController._info.speed);
    }

    private Vector3 GetNewPosition()
    {
        float x = transform.position.x + Random.Range(0f, 30f);
        float z = transform.position.x + Random.Range(0f, 30f);

        RaycastHit hit;
        if (Physics.Raycast(new Vector3(x, 100, z), transform.TransformDirection(Vector3.down), out hit, 150f, layerMask))
        {
            _npcController.isNPCStopped = false;
            return new Vector3(hit.point.x, hit.point.y + 2, hit.point.z);
        }

        return transform.position;
    }

    public void StopNPC()
    {
        _npcController.isNPCStopped = true;
        rgBody.velocity = new Vector3(0, rgBody.velocity.y, 0);
    }
}
