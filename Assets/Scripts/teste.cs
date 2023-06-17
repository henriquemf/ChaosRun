using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teste : MonoBehaviour
{
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.SetInt("Health", 100);
        PlayerPrefs.SetInt("AttackPoints", 10);
        PlayerPrefs.SetFloat("AttackSpeed", 1.0f);
        PlayerPrefs.SetInt("FireLevel", 0);
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetInt("Initialized", 1);
        PlayerPrefs.SetFloat("HealthSliderValue", 0);
        PlayerPrefs.SetFloat("AttackSliderValue", 0);
        PlayerPrefs.SetFloat("AttackSpeedSliderValue", 0);
        PlayerPrefs.SetFloat("FireLevelSliderValue", 0);
        PlayerPrefs.SetInt("HealthCoinsCost", 10);
        PlayerPrefs.SetInt("AttackCoinsCost", 10);
        PlayerPrefs.SetInt("AttackSpeedCoinsCost", 10);
        PlayerPrefs.SetInt("FireLevelCoinsCost", 100);
        PlayerPrefs.SetFloat("SfxVolume", 1.0f);
        PlayerPrefs.SetFloat("MusicVolume", 1.0f);
        PlayerPrefs.Save();
    }
}
