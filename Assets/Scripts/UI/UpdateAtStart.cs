using UnityEngine;
using UnityEngine.Events;

public class UpdateAtStart : MonoBehaviour
{
    public UnityEvent unityEvent;

    // Start is called before the first frame update
    void Start()
    {
        unityEvent.Invoke();
    }
}
