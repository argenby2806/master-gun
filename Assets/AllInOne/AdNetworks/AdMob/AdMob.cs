using UnityEngine;
using System;
using Oblius.Assets.AllInOneAdnetworks;

#if ALLINONE_ADMOB
using GoogleMobileAds.Api;
#endif

namespace Oblius.Assets.AllInOneAdnetworks.Admob
{
    public class AdMob : AdNetwork
    {
        public override string defineSymbolString
        {
            get { return "ALLINONE_ADMOB"; }
            set { }
        }

        public string androidBannerID;
        public string androidInterstitialID;
        public string androidRewardVideoID;

#if ALLINONE_ADMOB
        public AdPosition bannerPosition;

        AdMobBanner banner;
        AdMobInterstitial interstitial;
        AdMobRewardedVideo rewardedVideo;

        AllInOneMainThreadDispatcher dispatcher;

        void Start()
        {
#if UNITY_EDITOR
            return;
#endif
            Debug.Log("INIT ADMOB");

            MobileAds.Initialize(initStatus => { });

            dispatcher = GetComponent<AllInOneMainThreadDispatcher>();

            banner = new AdMobBanner(androidBannerID, bannerPosition);
            interstitial = new AdMobInterstitial(androidInterstitialID, dispatcher);
            rewardedVideo = new AdMobRewardedVideo(androidRewardVideoID, dispatcher);
        }

        public void ShowBanner() => banner?.Show();
        public void HideBanner() => banner?.Hide();

        public void ShowInterstitial(Action onClosed)
        {
            interstitial?.Show(onClosed);
        }

        public void ShowRewardedVideo(Action<bool> result)
        {
            rewardedVideo?.Show(result);
        }

        public bool RewardedVideoLoaded() => rewardedVideo != null && rewardedVideo.videoLoaded;
        public bool InterstitialLoaded() => interstitial != null && interstitial.IsLoaded();

        public bool IsShowingInterstitial() => interstitial != null && interstitial.isShowing;
        public bool IsShowingRewardedVideo() => rewardedVideo != null && rewardedVideo.isShowing;

#endif
    }
}