using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform playerArms;
    public Transform virtualCamera;
    public float mouseSensitivity;
    public static float xRotation;
    private float minXRotation = -40f;
    private float maxXRotation = 90f;
    [SerializeField] private Animator playerAnim;

    public float interactionDistance;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        ChangeLockState(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotating virtual camera
        virtualCamera.rotation = player.rotation;
        virtualCamera.Rotate(xRotation, 0f, 0f);
        playerArms.rotation = virtualCamera.rotation;

        //Rotate Player
        if (!PlayerController.isPlayerStopped) Rotate();

        //Looking for objects
        LookForInteraction();
    }

    public static void ChangeLockState(bool toBlock)
    {
        PlayerController.isPlayerStopped = toBlock;

        if (toBlock) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 3 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 3 * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);
        player.Rotate(Vector3.up * mouseX);
    }

    private void LookForInteraction()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionDistance, layerMask))
        {
            if (hit.transform.TryGetComponent(out PickupItem _pickupItem))
            {
                CursorController.instance.ChangeCursor(1);

                if (Input.GetKeyDown(KeyCode.F)) _pickupItem.Pickup();
            }

            if (hit.transform.TryGetComponent(out UIController _UIController))
            {
                CursorController.instance.ChangeCursor(2);

                if (Input.GetKeyDown(KeyCode.F)) _UIController.OpenCloseUI();
            }

            if (hit.transform.TryGetComponent(out DialogHolder _dialogHolder))
            {
                CursorController.instance.ChangeCursor(2);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    GameObject dialogMenu = GameObject.FindGameObjectWithTag("DialogMenu");
                    dialogMenu.GetComponent<DialogController>()._npcController = _dialogHolder.GetComponent<NpcController>();
                    dialogMenu.GetComponent<DialogController>().StartDialog(_dialogHolder._dialogData);
                }
            }
        }
        else CursorController.instance.ChangeCursor(0);
    }
}