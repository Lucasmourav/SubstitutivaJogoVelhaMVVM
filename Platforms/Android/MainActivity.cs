#if ANDROID
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace SubstitutivaJogoVelhaMVVM;

// Este arquivo só deve ser compilado quando o alvo for Android.
// O #if ANDROID evita erros no Visual Studio quando o alvo selecionado é Windows.
[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
}
#endif
