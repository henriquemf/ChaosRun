using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelChanger : MonoBehaviour
{
    public int levelThreshold = 100; // Valor necessário para subir de nível
    public Slider levelSlider; // Referência para o Slider de nível
    public TextMeshProUGUI levelText; // Referência para o TextMeshProUGUI do nível
    public GameObject levelUpPanel; // Referência para o painel de level up
    public InventoryManager inventoryManager; // Referência para o InventoryManager
    public AudioSource levelUpSound; // Referência para o AudioSource do level up

    private int currentLevel;
    private int currentExperience;

    private void Start()
    {
        currentLevel = StatsManager.runLevel;
        currentExperience = StatsManager.runExperience;
        UpdateLevelUI();
    }

    private void Update()
    {
        CheckLevelUp();
        UpdateSliderValue();
    }

    public void ExperienceIncrease(int value)
    {
        currentExperience += value;
        UpdateLevelUI();
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (currentExperience >= levelThreshold)
        {
            levelUpSound.Play();
            levelUpPanel.SetActive(true);
            inventoryManager.RandomizeItems();
            inventoryManager.AssignImagesAndTexts();

            levelSlider.value = 0;
            levelSlider.maxValue = currentExperience + 100;
            levelThreshold += 100;
            currentLevel++;
            currentExperience = 0;

            UpdateLevelUI();
            PauseManager.Pause();
        }
    }

    private void UpdateSliderValue()
    {
        levelSlider.value = currentExperience;
    }

    private void UpdateLevelUI()
    {
        levelText.text = "LEVEL " + currentLevel.ToString();
    }

    public void ResetLevel()
    {
        levelSlider.value = 0;
        levelSlider.maxValue = 100;
        levelThreshold = 100;

        UpdateLevelUI();
    }
}
