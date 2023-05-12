using TMPro;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public int slotID;
    public string itemName;
    public int quantity;
    public TMP_Text quantityText;

    public void QuantityUpdater()
    {
        quantity++;
        quantityText.text = quantity.ToString();
    }

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
    }
}
