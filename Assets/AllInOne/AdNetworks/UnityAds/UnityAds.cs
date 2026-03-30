using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if ALLINONE_UNITYADS
using Oblius.Assets.AllInOneAdnetworks.UnityADS;
using UnityEngine.Advertisements;
#endif


namespace Oblius.Assets.AllInOneAdnetworks.UnityADS
{
	public class UnityAds : AdNetwork
	{


		public override string defineSymbolString {
			get {
				return "ALLINONE_UNITYADS";
			}
			set {
			}
		}


		public string rewardedVideoID;
		public string interstitialID;

		#if ALLINONE_UNITYADS

		UnityAdsRewardedVideo rewardedVideo;
		UnityAdsInterstitial interstitial;

		void Start ()
		{
			rewardedVideo = new UnityAdsRewardedVideo (rewardedVideoID);
			interstitial = new UnityAdsInterstitial (interstitialID);
		}

		public void ShowInterstitial (Action onInterstitialWatched)
		{
			interstitial.Show (onInterstitialWatched);
		}


		public bool InterstitialLoaded ()
		{
			return interstitial.IsLoaded ();
		}

		public bool IsShowingInterstitial ()
		{
			return interstitial.isShowing;
		}

		public void ShowRewardedVideo (Action<bool> onRewardVideoWatched)
		{
			rewardedVideo.Show (onRewardVideoWatched);
		}


		public bool RewardedVideoLoaded ()
		{
			return rewardedVideo.IsLoaded ();
		}

		public bool IsShowingRewardedVideo ()
		{
			return rewardedVideo.isShowing;
		}

		#endif

	
	}
}
