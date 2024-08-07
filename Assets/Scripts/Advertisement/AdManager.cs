using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AdState
{
    Disable,
    Reward,
    Banner,
    Interstitial
}

public class AdManager : MonoBehaviour
{
    public static AdManager instance;
    public AdsInitializer adsInitializer;
    public AdBanner adBanner;
    public AdReword adReword;
    public AdInterstitial adInterstitial;

    [SerializeField] GameObject rewardTest;
    [SerializeField] GameObject bannerTest;
    [SerializeField] GameObject interstitialTest;

    [SerializeField]
    public AdState adState;

    void Awake()
    {
        instance = this;
        switch (adState)
        {
            case AdState.Banner:
                bannerTest.SetActive(true);
                break;
            case AdState.Interstitial:
                interstitialTest.SetActive(true);
                break;
            case AdState.Reward:
                // 테스트시 활성화
                rewardTest.SetActive(true);
                break;
        }
        if (adState != AdState.Disable)
        {
            adsInitializer.InitializeAds();
        }
    }
}
