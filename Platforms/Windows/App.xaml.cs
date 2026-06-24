#if WINDOWS
namespace SubstitutivaJogoVelhaMVVM.WinUI;

// Classe inicial do app no Windows.
// O #if WINDOWS evita erro quando o alvo selecionado for Android.
public partial class App : MauiWinUIApplication
{
    public App()
    {
        InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
#endif
