using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        MoveCameraToPlayer();
    }

    private void MoveCameraToPlayer()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
