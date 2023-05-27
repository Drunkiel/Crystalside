using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public int slotID;
    public TMP_Text nameText;
    public int quantity;
    public TMP_Text quantityText;
    public Image itemImage;
    public int value;
    public TMP_Text valueText;

    void Start()
    {
        slotID = GameObject.Find("Player").GetComponent<InventoryController>().slots.Count;
    }

    public void UpdateQuantity()
    {
        quantity++;
        quantityText.text = quantity.ToString();
    }
/*
    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out SpawnItem _spawnItem))
            {
                quantity--;
                _spawnItem.SpawnPickedItem();
                if (quantity <= 0) Destroy(child.GetChild(0).gameObject);
            }
        }
    }*/
}
