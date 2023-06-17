using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public Slider healthSlider;
    public Slider attackSlider;
    public Slider attackSpeedSlider;
    public Slider fireLevelSlider;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI fireLevelText;

    public Button healthButton;
    public Button attackButton;
    public Button attackSpeedButton;
    public Button fireLevelButton;

    public TextMeshProUGUI coinsText;

    private int healthValue;
    private int attackValue;
    private int attackSpeedValue;
    private int fireLevelValue;

    void Awake()
    {   
        healthSlider.value = PlayerPrefs.GetFloat("HealthSliderValue");
        attackSlider.value = PlayerPrefs.GetFloat("AttackSliderValue");
        attackSpeedSlider.value = PlayerPrefs.GetFloat("AttackSpeedSliderValue");
        fireLevelSlider.value = PlayerPrefs.GetFloat("FireLevelSliderValue");

        if (PlayerPrefs.HasKey("HealthCoinsCost"))
        {
            healthValue = PlayerPrefs.GetInt("HealthCoinsCost");
        }
        else
        {
            healthValue = 10;
            PlayerPrefs.SetInt("HealthCoinsCost", 10);
        }

        if (PlayerPrefs.HasKey("AttackCoinsCost"))
        {
            attackValue = PlayerPrefs.GetInt("AttackCoinsCost");
        }
        else
        {
            attackValue = 10;
            PlayerPrefs.SetInt("AttackCoinsCost", 10);
        }

        if (PlayerPrefs.HasKey("AttackSpeedCoinsCost"))
        {
            attackSpeedValue = PlayerPrefs.GetInt("AttackSpeedCoinsCost");
        }
        else
        {
            attackSpeedValue = 10;
            PlayerPrefs.SetInt("AttackSpeedCoinsCost", 10);
        }

        if (PlayerPrefs.HasKey("FireLevelCoinsCost"))
        {
            fireLevelValue = PlayerPrefs.GetInt("FireLevelCoinsCost");
        }
        else
        {
            fireLevelValue = 500;
            PlayerPrefs.SetInt("FireLevelCoinsCost", 500);
        }
    }

    void Start()
    {
        healthText.text = FormatNumber(healthValue);
        attackText.text = FormatNumber(attackValue);
        attackSpeedText.text = FormatNumber(attackSpeedValue);
        fireLevelText.text = FormatNumber(fireLevelValue);
    }

    void Update()
    {
        Debug.Log("COINS: " + PlayerPrefs.GetInt("Coins"));
        Debug.Log("HEALTH: " + PlayerPrefs.GetInt("Health"));
        Debug.Log("ATTACK POINTS: " + PlayerPrefs.GetInt("AttackPoints"));
        Debug.Log("ATTACK SPEED: " + PlayerPrefs.GetFloat("AttackSpeed"));
        Debug.Log("FIRE LEVEL: " + PlayerPrefs.GetInt("FireLevel"));

        CheckCoins(healthButton, healthValue, healthSlider);
        CheckCoins(attackButton, attackValue, attackSlider);
        CheckCoins(attackSpeedButton, attackSpeedValue, attackSpeedSlider);
        CheckCoins(fireLevelButton, fireLevelValue, fireLevelSlider);
    }

    void CheckCoins(Button button, int value, Slider slider)
    {
        coinsText.text = FormatNumber(PlayerPrefs.GetInt("Coins"));
        if (PlayerPrefs.GetInt("Coins") > value)
        {
            if (slider.value == slider.maxValue)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }
        else
        {
            button.interactable = false;
        }
    }

    private string FormatNumber(int value)
    {
        string suffix = "";
        float formattedValue = value;

        if (value >= 1000)
        {
            string[] suffixes = { "", "K", "M", "B", "T" }; // Pode adicionar mais sufixos conforme necessário
            int suffixIndex = 0;

            while (value >= 1000)
            {
                value /= 1000;
                formattedValue /= 1000;
                suffixIndex++;
            }

            suffix = suffixes[suffixIndex];
        }

        return formattedValue.ToString("0.##") + suffix;
    }

    public void UpgradeHealth()
    {
        PlayerPrefs.SetFloat("HealthSliderValue", healthSlider.value);
        int playerHealth = PlayerPrefs.GetInt("Health");
        PlayerPrefs.SetInt("Health", playerHealth + 15);
        healthSlider.value++;

        int playerCoins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", playerCoins - healthValue);

        healthValue += Mathf.CeilToInt(50 * Mathf.Log10(healthValue + 1));
        PlayerPrefs.SetInt("HealthCoinsCost", healthValue);
        string formattedValue = FormatNumber(healthValue);
        healthText.text = formattedValue;
        PlayerPrefs.Save();
    }

    public void UpgradeAttackPoints()
    {
        PlayerPrefs.SetFloat("AttackSliderValue", attackSlider.value);
        int playerAttackPoints = PlayerPrefs.GetInt("AttackPoints");
        PlayerPrefs.SetInt("AttackPoints", playerAttackPoints + 10);
        attackSlider.value++;

        int playerCoins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", playerCoins - attackValue);

        attackValue += Mathf.CeilToInt(50 * Mathf.Log10(attackValue + 1));
        PlayerPrefs.SetInt("AttackCoinsCost", attackValue);
        string formattedValue = FormatNumber(attackValue);
        attackText.text = formattedValue;
        PlayerPrefs.Save();
    }

    public void UpgradeAttackSpeed()
    {
        PlayerPrefs.SetFloat("AttackSpeedSliderValue", attackSpeedSlider.value);
        float playerAttackSpeed = PlayerPrefs.GetFloat("AttackSpeed");
        PlayerPrefs.SetFloat("AttackSpeed", playerAttackSpeed + 0.1f);
        attackSpeedSlider.value++;

        int playerCoins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", playerCoins - attackSpeedValue);

        attackSpeedValue += Mathf.CeilToInt(50 * Mathf.Log10(attackSpeedValue + 1));
        PlayerPrefs.SetInt("AttackSpeedCoinsCost", attackSpeedValue);
        string formattedValue = FormatNumber(attackSpeedValue);
        attackSpeedText.text = formattedValue;
        PlayerPrefs.Save();
    }

    public void UpgradeFireLevel()
    {
        PlayerPrefs.SetFloat("FireLevelSliderValue", fireLevelSlider.value);
        int playerFireLevel = PlayerPrefs.GetInt("FireLevel");
        PlayerPrefs.SetInt("FireLevel", playerFireLevel + 1);
        fireLevelSlider.value++;

        int playerCoins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", playerCoins - fireLevelValue);

        // Uso de coeficiente maior para um crescimento mais rápido do custo.
        fireLevelValue += Mathf.CeilToInt(500 * Mathf.Log10(fireLevelValue + 1));
        PlayerPrefs.SetInt("FireLevelCoinsCost", fireLevelValue);
        string formattedValue = FormatNumber(fireLevelValue);
        fireLevelText.text = formattedValue;
        PlayerPrefs.Save();
    }

}
