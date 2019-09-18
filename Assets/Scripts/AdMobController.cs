using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdMobController : MonoBehaviour
{
    private BannerView bannerView;

    public void Start()
    {
        RequestBanner();
        RequestInterstitial();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
             string adUnitId = "ca-app-pub-2737994209900703/8625165175";
#elif UNITY_IPHONE
             string adUnitId = "ca-app-pub-2737994209900703/6911077491";
#else
        string adUnitId = "unexpected_platform";
#endif

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        bannerView.LoadAd(CreateAdRequest());
        bannerView.Show();
    }

    private InterstitialAd interstitial;

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-2737994209900703/2757469249";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-2737994209900703/6673786653";
#else
        string adUnitId = "unexpected_platform";
#endif

        interstitial = new InterstitialAd(adUnitId);

        interstitial.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    public void InterstitialShow()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        interstitial.Destroy();
        RequestInterstitial();
    }
}