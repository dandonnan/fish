namespace CommuteAndroid.Ads
{
    using Android.Gms.Ads;
    using Android.Gms.Ads.Interstitial;

    /// <summary>
    /// A listener for interstital ads.
    /// </summary>
    internal class InterstitialAdListener : InterstitialAdLoadCallback
    {
        /// <summary>
        /// Called when the ad is loaded.
        /// </summary>
        /// <param name="interstitialAd">The ad.</param>
        public override void OnAdLoaded(InterstitialAd interstitialAd)
        {
            AdManager.AdLoaded = true;
            AdManager.InterstitialHandler = interstitialAd;
            base.OnAdLoaded(interstitialAd);
        }

        /// <summary>
        /// Called when the ad fails to load.
        /// </summary>
        /// <param name="p0">The error.</param>
        public override void OnAdFailedToLoad(LoadAdError p0)
        {
            AdManager.InterstitialHandler = null;
            base.OnAdFailedToLoad(p0);
        }
    }
}