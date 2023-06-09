using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity;
    private float xRotation;

    public float pickupDistance;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        ChangeLockState(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotating
        transform.rotation = player.rotation;

        if (!PlayerController.isPlayerStopped) Rotate();

        //Looking for objects
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, pickupDistance, layerMask))
        {
            if (hit.transform.TryGetComponent(out PickupItem _pickupItem))
            {
                CursorController.instance.ChangeCursor(1);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _pickupItem.Pickup();
                }
            }

            if (hit.transform.TryGetComponent(out UIController _UIController))
            {
                CursorController.instance.ChangeCursor(2);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _UIController.OpenCloseUI();
                }
            }
        }
        else CursorController.instance.ChangeCursor(0);
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
        xRotation = Mathf.Clamp(xRotation, -40f, 90f);

        transform.Rotate(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}