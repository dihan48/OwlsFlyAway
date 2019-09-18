using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class Advert : MonoBehaviour, IUnityAdsListener
{

    //string myPlacementId = "video";
    //static bool adsReady = false;
    //bool testMode = true;
    static bool finish = false;
    string banner = "banner";
#if UNITY_EDITOR
    private string gameId = "3237431";
#elif UNITY_IOS
    private string gameId = "3237431";
#elif UNITY_ANDROID
     private string gameId = "3237430";
#elif UNITY_WEBGL
     private string gameId = "3237430";
#endif


    void Start()
    {
        finish = false;
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId);
        StartCoroutine(ShowBannerWhenReady());
    }

    public static bool AdsReady(string placementId)
    {
        if (Advertisement.IsReady(placementId))
            return true;
        else
            return false;
    }

    public static bool Finish()
    {
        return finish;
    }

    public static void Show(string placementId)
    {
        Advertisement.Show(placementId);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                Debug.LogWarning("ADS Failed");
                break;
            case ShowResult.Skipped:
                Debug.LogWarning("ADS Skipped");
                break;
            case ShowResult.Finished:
                Debug.LogWarning("ADS Finished");
                break;
        }
        finish = true;
    }
    public void OnUnityAdsReady(string placementId)
    {

    }

    public void OnUnityAdsDidError(string message)
    {
        finish = true;
    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(banner))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(banner);
    }
}
