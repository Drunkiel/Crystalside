using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPopUp : MonoBehaviour
{
    public GameObject popUp;
    public Transform parent;

    public void SetPopUp(ItemData _itemData)
    {
        GameObject newPopUp = Instantiate(popUp, parent.position, Quaternion.identity, parent);
        newPopUp.transform.GetChild(2).GetComponent<Image>().sprite = _itemData.itemBTN.GetComponent<Image>().sprite;   
        newPopUp.transform.GetChild(3).GetComponent<TMP_Text>().text = _itemData.name;   

    }
}
