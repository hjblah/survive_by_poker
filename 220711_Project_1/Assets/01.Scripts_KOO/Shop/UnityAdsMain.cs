using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsMain : MonoBehaviour, IUnityAdsInitializationListener
{
    public string gameId = "4877075";
    public bool testMode = true;

    private void Awake()
    {
        Advertisement.Initialize(gameId, testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Init completed");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Unity Ads Init failed");
        Debug.Log("error : " + error);
        Debug.Log("message : " + message);
    }
}
