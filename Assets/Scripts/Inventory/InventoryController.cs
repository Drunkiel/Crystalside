using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();

    public GameObject inventory;
    public GameObject slotPrefab;

    public UIController _UIController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && GameController.isGamePlaying)
        {
            _UIController.OpenCloseUI();
        }
    }
}
