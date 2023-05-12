using UnityEngine;

[CreateAssetMenu()]
public class ItemData : ScriptableObject
{
    public int itemID;
    public string itemName;
    public string itemDescription;

    public GameObject itemBTN;
    public GameObject itemOBJ;
}
