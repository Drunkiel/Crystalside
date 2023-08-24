using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    private bool isLoaded;

    public Slider loadingSlider;
    public TMP_Text percentText;

    public UIController _UIController;

    private void Start()
    {
        isLoaded = true;
        /*_UIController.OpenCloseUI(true);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLoaded)
        {
            if (loadingSlider.value != loadingSlider.maxValue)
            {
                UpdateSlider(0.1f);
            }
            else EndLoading();
        }
    }

    private void UpdateSlider(float number)
    {
        if (loadingSlider.value + number <= loadingSlider.maxValue) loadingSlider.value += number;
        else loadingSlider.value += loadingSlider.value + number - loadingSlider.maxValue;

        percentText.text = loadingSlider.value + "%";
    }

    private void EndLoading()
    {
        _UIController.OpenCloseUI(false);
        isLoaded = true;
    }
}
