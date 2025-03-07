# MauiBlazorHybridAppMauiMTAdmob

## Prop�sito
Esta aplicaci�n de prueba, con el nombre tan largo ;D, tiene como objetivo la investigaci�n y el desarrollo utilizando la plataforma .NET MAUI junto con Blazor Hybrid y la integraci�n de anuncios de Google AdMob. La aplicaci�n est� dise�ada para explorar las capacidades de estas tecnolog�as y c�mo se pueden combinar para crear aplicaciones multiplataforma.

## Funcionalidades
- **Integraci�n de MAUI y Blazor Hybrid**: Utiliza .NET MAUI para la creaci�n de interfaces de usuario nativas y Blazor para la l�gica de la aplicaci�n y la renderizaci�n de componentes web.
- **Anuncios de Google AdMob**: Implementa el plugin `Plugin.MauiMTAdmob` para mostrar anuncios de Google AdMob en la aplicaci�n.
- **Gesti�n de consentimiento**: Incluye un sistema para solicitar y gestionar el consentimiento del usuario para la personalizaci�n de anuncios, utilizando la plataforma de mensajer�a de usuario de Google.

## Versiones Incluidas
- **.NET**: 8.0
- **Plugin.MauiMTAdmob**: 2.0.0.5

## Problemas Conocidos
### iOS
Actualmente, la aplicaci�n no funciona en iOS debido a un error al intentar instalar y compilar el plugin `MTAdmob.Google.MobileAds`. El error espec�fico es:
No se puede encontrar una parte de la ruta de acceso 'C:\Users\XXXXXX\.nuget\packages\mtadmob.ios.binding\11.2.0.1\lib\net8.0-ios18.0\MTAdmob.Google.MobileAds.resources\GoogleMobileAds.xcframework\ios-arm64_x86_64-simulator\GoogleMobileAds.framework\Headers\Mediation\GADMediatedUnifiedNativeAdNotificationSource.h'.
Por lo tanto se decide quitar el valor net8.0-ios de la propiedad TargetFrameworks.

## Necesidades y retos detectados
- **Soporte para iOS**: Se necesita investigar y resolver el problema de compilaci�n en iOS.
- **Soporte para Android**: Aunque la aplicaci�n funciona en Android, es necesario realizar investigaciones adicionales para conseguir insertar los anuncios de Google AdMob dentro de los componentes de Blazor en tiempo de ejecuci�n.

## Dudas y preguntas
La aplicaci�n es un proyecto de prueba y no est� destinada a ser utilizada en producci�n. Si tienes alguna pregunta o duda, te remito a revisar la documentaci�n oficial de .NET MAUI, Blazor Hybrid y el plugin `Plugin.MauiMTAdmob`.
El plugin `Plugin.MauiMTAdmob` es un proyecto que puedes encontrar en GitHub: https://github.com/marcojak/MauiMTAdmob
