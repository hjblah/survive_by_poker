
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class Ads_Rewarded : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public string adUnitId = "Rewarded_Android";
    public Button btnLoad;
    public Button btnShow;
    public GameObject rewardPopup;

    // Start is called before the first frame update
    void Start()
    {
        this.btnShow.interactable = false;

        this.btnLoad.onClick.AddListener(() => {
            this.LoadAd();
            this.btnShow.interactable = true;
            rewardPopup.SetActive(true);
        });
        this.btnShow.onClick.AddListener(() => {
            rewardPopup.SetActive(false);
            this.ShowAd();
            this.btnShow.interactable = false;
        });
    }

    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + adUnitId);
        Advertisement.Load(adUnitId, this);
    }
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.LogFormat("OnUnityAdsAdLoaded: {0}", placementId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogFormat("OnUnityAdsFailedToLoad: {0}, {1}, {2}", placementId, error, message);
    }


    // Implement a method to execute when the user clicks the button.
    public void ShowAd()
    {
        // Then show the ad:
        Advertisement.Show(adUnitId, this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogFormat("OnUnityAdsShowFailure: {0}, {1}, {2}", placementId, error, message);
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.LogFormat("OnUnityAdsShowStart: {0}", placementId);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.LogFormat("OnUnityAdsShowClick: {0}", placementId);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.LogFormat("OnUnityAdsShowComplete: {0}, {1}", placementId, showCompletionState.ToString());

        if (adUnitId.Equals(placementId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            Debug.Log("������ �޾ҽ��ϴ�.");

            // Load another ad:
            Advertisement.Load(adUnitId, this);
        }
    }
}
