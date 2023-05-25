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
            //Adding to duplicates
            ItemSlot _itemSlot = _inventoryController.slots[duplicatesID[0]].GetComponent<ItemSlot>();
            _itemSlot.UpdateQuantity();

            if (_itemSlot.quantity == _inventoryController.maxCapacity) _inventoryController.isFull[duplicatesID[0]] = true;
            Destroy(gameObject);
        }
        else
        {
            //Creating new item
            GameObject newSlot = Instantiate(_inventoryController.slotPrefab, _inventoryController.inventory.transform, false);
            _inventoryController.slots.Add(newSlot);
            _inventoryController.isFull.Add(false);
            ItemSlot _itemSlot = newSlot.GetComponent<ItemSlot>();
            _itemSlot.nameText.text = _itemData.itemName;
            _itemSlot.value = _itemData.value;
            _itemSlot.itemImage.sprite = _itemData.itemSprite;
            _itemSlot.UpdateQuantity();

            //Destroying object
            Destroy(gameObject);
        }

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SpawnPopUp>().SetPopUp(_itemData);
    }

    private List<int> CheckForDuplicates()
    {
        List<int> duplicatesID = new List<int>();
        duplicatesID.Clear();

        for (int i = 0; i < _inventoryController.slots.Count; i++)
        {
            ItemSlot _itemSlot = _inventoryController.slots[i].GetComponent<ItemSlot>();

            if (_inventoryController.isFull[i] == false
                && _itemSlot.nameText.text == _itemData.itemName) duplicatesID.Add(i);
        }

        return duplicatesID;
    }
}
