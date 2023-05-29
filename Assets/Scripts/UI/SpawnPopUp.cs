using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPopUp : MonoBehaviour
{
    public static SpawnPopUp instance;

    public GameObject[] popUps;
    public Transform parent;
    public Sprite errorSprite;

    void Awake()
    {
        instance = this;
    }

    public void SetPopUp(ItemData _itemData)
    {
        GameObject newPopUp = Instantiate(popUps[0], parent.position, Quaternion.identity, parent);
        newPopUp.transform.GetChild(2).GetComponent<Image>().sprite = _itemData.itemSprite;   
        newPopUp.transform.GetChild(3).GetComponent<TMP_Text>().text = _itemData.name;   
    }

    public void ErrorPopUp(string errorText)
    {
        GameObject newPopUp = Instantiate(popUps[1], parent.position, Quaternion.identity, parent);
        newPopUp.transform.GetChild(1).GetComponent<Image>().sprite = errorSprite;
        newPopUp.transform.GetChild(2).GetComponent<TMP_Text>().text = errorText;
    }
}
