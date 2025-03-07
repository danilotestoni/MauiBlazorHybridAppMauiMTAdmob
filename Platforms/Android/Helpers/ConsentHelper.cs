using Android.App;
using Android.Content;
using Android.Util;
using Plugin.MauiMTAdmob.Extra;
using Xamarin.Google.UserMesssagingPlatform;
using static Xamarin.Google.UserMesssagingPlatform.UserMessagingPlatform;

namespace MauiBlazorHybridAppMauiMTAdmob.Platforms.Android
{
    public class ConsentHelper
    {
        private readonly Activity _activity;
        private readonly IConsentInformation _consentInformation;

        public ConsentHelper(Activity activity)
        {
            _activity = activity;
            _consentInformation = UserMessagingPlatform.GetConsentInformation(_activity);
        }

        // Propiedad pública para obtener el estado de consentimiento
        public int ConsentStatus => _consentInformation.ConsentStatus;

        public void RequestConsentInfoUpdate()
        {
            var paramsObject = new ConsentRequestParameters.Builder().Build();

            _consentInformation.RequestConsentInfoUpdate(
                _activity,
                paramsObject,
                new ConsentInfoUpdateCallback(this),
                new ConsentInfoUpdateCallback(this)
            );
        }

        // Callback adaptado para decidir si mostramos el formulario
        private class ConsentInfoUpdateCallback : Java.Lang.Object,
            IConsentInformationOnConsentInfoUpdateSuccessListener,
            IConsentInformationOnConsentInfoUpdateFailureListener
        {
            private readonly ConsentHelper _helper;

            public ConsentInfoUpdateCallback(ConsentHelper helper)
            {
                _helper = helper;
            }

            public void OnConsentInfoUpdateSuccess()
            {
                Log.Debug("ConsentHelper", "Consent info update success");

                // Guardamos el estado actual en SharedPreferences para futuras referencias
                _helper.StoreConsentStatus(_helper._consentInformation.ConsentStatus);

                var status = (ConsentStatus)_helper._consentInformation.ConsentStatus;
                Log.Debug("ConsentHelper", $"ConsentStatus actual: {status}");

                if (status == Plugin.MauiMTAdmob.Extra.ConsentStatus.Required || status == Plugin.MauiMTAdmob.Extra.ConsentStatus.Unknown)
                {
                    if (_helper._consentInformation.IsConsentFormAvailable)
                    {
                        Log.Debug("ConsentHelper", "Mostrando formulario de consentimiento porque es requerido o desconocido.");
                        _helper.LoadAndShowConsentForm(_helper);
                    }
                }
                else
                {
                    Log.Debug("ConsentHelper", "Formulario de consentimiento NO necesario.");
                }
            }

            public void OnConsentInfoUpdateFailure(FormError error)
            {
                Log.Error("ConsentHelper", $"Consent info update failed: {error?.Message}");
            }
        }

        public void LoadAndShowConsentForm(ConsentHelper helper)
        {
            var callback = new ConsentFormLoadCallback(helper);
            UserMessagingPlatform.LoadConsentForm(
                _activity,
                callback, // success
                callback  // failure
            );
        }

        private class ConsentFormLoadCallback : Java.Lang.Object,
            IOnConsentFormLoadSuccessListener,
            IOnConsentFormLoadFailureListener
        {
            private readonly ConsentHelper _helper;

            public ConsentFormLoadCallback(ConsentHelper helper)
            {
                _helper = helper;
            }

            public void OnConsentFormLoadSuccess(IConsentForm consentForm)
            {
                Log.Debug("ConsentHelper", "Consent form loaded successfully");
                consentForm?.Show(Platform.CurrentActivity, new ConsentFormDismissCallback(_helper)); // Pasamos _helper aquí
            }

            public void OnConsentFormLoadFailure(FormError error)
            {
                Log.Error("ConsentHelper", $"Consent form load failed: {error?.Message}");
            }
        }


        private class ConsentFormDismissCallback : Java.Lang.Object,
            IConsentFormOnConsentFormDismissedListener
        {
            private readonly ConsentHelper _helper;

            public ConsentFormDismissCallback(ConsentHelper helper)
            {
                _helper = helper;
            }

            public void OnConsentFormDismissed(FormError error)
            {
                if (error != null)
                {
                    Log.Error("ConsentHelper", $"Consent form dismissed with error: {error.Message}");
                }
                else
                {
                    Log.Debug("ConsentHelper", "Consent form dismissed successfully");

                    var currentStatus = (ConsentStatus)_helper._consentInformation.ConsentStatus;
                    Log.Debug("ConsentHelper", $"Nuevo ConsentStatus tras cierre de formulario: {currentStatus}");

                    _helper.StoreConsentStatus(_helper._consentInformation.ConsentStatus);
                }
            }
        }

        public void StoreConsentStatus(int consentStatus)
        {
            var sharedPreferences = _activity.GetSharedPreferences("UserConsent", FileCreationMode.Private);
            var editor = sharedPreferences?.Edit();
            editor?.PutInt("ConsentStatus", consentStatus);
            editor?.Apply();
            Log.Debug("ConsentHelper", $"ConsentStatus guardado: {(ConsentStatus)consentStatus}");
        }

        public ConsentStatus GetStoredConsentStatus()
        {
            var sharedPreferences = _activity.GetSharedPreferences("UserConsent", FileCreationMode.Private);
            var storedStatus = sharedPreferences?.GetInt("ConsentStatus", (int)Plugin.MauiMTAdmob.Extra.ConsentStatus.Unknown);
            return storedStatus != null ? (ConsentStatus)storedStatus : Plugin.MauiMTAdmob.Extra.ConsentStatus.Unknown;
        }

    }
}
