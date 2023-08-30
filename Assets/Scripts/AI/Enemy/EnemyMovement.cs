using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [HideInInspector] public float distanceToAttack;
    [HideInInspector] public float distanceToDestination;
    public bool onTheGround;
    public LayerMask layerMask;
    private Vector3 newPosition;

    public EnemyController _enemyController;
    public Rigidbody rgBody;

    // Start is called before the first frame update
    public virtual void Start()
    {
        newPosition = GetNewPosition();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        onTheGround = Physics.Raycast(transform.position, Vector3.down, 1.8f, layerMask);
        distanceToDestination = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(newPosition.x, 0, newPosition.z));

        if (_enemyController.isPatrolling)
        {
            if (distanceToDestination <= 3)
            {
                if (cooldown <= 0)
                {
                    newPosition = GetNewPosition();
                    cooldown = Random.Range(5, 10);
                }
                else
                {
                    cooldown -= Time.deltaTime;
                }
            }

            if (onTheGround && rgBody.velocity.x < 0.5f && rgBody.velocity.z < 0.5f) JumpNPC();
        }

        if (_enemyController.isPlayerInRadius)
        {
            if (cooldown <= 0 && distanceToDestination < distanceToAttack)
            {
                Attack();
                cooldown = Random.Range(0.5f, 2);
            }
            else cooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (_enemyController.isPatrolling) MoveNPC();
        if (_enemyController.isPlayerInRadius) GetDistanceToPlayer();
    }

    private Vector3 GetNewPosition()
    {
        float x = transform.position.x + Random.Range(-20f, 20f);
        float z = transform.position.z + Random.Range(-20f, 20f);

        RaycastHit hit;
        if (Physics.Raycast(new Vector3(x, 100, z), transform.TransformDirection(Vector3.down), out hit, 150f, layerMask))
        {
            return hit.point;
        }

        return transform.position;
    }

    public void MoveNPC(float additionalForce = 0f)
    {
        //Rotate to new position
        RotateTo(transform, newPosition);

        //Move forward
        Vector3 movement = transform.forward * (_enemyController._info.speed + additionalForce) * Time.deltaTime;
        rgBody.AddForce(movement);
    }

    public void JumpNPC(float additionalForce = 0f)
    {
        if (rgBody.velocity.y < 3) rgBody.AddForce(Vector3.up * (_enemyController._info.speed + additionalForce));
    }

    public void RotateTo(Transform transformToRotate, Vector3 positionToRotate)
    {
        Vector3 direction = positionToRotate - transformToRotate.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transformToRotate.rotation = Quaternion.Slerp(transformToRotate.rotation, targetRotation, Time.deltaTime);
    }

    public virtual void Attack()
    {

    }

    public virtual void GetDistanceToPlayer()
    {
        newPosition = GameObject.Find("Player").transform.position;
        RotateTo(transform, newPosition);
    }
}
