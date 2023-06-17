using UnityEngine;

public class ProgressiveRewards : MonoBehaviour
{
    public static float coinMultiplier = 1f;
    public static float xpMultiplier = 1f; // Multiplier for experience reward
    public StatsManager statsManager;
    public LevelChanger levelChanger;

    void Start()
    {
        InvokeRepeating(nameof(UpdateRewardsMultiplier), 1.0f, 1.0f);
        InvokeRepeating(nameof(CoinReward), 2.0f, 2.0f);
        InvokeRepeating(nameof(ExperienceReward), 0.8f, 0.8f);
    }

    private void UpdateRewardsMultiplier()
    {
        coinMultiplier = Mathf.Log(StatsManager.runDistance + 1);
        xpMultiplier = Mathf.Log(StatsManager.runDistance + 1) * StatsManager.runLevel;
    }

    public void CoinReward()
    {
        int coinsToAdd = Mathf.RoundToInt(1 * coinMultiplier);
        statsManager.AddCoins(coinsToAdd);
    }

    public void ExperienceReward()
    {
        int xpToAdd = Mathf.RoundToInt(1 * xpMultiplier);
        levelChanger.ExperienceIncrease(xpToAdd);
    }
}
