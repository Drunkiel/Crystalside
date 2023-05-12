using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemData _itemData;

    InventoryController _inventoryController;

    void Start()
    {
        _inventoryController = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryController>();
    }

    public void Pickup()
    {
        List<int> duplicatesID = CheckForDuplicates();

        if (duplicatesID.Count > 0)
        {
            ItemSlot _itemSlot = _inventoryController.slots[duplicatesID[0]].GetComponent<ItemSlot>();
            _itemSlot.QuantityUpdater();

            if (_itemSlot.quantity == _inventoryController.maxCapacity) _inventoryController.isFull[duplicatesID[0]] = true;
            Destroy(gameObject);
        }
        else
        {
            for (int i = 0; i < _inventoryController.slots.Length; i++)
            {
                ItemSlot _itemSlot = _inventoryController.slots[i].GetComponent<ItemSlot>();

                if (_inventoryController.isFull[i] == false && _itemSlot.quantity == 0)
                {
                    Instantiate(_itemData.itemBTN, _inventoryController.slots[i].transform.GetChild(0), false);
                    _itemSlot.itemName = _itemData.itemName;
                    _itemSlot.QuantityUpdater();
                    Destroy(gameObject);
                    break;
                }
            }
        }

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SpawnPopUp>().SetPopUp(_itemData);
    }

    private List<int> CheckForDuplicates()
    {
        List<int> duplicatesID = new List<int>();
        duplicatesID.Clear();

        for (int i = 0; i < _inventoryController.slots.Length; i++)
        {
            ItemSlot _itemSlot = _inventoryController.slots[i].GetComponent<ItemSlot>();

            if (_inventoryController.isFull[i] == false
                && _itemSlot.itemName == _itemData.itemName) duplicatesID.Add(i);
        }

        return duplicatesID;
    }
}
