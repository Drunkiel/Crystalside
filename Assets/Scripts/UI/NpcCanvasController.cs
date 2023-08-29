using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcCanvasController : MonoBehaviour
{
    public TMP_Text titleText;
    public Slider healthSlider;

    public void UpdateTitle(string name)
    {
        titleText.text = "Bob the " + name;
    }

    public void SetNewHealthSlider(int newMaxHP)
    {
        healthSlider.maxValue = newMaxHP;
        healthSlider.value = newMaxHP;
    }

    public void UpdateHealthSlider(int value)
    {
        if (healthSlider.value - value <= 0) healthSlider.value = 0;
        else healthSlider.value -= value;
    }
}
