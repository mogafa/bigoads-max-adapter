# BigoAd Max Unity Adapter

## 安装
从Unity的Window菜单，选择Package Manager，从My registeries中选择：com.bigossp.max.adapter.unity 安装。

## 初始化
一定要在初始化Max之前初始化BigoAd：

```csharp
#if UNITY_ANDROID
        var adConfig = new AdConfig.Builder()
                    .SetAppId("YOUR_BIGOAD_APP_ID")
                    .SetDebug(true) //需要查看详细日志设置为true
                    .Build();
        BigoAdsdk.Initialize(adConfig, new SDKInitListener());
#endif
```
SDKInitListener类定义如下：
```csharp
public class SDKInitListener : MogafaBase, BigoAdsdk.InitListener
{
    public void OnInitialized()
    {
        Logger.LogDebug("IsInitialized: " + BigoAdsdk.IsInitialized());
        Logger.LogDebug("SDK Version: " + BigoAdsdk.GetSDKVersionName() + "(" + BigoAdsdk.GetSDKVersion() + ")");
    }
}
```