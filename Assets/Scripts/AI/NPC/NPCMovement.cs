using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float cooldown;
    private float stuckCooldown = 5f;
    [SerializeField] private bool onTheGround;
    public LayerMask layerMask;
    private Vector3 newPosition;
    private Transform transformToMoveAround;

    [SerializeField] private NpcController _npcController;
    [SerializeField] private Rigidbody rgBody;

    // Start is called before the first frame update
    void Start()
    {
        if (_npcController._info.npcRole == Role.Priest) transformToMoveAround = transform;
        else transformToMoveAround = GameObject.Find("NPC_Priest").transform;

        newPosition = GetNewPosition();
    }

    // Update is called once per frame
    void Update()
    {
        onTheGround = Physics.Raycast(transform.position, Vector3.down, 2.2f, layerMask);

        if (!_npcController.isNPCTalking)
        {
            float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(newPosition.x, 0, newPosition.z));
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
            else if (distance > 3)
            {
                _npcController.isNPCStopped = false;
                StuckNPC();
            }

            if (!_npcController.isNPCStopped)
            {
                if (onTheGround && rgBody.velocity.x < 0.5f && rgBody.velocity.z < 0.5f) JumpNPC();
                if (!onTheGround && distance > 20 && rgBody.velocity.x <= 0.1f && rgBody.velocity.z <= 0.1f) GetNewPosition();
            }
        }

        if (_npcController.isNPCTalking) RotateTo(transform, GameObject.Find("Player").transform.position);
    }

    private void FixedUpdate()
    {
        if (!_npcController.isNPCStopped && !_npcController.isNPCTalking) MoveNPC();
    }

    private void MoveNPC()
    {
        //Rotate to new position
        RotateTo(transform, newPosition);

        //Move forward
        Vector3 movement = transform.forward * _npcController._info.speed * Time.deltaTime;
        rgBody.AddForce(movement);
    }

    public void RotateTo(Transform transformToRotate, Vector3 positionToRotate)
    {
        Vector3 direction = positionToRotate - transformToRotate.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transformToRotate.rotation = Quaternion.Slerp(transformToRotate.rotation, targetRotation, Time.deltaTime);
    }

    private void JumpNPC()
    {
        if (rgBody.velocity.y < 3) rgBody.AddForce(Vector3.up * _npcController._info.speed);
    }

    private Vector3 GetNewPosition()
    {
        float x = transformToMoveAround.position.x + Random.Range(-20f, 20f);
        float z = transformToMoveAround.position.z + Random.Range(-20f, 20f);

        RaycastHit hit;
        if (Physics.Raycast(new Vector3(x, 100, z), transformToMoveAround.TransformDirection(Vector3.down), out hit, 150f, layerMask))
        {
            _npcController.isNPCStopped = false;
            return hit.point;
        }

        return transformToMoveAround.position;
    }

    private void StuckNPC()
    {
        if (rgBody.velocity.x < 0.3f && rgBody.velocity.z < 0.3f)
        {
            if (stuckCooldown <= 0) GetNewPosition();
            else stuckCooldown -= Time.deltaTime;
        }
    }

    public void StopNPC()
    {
        _npcController.isNPCStopped = true;
        rgBody.velocity = new Vector3(0, rgBody.velocity.y, 0);
    }
}
