using SubstitutivaJogoVelhaMVVM.Services;

namespace SubstitutivaJogoVelhaMVVM;

public partial class App : Application
{
    // Serviço único de banco para todo o aplicativo.
    // Ele fica estático para facilitar o uso nas telas sem precisar configurar injeção de dependência.
    public static DatabaseService Database { get; private set; } = null!;

    public App()
    {
        InitializeComponent();

        // FileSystem.AppDataDirectory aponta para uma pasta segura do app no celular/emulador.
        // O arquivo .db3 será criado automaticamente na primeira execução.
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "sub_jogo_velha.db3");
        Database = new DatabaseService(dbPath);

        MainPage = new AppShell();
    }
}
