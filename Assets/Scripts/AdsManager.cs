using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using UnityEngine.Monetization;
using UnityEngine.Advertisements;
using ShowResult = UnityEngine.Monetization.ShowResult;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;
    public static string targetStr = "";

    #region UnityAds
    string placementId_video = "video";
    string placementId_rewardedvideo = "rewardedVideo";
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        //Initialize Unity
        Monetization.Initialize("3222239", false);
        //Advertisement.Initialize("3222239", false);
    }

    private void Start()
    {
        //StartCoroutine(ShowBannerWhenReady());
    }

    //IEnumerator ShowBannerWhenReady()
    //{
    //    while (!Advertisement.IsReady("bannerPlacement"))
    //    {
    //        Debug.Log("Banner not ready");
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //    Debug.Log("Showing Banner");
    //    Advertisement.Banner.Show("bannerPlacement");
    //    Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    //}

    public void ShowInterstitial(string str)
    {
        Debug.Log("Showing interstitial ad   =" + str);
        targetStr = str;
        Time.timeScale = 0;
        StartCoroutine(WaitForAd());
    }

    public void ShowRewardedVideo(string str)
    {
        Debug.Log("Show Rewarded ad  =" + str);
        targetStr = str;
        Time.timeScale = 0;
        StartCoroutine(WaitForAd(true));
    }

    IEnumerator WaitForAd(bool rewarded = false)
    {
        string placementId = rewarded ? placementId_rewardedvideo : placementId_video;
        while (!Monetization.IsReady(placementId))
        {
            yield return null;
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;

        if (ad != null)
        {
            if(rewarded == true)
                ad.Show(OnResultRewarded);
            else
                ad.Show(OnResultInterstitial);
        }
    }

    private void OnResultInterstitial(ShowResult result)
    {
        Debug.Log("InterstitialResult ::::::" + result);
        Time.timeScale = 1;
        SendSuccessMsg();
    }

    private void OnResultRewarded(ShowResult result)
    {
        Debug.Log("RewardedResult ::::::" + result);
        if(result == ShowResult.Finished)
            SendSuccessMsg();
        else
        {
            SendFailMsg();
        }
    }
    #endregion

    private void SendSuccessMsg()
    {
        Time.timeScale = 1;
        Debug.Log("@@@@  =" + targetStr);
        GameObject.Find("GameController").SendMessage("OnRVRewardReceived", targetStr);
        //GameObject.Find("Canvas").BroadcastMessage("OnRVRewardReceived", targetStr);

        targetStr = "";
    }

    private void SendFailMsg()
    {
        Time.timeScale = 1;
        Debug.Log("%%% =" + targetStr);
        GameObject.Find("GameController").SendMessage("OnRVRewardReceived_Fail", targetStr);
        //GameObject.Find("Canvas").SendMessage("OnRVRewardReceived_Fail", targetStr);

        targetStr = "";
    }

    
}