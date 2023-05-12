using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public int timeToDestroy;

    public void TimeDestroy()
    {
        Destroy(this.gameObject, timeToDestroy);
    }
}
