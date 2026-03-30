#if ALLINONE_ADMOB
using UnityEngine;
using GoogleMobileAds.Api;

namespace Oblius.Assets.AllInOneAdnetworks.Admob
{
    public class AdMobBanner
    {
        BannerView banner;
        string adUnitID;

        public AdMobBanner(string adUnitID, AdPosition position)
        {
            this.adUnitID = adUnitID;

            banner = new BannerView(adUnitID, AdSize.Banner, position);

            Debug.Log("LOAD BANNER");

            banner.LoadAd(new AdRequest());
            banner.Hide();
        }

        public void Show()
        {
            Debug.Log("SHOW BANNER");
            banner?.Show();
        }

        public void Hide()
        {
            banner?.Hide();
        }
    }
}
#endif