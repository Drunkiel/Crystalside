using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuelController : MonoBehaviour
{
    public int fuelState;

    public int fuelCost;
    public int tankCapacity;
    public int fuelConsumption;

    public TMP_Text fuelCostText;
    public TMP_Text[] fuelPercentTexts;
    public Slider tankSlider;
    public TMP_Text tankCapacityText;
    public TMP_Text fuelConsumptionText;

    private void Start()
    {
        UpdateFuelTankTexts();
    }

    public void UpdateFuelTankTexts()
    {
        fuelCostText.text = fuelCost.ToString();
        tankSlider.maxValue = tankCapacity;
        tankSlider.value = fuelState;
        for (int i = 0; i < fuelPercentTexts.Length; i++)
        {
            fuelPercentTexts[i].text = (fuelState * 100 / tankCapacity) + "%";
        }
        tankCapacityText.text = tankCapacity + "l";
        fuelConsumptionText.text = fuelConsumption + "l/1l.y";
    }

    public void UseFuel(int value)
    {
        StatisticsController _statisticsController = GameObject.Find("Player").GetComponent<PlayerController>()._statisticsController;

        fuelState -= value;
        UpdateFuelTankTexts();
    }

    public void RefillFuel(int value)
    {
        StatisticsController _statisticsController = GameObject.Find("Player").GetComponent<PlayerController>()._statisticsController;

        if (fuelState != tankCapacity)
        {
            if (_statisticsController.money >= fuelCost)
            {
                fuelState += value;
                _statisticsController.money -= fuelCost;
                UpdateFuelTankTexts();
            }
            else
            {
                SpawnPopUp.instance.AttentionPopUp("Not enough cash", 0);
            }
        }
        else
        {
            SpawnPopUp.instance.AttentionPopUp("Tank is full", 1);
        }
    }

    public bool CheckIfEnoughFuel(int value)
    {
        if (fuelState >= value) return true;
        else SpawnPopUp.instance.AttentionPopUp("Not enough fuel", 0);

        return false;
    }
}
