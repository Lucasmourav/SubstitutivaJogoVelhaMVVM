#if ANDROID
using Android.App;
using Android.Runtime;

namespace SubstitutivaJogoVelhaMVVM;

// Classe inicial do app no Android.
// O #if ANDROID impede que o Windows tente compilar referências Android.
[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
#endif
