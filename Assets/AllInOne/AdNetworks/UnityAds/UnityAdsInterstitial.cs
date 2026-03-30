#if ALLINONE_UNITYADS
using UnityEngine.Advertisements;
using System;


namespace Oblius.Assets.AllInOneAdnetworks.UnityADS
{
	public class UnityAdsInterstitial
	{

		public string id;
		Action onInterstitialWatched;

		public bool isShowing;

		public UnityAdsInterstitial (string id)
		{
			this.id = id;
			Advertisement.Initialize (id);
		}

		public void Show (Action onRewardVideoWatched)
		{
			this.onInterstitialWatched = onRewardVideoWatched;
			ShowOptions options = new ShowOptions ();
			options.resultCallback = HandleShowResult;
			Advertisement.Show (id, options);
			isShowing = true;
		}

		void HandleShowResult (ShowResult result)
		{
			isShowing = false;

			if (result == ShowResult.Finished) {
				onInterstitialWatched ();
			} else if (result == ShowResult.Skipped) {
				onInterstitialWatched ();
			} else if (result == ShowResult.Failed) {
				onInterstitialWatched ();
			}

		}

		public bool IsLoaded ()
		{
			return Advertisement.IsReady (id);
		}

	}
}
#endif