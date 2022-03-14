namespace BigoAds
{
#if UNITY_ANDROID
    using UnityEngine;
    public class BaseJavaRef
    {
        private AndroidJavaObject javaObj;

        internal AndroidJavaObject GetJavaObject()
        {
            return javaObj;
        }

        protected BaseJavaRef(AndroidJavaObject obj)
        {
            this.javaObj = obj;
        }
    }
#endif  
}
