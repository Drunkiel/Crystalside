using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public bool isTriggered;
    public string tagName = "";

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(tagName))
        {
            isTriggered = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag(tagName))
        {
            isTriggered = false;
        }
    }
}