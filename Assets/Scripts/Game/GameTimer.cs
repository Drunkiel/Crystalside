using TMPro;
using UnityEngine;

[System.Serializable]
public class GameTimer
{
    public TMP_Text timeText;
    public float timeLeft;

    public void SetNewTimer(int value)
    {
        timeLeft = value * 60; 
    }

    public void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;
        timeText.text = Mathf.Ceil(timeLeft) + "s";
    }
}
