using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float timer;
    private float refresh;
    [SerializeField] private float avgFramerate;
    private int frameCount;

    public TMP_Text displayText;

    // Update is called once per frame
    void Update()
    {
        frameCount++;

        if (frameCount >= 30)
        {
            CountFPS();
            frameCount = 0;
        }
    }

    private void CountFPS()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) avgFramerate = (int)(1f / timelapse);
        displayText.text = avgFramerate + " FPS";
    }
}
