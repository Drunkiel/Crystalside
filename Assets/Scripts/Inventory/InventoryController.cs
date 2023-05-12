using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject[] slots;
    public bool[] isFull;
    public int maxCapacity;

    UIController _UIController;

    void Start()
    {
        _UIController = GetComponent<UIController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _UIController.OpenCloseUI();
        }
    }
}
