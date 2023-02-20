namespace CommuteAndroid.Ads
{
    using Android.Gms.Ads;
    using Android.Gms.Ads.Interstitial;

    /// <summary>
    /// A manager for ads.
    /// </summary>
    internal static class AdManager
    {
        /// <summary>
        /// The ad view that displays ads.
        /// </summary>
        public static AdView AdView = null;

        /// <summary>
        /// Whether an ad is loaded.
        /// </summary>
        public static bool AdLoaded = false;

        /// <summary>
        /// The ad handler.
        /// </summary>
        public static InterstitialAd InterstitialHandler = null;

        /// <summary>
        /// A counter that determines when to display ads.
        /// </summary>
        private static int adCounter;

        /// <summary>
        /// The id of the ad.
        /// </summary>
        private static readonly string adId = "INSERT-VALID-AD-ID-HERE";

        /// <summary>
        /// Initialise an advert.
        /// </summary>
        public static void InitialiseAd()
        {
            MobileAds.Initialize(MainGame.Activity);

            InterstitialAd.Load(MainGame.Activity, adId, new AdRequest.Builder().Build(), new InterstitialAdListener());
        }

        /// <summary>
        /// Show an ad.
        /// </summary>
        public static void ShowAd()
        {
            // Increase the counter
            adCounter++;

            // If the counter has reached its target, and an ad is loaded
            if (adCounter >= 2 && AdLoaded && InterstitialHandler != null)
            {
                // Drop the counter so an ad will not display next time
                adCounter = -1;

                AdLoaded = false;
                InterstitialHandler.Show(MainGame.Activity);
            }
        }
    }
}