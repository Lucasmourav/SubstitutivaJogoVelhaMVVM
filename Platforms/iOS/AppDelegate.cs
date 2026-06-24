#if IOS
using Foundation;

namespace SubstitutivaJogoVelhaMVVM;

// Arquivo usado apenas em iOS.
// Em prova no Windows, ele fica ignorado e não gera erro de Foundation.
[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
#endif
