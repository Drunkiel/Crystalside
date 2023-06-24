using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StatisticsController
{
    public int health;
    public int oxygen;
    public int money;

    public Slider healthSlider;
    public Slider oxygenSlider;

    public float damageCooldown;
    public float oxygenCooldown;
    private float resetCooldown = 1f;

    public UIController[] _UIControllers;

    public void TakeDamage(int value, bool instantDamage = false)
    {
        if (instantDamage) damageCooldown = 0;

        if (damageCooldown > 0) damageCooldown -= Time.deltaTime;
        else
        {
            if (health - value >= 0) health -= value;
            if (health == 0) PlayerDeath();

            UpdateTexts();
            damageCooldown = resetCooldown;
        }
    }

    public void TakeOxygen(int value, bool instantTake = false)
    {
        if (instantTake) oxygenCooldown = 0;

        if (oxygenCooldown > 0) oxygenCooldown -= Time.deltaTime;
        else
        {
            if (oxygen - value >= 0) oxygen -= value;
            else TakeDamage(5, true);

            UpdateTexts();
            oxygenCooldown = resetCooldown;
        }
    }

    private void PlayerDeath()
    {
        GameController.isGamePlaying = false;
        GameController.isGamePaused = false;
        PlayerController.isPlayerStopped = true;

        if (_UIControllers[0].isUIOpen) _UIControllers[0].OpenCloseUI();
        _UIControllers[1].OpenCloseUI();
    }

    private void UpdateTexts()
    {
        healthSlider.value = health;
        oxygenSlider.value = oxygen;
    }
}
