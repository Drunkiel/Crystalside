using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
    public List<bool> isFull = new List<bool>();
    public int maxCapacity;

    public GameObject inventory;
    public GameObject slotPrefab;

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
