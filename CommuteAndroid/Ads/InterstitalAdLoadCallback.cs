namespace CommuteAndroid.Ads
{
    using Android.Runtime;
    using System;

    /// <summary>
    /// A callback for when an interstital ad is loaded.
    /// Some code in the Google Ads library is broken, so this is required to override and fix it.
    /// </summary>
    public abstract class InterstitialAdLoadCallback : global::Android.Gms.Ads.Interstitial.InterstitialAdLoadCallback
    {
        /// <summary>
        /// The ad loaded handler.
        /// </summary>
        private static Delegate cb_onAdLoaded;

        /// <summary>
        /// Get the ad loaded handler.
        /// </summary>
        /// <returns>The ad loaded handler.</returns>
        private static Delegate GetOnAdLoadedHandler()
        {
            if (cb_onAdLoaded is null)
            {
                cb_onAdLoaded = JNINativeWrapper.CreateDelegate((Action<IntPtr, IntPtr, IntPtr>)n_onAdLoaded);
            }
            return cb_onAdLoaded;
        }

        /// <summary>
        /// Called when the ad is loaded.
        /// </summary>
        /// <param name="jnienv">A pointer for the Java native interface.</param>
        /// <param name="native__this">A pointer for this object.</param>
        /// <param name="native_p0">A pointer for the ad.</param>
        private static void n_onAdLoaded(IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
        {
            InterstitialAdLoadCallback thisobject = GetObject<InterstitialAdLoadCallback>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            global::Android.Gms.Ads.Interstitial.InterstitialAd resultobject = GetObject<global::Android.Gms.Ads.Interstitial.InterstitialAd>(native_p0, JniHandleOwnership.DoNotTransfer);
            thisobject.OnAdLoaded(resultobject);
        }

        /// <summary>
        /// Called when the ad is loaded.
        /// </summary>
        /// <param name="interstitialAd">The ad.</param>
        [Register("onAdLoaded", "(Lcom/google/android/gms/ads/interstitial/InterstitialAd;)V", "GetOnAdLoadedHandler")]
        public virtual void OnAdLoaded(global::Android.Gms.Ads.Interstitial.InterstitialAd interstitialAd)
        {
        }
    }
}