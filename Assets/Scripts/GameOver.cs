using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    private int runCoins;
    private float runDistance;
    private int playerCoins;
    public LevelChanger levelChanger;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI distanceText;

    private void Start()
    {
        runCoins = StatsManager.runCoins;
        runDistance = StatsManager.runDistance;
        playerCoins = PlayerPrefs.GetInt("Coins");

        coinsText.text = StatsManager.FormatNumber(runCoins);
        distanceText.text = StatsManager.FormatNumber((int)runDistance) + "meters";
    }

    public void ReloadGame()
    {
        PauseManager.Resume();
        playerCoins += runCoins;
        PlayerPrefs.SetInt("Coins", playerCoins);
        PlayerPrefs.Save();
        Player.DistanceTravelled = 0;
        StatsManager.ResetStats();
        levelChanger.ResetLevel();
        PauseManager.Resume();
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        PauseManager.Resume();
        playerCoins += runCoins;
        PlayerPrefs.SetInt("Coins", playerCoins);
        PlayerPrefs.Save();
        Player.DistanceTravelled = 0;
        StatsManager.ResetStats();
        levelChanger.ResetLevel();
        PauseManager.Resume();
        SceneManager.LoadScene(0);
    }

    public void DoubleCoins()
    {
        runCoins *= 2;
    }
}
