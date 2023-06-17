using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public static float playerASpeed;
    public static float playerMSpeed;
    public static int playerShootLevel;
    public static int playerShootStyle;
    
    public static int runCoins;
    public static float runDistance;
    public static int runExperience;
    public static int runLevel;
    public static GameObject[] orangeStaticPrefabs;
    public static GameObject[] greenStaticPrefabs;
    public static GameObject[] blueStaticPrefabs;
    public static GameObject[] purpleStaticPrefabs;

    public TextMeshProUGUI coinsText;
    public GameObject[] orangePrefabs;
    public GameObject[] greenPrefabs;
    public GameObject[] bluePrefabs;
    public GameObject[] purplePrefabs;

    void Awake() 
    {
        playerASpeed = PlayerPrefs.GetFloat("AttackSpeed");
        playerMSpeed = 1.5f;
        playerShootLevel = PlayerPrefs.GetInt("FireLevel");
        playerShootStyle = 0;

        runCoins = 0;
        runDistance = 0;
        runExperience = 0;
        runLevel = 1;
    }

    void Start()
    {
        orangeStaticPrefabs = orangePrefabs;
        greenStaticPrefabs = greenPrefabs;
        blueStaticPrefabs = bluePrefabs;
        purpleStaticPrefabs = purplePrefabs;
        coinsText.text = "COINS: " + FormatNumber(runCoins);
    }

    // Update is called once per frame
    void Update()
    {
        runDistance = Player.DistanceTravelled;
    }

    public void AddCoins(int value)
    {
        runCoins += value;
        coinsText.text = "COINS: " + FormatNumber(runCoins);
    }

    public static void RandomizePrefabs()
    {
        int randomIndex = Random.Range(0, orangeStaticPrefabs.Length);
        while (randomIndex == playerShootStyle)
        {
            randomIndex = Random.Range(0, orangeStaticPrefabs.Length);
        }
        playerShootStyle = randomIndex;
    }

    public static void ResetStats()
    {
        playerASpeed = PlayerPrefs.GetFloat("AttackSpeed");
        playerMSpeed = 1.0f;
        playerShootLevel = PlayerPrefs.GetInt("FireLevel");
        playerShootStyle = 0;

        runCoins = 0;
        runDistance = 0;
        runExperience = 0;
        runLevel = 1;
    }

    public static string FormatNumber(int value)
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
}
