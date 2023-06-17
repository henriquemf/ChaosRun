using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAds : MonoBehaviour
{
    public RewardedAdsButton rewardedAdsButton;
    void Start()
    {
        rewardedAdsButton.LoadAd();
    }
}
