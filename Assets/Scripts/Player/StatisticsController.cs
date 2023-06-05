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

    public void TakeDamage(int value, bool instantDamage = false)
    {
        if (damageCooldown > 0)
        {
            if (!instantDamage) damageCooldown -= Time.deltaTime;
            else damageCooldown = 0;
        }
        else
        {
            if (health - value > 0) health -= value;
            else
            {
                SpawnPopUp.instance.AttentionPopUp("Player died", 0);
                health = 100;
                oxygen = 100;
            }

            UpdateTexts();
            damageCooldown = resetCooldown;
        }
    }

    public void TakeOxygen(int value)
    {
        if (oxygenCooldown > 0) oxygenCooldown -= Time.deltaTime;
        else
        {
            if (oxygen - value >= 0) oxygen -= value;
            else TakeDamage(5, true);

            UpdateTexts();
            oxygenCooldown = resetCooldown;
        }
    }

    private void UpdateTexts()
    {
        healthSlider.value = health;
        oxygenSlider.value = oxygen;
    }
}
