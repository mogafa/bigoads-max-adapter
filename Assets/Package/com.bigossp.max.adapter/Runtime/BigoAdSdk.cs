namespace BigoAds
{
#if UNITY_ANDROID
    using UnityEngine;
    public class AdConfig : BaseJavaRef
    {
        private AdConfig(AndroidJavaObject obj) : base(obj) { }

        public string GetAppId()
        {
            return GetJavaObject().Call<string>("getAppKey");
        }

        public string GetChannel()
        {
            return GetJavaObject().Call<string>("getChannel");
        }

        public bool IsDebug()
        {
            return GetJavaObject().Call<bool>("isDebug");
        }

        public class Builder : BaseJavaRef
        {
            public Builder() : base(new AndroidJavaObject("sg.bigo.ads.api.AdConfig$Builder")) { }

            public Builder SetAppId(string appId)
            {
                GetJavaObject().Call<AndroidJavaObject>("setAppId", appId);
                return this;
            }

            public Builder SetChannel(string channel)
            {
                GetJavaObject().Call<AndroidJavaObject>("setChannel", channel);
                return this;
            }

            public Builder SetDebug(bool debug)
            {
                GetJavaObject().Call<AndroidJavaObject>("setDebug", debug);
                return this;
            }

            public AdConfig Build()
            {
                return new AdConfig(GetJavaObject().Call<AndroidJavaObject>("build"));
            }
        }
    }

    public class BigoAdsdk
    {
        public interface InitListener
        {
            void OnInitialized();
        }

        #region public methods
        public static void Initialize(AdConfig config, InitListener listener)
        {
            var javaListener = new JavaInitListener(listener);
            GetBigoAdSDK().CallStatic("initialize", GetActivity(), config.GetJavaObject(), javaListener);
        }

        public static bool IsInitialized()
        {
            return GetBigoAdSDK().CallStatic<bool>("isInitialized");
        }

        public static string GetSDKVersion()
        {
            return GetBigoAdSDK().CallStatic<string>("getSDKVersion");
        }

        public static string GetSDKVersionName()
        {
            return GetBigoAdSDK().CallStatic<string>("getSDKVersionName");
        }

        public static void SetUserConsent(ConsentOptions option, bool consent)
        {
            AndroidJavaClass clazz = new AndroidJavaClass("sg.bigo.ads.ConsentOptions");
            AndroidJavaObject obj = null;
            switch (option)
            {
                case ConsentOptions.GDPR:
                    obj = clazz.GetStatic<AndroidJavaObject>("GDPR");
                    break;
                case ConsentOptions.CCPA:
                    obj = clazz.GetStatic<AndroidJavaObject>("CCPA");
                    break;
            }
            GetBigoAdSDK().CallStatic("setUserConsent", GetActivity(), obj, consent);
        }
        #endregion

        #region private implementations
        private class JavaInitListener : AndroidJavaProxy
        {
            private InitListener listener;

            public JavaInitListener(InitListener listener) : base("sg.bigo.ads.BigoAdSdk$InitListener")
            {
                this.listener = listener;
            }

            public void onInitialized()
            {
                listener.OnInitialized();
            }
        }

        private static AndroidJavaObject GetBigoAdSDK()
        {
            return new AndroidJavaClass("sg.bigo.ads.BigoAdSdk");
        }

        private static AndroidJavaObject GetActivity()
        {
            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
        #endregion
    }

    public enum ConsentOptions
    {
        GDPR,
        CCPA
    }
#endif  
}
