using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    public Slider loadingSlider;
    public TMP_Text percentText;

    private int frames;

    public UIController _UIController;

    // Update is called once per frame
    void Update()
    {
        if (loadingSlider.value != loadingSlider.maxValue)
        {
            frames++;

            if (frames >= 10)
            {
                UpdateSlider(1);
                frames = 0;
            }
        }
        else EndLoading();
    }

    private void UpdateSlider(int number)
    {
        if (loadingSlider.value + number <= loadingSlider.maxValue) loadingSlider.value += number;
        else loadingSlider.value += loadingSlider.value + number - loadingSlider.maxValue;

        percentText.text = loadingSlider.value + "%";
    }

    private void EndLoading()
    {
        _UIController.OpenCloseUI(false);
        Destroy(gameObject);
    }
}
