using Microsoft.Extensions.Logging;

namespace SubstitutivaJogoVelhaMVVM;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                // A prova não exige fonte externa. Usamos fonte padrão do sistema.
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
