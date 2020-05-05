using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class AdMobManager : MonoBehaviour
{
    public static AdMobManager instance;

    private InterstitialAd interstitial;

    private RewardBasedVideoAd rewardedVideoAds;

    [Header("FOR ANDROID APP ID")] 
    [SerializeField] private string AndroidAppID;

    [Header("FOR IPHONE APP ID")] 
    [SerializeField] private string IphoneAppID;

    [Header("FOR ANDROID ADS")]
    [SerializeField] private string AndroidPopUp;
    [SerializeField] private string AndroidRewardedAd;

    [Header("FOR IPHONE ADS")] 
    [SerializeField] private string IphonePopUp;
    [SerializeField] private string IphoneRewardedAd;

    public bool RewardedAdFail = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {

#if UNITY_ANDROID
        string appID = AndroidAppID;
#elif UNITY_IPHONE
        string appID = IphoneAppID;
#else
        string appID="";
#endif

        MobileAds.Initialize(appID);

        this.rewardedVideoAds = RewardBasedVideoAd.Instance;

        rewardedVideoAds.OnAdClosed += VideoClosed;
        rewardedVideoAds.OnAdRewarded += RewardBasedVideoWatched;

        RequestInterstitial();
        RequestRewardedBasedAd();
    }

    #region Pop-Up

    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string interstitialId = AndroidPopUp;
#elif UNITY_IPHONE
            string interstitialId = IphonePopUp;
#else
            string interstitialId = "unexpected_platform";
#endif

        if (interstitial != null)
        {
            interstitial.Destroy();
        }

        interstitial = new InterstitialAd(interstitialId);

        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

    }

    public void ShowInterstialAds()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        } 
            RequestInterstitial();
    }

    #endregion

    public void RequestRewardedBasedAd()
    {
#if UNITY_ANDROID
        string rewardedID = AndroidRewardedAd;
#elif UNITY_IPHONE
            string rewardedID = IphoneRewardedAd;
#else
            string rewardedID = "unexpected_platform";
#endif

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedVideoAds.LoadAd(request,rewardedID);
    }

    public void ShowRewardBasedAd()
    {
        if (this.rewardedVideoAds.IsLoaded())
        {
            rewardedVideoAds.Show();
        }
    }

    public void RewardBasedVideoWatched(object sender, EventArgs e)
    {
        if (ShopManager.instance.getCoinButtonClicked)
        {
            EventManager.TriggerEvent(EventManager.instance.WatchAdsGetCoin);

        }
        else if (MainMenuManager.instance.yesButtonClicked)
        {
            EventManager.TriggerEvent(EventManager.instance.WatchAdsGetEnergy);
        }

    }

    public void VideoClosed(object sender, EventArgs e)
    {
        RequestRewardedBasedAd();
    }
    

}