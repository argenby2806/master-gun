#if ALLINONE_ADMOB
using UnityEngine;
using GoogleMobileAds.Api;
using System;

namespace Oblius.Assets.AllInOneAdnetworks.Admob
{
    public class AdMobInterstitial
    {
        string adUnitID;
        InterstitialAd interstitial;

        Action onAdClosed;
        AllInOneMainThreadDispatcher dispatcher;

        public bool isShowing;

        public AdMobInterstitial(string adUnitID, AllInOneMainThreadDispatcher dispatcher)
        {
            this.adUnitID = adUnitID;
            this.dispatcher = dispatcher;
            LoadAd();
        }

        void LoadAd()
        {
            Debug.Log("LOADING INTERSTITIAL...");

            InterstitialAd.Load(adUnitID, new AdRequest(), (ad, error) =>
            {
                if (error != null)
                {
                    Debug.Log("FAILED LOAD INTERSTITIAL: " + error);
                    return;
                }

                Debug.Log("INTERSTITIAL LOADED");

                interstitial = ad;

                interstitial.OnAdFullScreenContentClosed += () =>
                {
                    dispatcher.Enqueue(() =>
                    {
                        Debug.Log("INTERSTITIAL CLOSED");

                        isShowing = false;
                        onAdClosed?.Invoke();
                    });
                };
            });
        }

        public void Show(Action onAdClosed)
        {
            if (interstitial != null && interstitial.CanShowAd())
            {
                Debug.Log("SHOW INTERSTITIAL");

                isShowing = true;
                this.onAdClosed = onAdClosed;

                interstitial.Show();
            }
            else
            {
                Debug.Log("INTERSTITIAL NOT READY");

                onAdClosed?.Invoke();
            }

            LoadAd(); // preload next
        }

        public bool IsLoaded()
        {
            return interstitial != null && interstitial.CanShowAd();
        }
    }
}
#endif