#if MACCATALYST
using Foundation;

namespace SubstitutivaJogoVelhaMVVM;

// Arquivo usado apenas em MacCatalyst.
// Em Windows/Android, ele não deve ser compilado.
[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
#endif
