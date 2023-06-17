using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public static int runCoins;
    private float runDistance;
    private int playerCoins;
    public LevelChanger levelChanger;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI distanceText;
    public AudioSource gameOverSound;
    public AudioSource gameMusic;
    public RewardedAdsButton rewardedAdsButton;

    private void Start()
    {
        rewardedAdsButton.LoadAd();
        
        runCoins = StatsManager.runCoins;
        runDistance = StatsManager.runDistance;
        playerCoins = PlayerPrefs.GetInt("Coins");
        gameOverSound.Play();
        gameMusic.pitch = 0.5f;
        gameMusic.volume /= 3;

        coinsText.text = StatsManager.FormatNumber(runCoins);
        distanceText.text = StatsManager.FormatNumber((int)runDistance) + "meters";
    }

    public void ReloadGame()
    {
        playerCoins += runCoins;
        PlayerPrefs.SetInt("Coins", playerCoins);
        PlayerPrefs.Save();
        Player.DistanceTravelled = 0;
        StatsManager.ResetStats();
        levelChanger.ResetLevel();
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        playerCoins += runCoins;
        PlayerPrefs.SetInt("Coins", playerCoins);
        PlayerPrefs.Save();
        Player.DistanceTravelled = 0;
        StatsManager.ResetStats();
        levelChanger.ResetLevel();
        SceneManager.LoadScene(0);
    }

    public void DoubleCoins()
    {
        runCoins *= 2;
    }
}
