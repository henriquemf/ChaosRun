using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static int Health { get; set; }
    public static int AttackPoints { get; set; }
    public static float AttackSpeed { get; set; }
    public static int FireLevel { get; set; }
    public static int Coins { get; set; }

    public static int Initialized;  

    void Awake()
    {
        if (PlayerPrefs.HasKey("Initialized"))
        {
            return;
        }

        Health = 100;
        AttackPoints = 10;
        AttackSpeed = 0.5f;
        FireLevel = 0;
        Coins = 0;

        PlayerPrefs.SetInt("Health", Health);
        PlayerPrefs.SetInt("AttackPoints", AttackPoints);
        PlayerPrefs.SetFloat("AttackSpeed", AttackSpeed);
        PlayerPrefs.SetInt("FireLevel", FireLevel);
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.SetInt("Initialized", 1);
        PlayerPrefs.Save();
    }
}
