using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool isPlayerStopped;
    public StatisticsController _statisticsController;

    [Header("Movement")]
    public float moveSpeed;
    public float sprintSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    [SerializeField] private bool grounded;

    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    [SerializeField] private Rigidbody rgBody;
    public Animator anim;

    private void Start()
    {
        readyToJump = true;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        if (!isPlayerStopped) MyInput();
        SpeedControl();

        // handle drag
        if (grounded) rgBody.drag = groundDrag;
        else rgBody.drag = 0;
    }

    private void FixedUpdate()
    {
        if (!isPlayerStopped) MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if ((horizontalInput != 0 || verticalInput != 0) && grounded) anim.SetBool("Walk", true);
        else anim.SetBool("Walk", false);

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded) rgBody.AddForce(moveDirection.normalized * Speed() * 10f, ForceMode.Force);
        else if (!grounded) rgBody.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rgBody.velocity.x, 0f, rgBody.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > Speed())
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rgBody.velocity = new Vector3(limitedVel.x, rgBody.velocity.y, limitedVel.z);
        }
    }

    private float Speed()
    {
        if (Input.GetKey(sprintKey)) return sprintSpeed;
        else return moveSpeed;
    }

    private void Jump()
    {
        // reset y velocity
        rgBody.velocity = new Vector3(rgBody.velocity.x, 0f, rgBody.velocity.z);

        rgBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}