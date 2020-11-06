using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class ReklamRewardedVideo : MonoBehaviour
{
    private int altin = 0;
    private RewardBasedVideoAd reklamObjesi;

    void Start()
    {
        MobileAds.Initialize(reklamDurumu => { });

        reklamObjesi = RewardBasedVideoAd.Instance;
        reklamObjesi.OnAdClosed += YeniReklamAl; // Kullanıcı reklamı kapattıktan sonra çağrılır
        reklamObjesi.OnAdRewarded += OyuncuyuOdullendir; // Kullanıcı reklamı tamamen izledikten sonra çağrılır

        YeniReklamAl(null, null);
    }

    public void YeniReklamAl(object sender, EventArgs args)
    {
        AdRequest reklamIstegi = new AdRequest.Builder().Build();
        reklamObjesi.LoadAd(reklamIstegi, "ca-app-pub-3940256099942544/5224354917");
    }

    private void OyuncuyuOdullendir(object sender, Reward odul)
    {
        Debug.Log("Ödül türü: " + odul.Type);
        altin += (int)odul.Amount;
    }
    public void ReklamGostr()
    {
        reklamObjesi.Show();
        GameManager.Instance.HearthAdd();
    }
}