using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPopUp : MonoBehaviour
{
    public static SpawnPopUp instance;

    public GameObject[] popUps;
    public Transform parent;

    public Sprite[] attentionSprites;

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

    public void AttentionPopUp(string attentionText, int i)
    {
        GameObject newPopUp = Instantiate(popUps[1], parent.position, Quaternion.identity, parent);
        newPopUp.transform.GetChild(1).GetComponent<Image>().sprite = attentionSprites[i];
        newPopUp.transform.GetChild(2).GetComponent<TMP_Text>().text = attentionText;
    }
}
