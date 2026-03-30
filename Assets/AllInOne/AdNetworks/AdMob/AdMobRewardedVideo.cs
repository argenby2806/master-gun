#if ALLINONE_ADMOB
using UnityEngine;
using GoogleMobileAds.Api;
using System;

namespace Oblius.Assets.AllInOneAdnetworks.Admob
{
    public class AdMobRewardedVideo
    {
        RewardedAd rewardedAd;
        string adUnitId;

        public bool videoLoaded;
        public bool isShowing;

        public Action<bool> onRewardVideoWatched;

        AllInOneMainThreadDispatcher dispatcher;

        public AdMobRewardedVideo(string adUnitId, AllInOneMainThreadDispatcher dispatcher)
        {
            this.adUnitId = adUnitId;
            this.dispatcher = dispatcher;
            LoadAd();
        }

        public void LoadAd()
        {
            Debug.Log("LOADING REWARDED");

            videoLoaded = false;

            RewardedAd.Load(adUnitId, new AdRequest(), (ad, error) =>
            {
                if (error != null)
                {
                    Debug.Log("FAILED LOAD REWARDED: " + error);
                    return;
                }

                Debug.Log("REWARDED LOADED");

                rewardedAd = ad;
                videoLoaded = true;
            });
        }

        public void Show(Action<bool> onRewardVideoWatched)
        {
            if (rewardedAd != null && videoLoaded)
            {
                Debug.Log("SHOW REWARDED");

                isShowing = true;
                this.onRewardVideoWatched = onRewardVideoWatched;

                bool rewarded = false;

                rewardedAd.Show((reward) =>
                {
                    rewarded = true;
                });

                dispatcher.Enqueue(() =>
                {
                    isShowing = false;
                    onRewardVideoWatched?.Invoke(rewarded);
                });

                LoadAd();
            }
            else
            {
                Debug.Log("REWARDED NOT READY");
                onRewardVideoWatched?.Invoke(false);
            }
        }
    }
}
#endif