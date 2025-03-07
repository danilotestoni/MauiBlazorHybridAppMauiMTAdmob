using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.MauiMTAdmob;
using Plugin.MauiMTAdmob.Extra;

namespace MauiBlazorHybridAppMauiMTAdmob;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        string appId = "ca-app-pub-8466562235188404~8043831924"; // Reemplaza con tu AdMob App ID
        string license = null; // Reemplaza con tu clave de licencia si la tienes
        string nativeAdsId = null; // Reemplaza con tu ID de anuncios nativos
        string openAdsId = null; // Estos anuncios se muestran cuando el usuario abre tu aplicación.
        bool enableOpenAds = false; // Estos anuncios se muestran cuando el usuario abre tu aplicación.
        bool tagForUnderAgeOfConsent = false; // Establece según tus necesidades
        string testDeviceId = "123a123b-12ab-123a-1a23-1234a1b123c1"; // Reemplaza con tu ID de dispositivo de prueba (este es totalmente falso). Los emuladores de Android se configuran automáticamente como dispositivos de prueba. 
        bool forceTesting = true; // Establece según tus necesidades
        DebugGeography geography = DebugGeography.DEBUG_GEOGRAPHY_EEA; // Establece según tus necesidades. DEBUG_GEOGRAPHY_EEA es Europa
        bool initialiseConsentAtStartup = true; // Establece según tus necesidades. Si usas la versión con Licencia de MauiMTAdmob.
        bool debugMode = true; // Establece según tus necesidades

        CrossMauiMTAdmob.Current.Init(
            this,
            appId,
            license,
            nativeAdsId,
            openAdsId,
            enableOpenAds,
            tagForUnderAgeOfConsent,
            testDeviceId,
            forceTesting,
            geography,
            initialiseConsentAtStartup,
            debugMode
        );

        // ID de la unidad de anuncios de prueba.
        // En https://developers.google.com/admob/android/test-ads?hl=es-419 tienes más información.
        CrossMauiMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/1033173712");
    }

    protected override void OnResume()
    {
        base.OnResume();
        if (CrossMauiMTAdmob.Current.IsInterstitialLoaded())
        {
            CrossMauiMTAdmob.Current.ShowInterstitial();
        }
    }

    protected override void OnPause()
    {
        base.OnPause();
        if (CrossMauiMTAdmob.Current.IsInterstitialLoaded())
        {
            CrossMauiMTAdmob.Current.ShowInterstitial();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (CrossMauiMTAdmob.Current.IsInterstitialLoaded())
        {
            CrossMauiMTAdmob.Current.ShowInterstitial();
        }
    }

}
