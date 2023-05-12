using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    private GameObject cursorToDisplay;
    public int cursorNumber;
    public bool onClick;
    public bool onDrag;
    public UnityEvent unityEvent;

    TriggerController _triggerController;

    void Start()
    {
        if (cursorToDisplay == null) cursorToDisplay = GameObject.Find("Cursors").transform.GetChild(cursorNumber).gameObject;

        _triggerController = GetComponent<TriggerController>();
    }

    void OnMouseOver()
    {
        if (_triggerController.isTriggered) cursorToDisplay.SetActive(true);
        else cursorToDisplay.SetActive(false);
    }

    void OnMouseExit()
    {
        cursorToDisplay.SetActive(false);
    }

    void OnMouseDrag()
    {
        if (onDrag && _triggerController.isTriggered) unityEvent.Invoke();
    }

    void OnMouseDown()
    {
        if (onClick && _triggerController.isTriggered) unityEvent.Invoke();
    }
}
