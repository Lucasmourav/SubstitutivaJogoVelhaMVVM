using SQLite;
using SQLitePCL;
using SubstitutivaJogoVelhaMVVM.Models;

namespace SubstitutivaJogoVelhaMVVM.Services;

// Serviço responsável por todo acesso ao SQLite.
// A ViewModel chama este serviço, e a View nunca acessa banco diretamente.
public class DatabaseService
{
    private readonly SQLiteAsyncConnection _database;
    private readonly SemaphoreSlim _inicializacaoLock = new(1, 1);
    private bool _inicializado;

    public DatabaseService(string dbPath)
    {
        // Inicializa o provedor nativo do SQLite.
        // Ajuda principalmente no Android/Windows quando usamos SQLitePCLRaw.bundle_green.
        Batteries_V2.Init();

        _database = new SQLiteAsyncConnection(dbPath);
    }

    private async Task InicializarAsync()
    {
        if (_inicializado)
            return;

        // Protege a primeira inicialização caso jogo e histórico chamem o banco ao mesmo tempo.
        await _inicializacaoLock.WaitAsync();
        try
        {
            if (_inicializado)
                return;

            // Cria a tabela Partida caso ela ainda não exista.
            await _database.CreateTableAsync<Partida>();
            _inicializado = true;
        }
        finally
        {
            _inicializacaoLock.Release();
        }
    }

    public async Task<int> SalvarPartidaAsync(Partida partida)
    {
        await InicializarAsync();

        // InsertAsync grava uma nova linha na tabela.
        return await _database.InsertAsync(partida);
    }

    public async Task<List<Partida>> ListarPartidasAsync()
    {
        await InicializarAsync();

        return await _database.Table<Partida>()
            .OrderByDescending(p => p.DataPartida)
            .ToListAsync();
    }

    public async Task<List<Partida>> FiltrarPartidasAsync(DateTime dataInicio, DateTime dataFim, string? nomeJogador)
    {
        await InicializarAsync();

        // Para evitar problema com hora, buscamos até o final do dia escolhido.
        var inicio = dataInicio.Date;
        var fim = dataFim.Date.AddDays(1).AddTicks(-1);
        var nome = (nomeJogador ?? string.Empty).Trim().ToLower();

        // sqlite-net permite consultas simples, mas para filtro com Contains ignorando maiúsculas,
        // trazemos a lista e filtramos em memória. Para prova acadêmica fica claro e fácil de explicar.
        var todas = await ListarPartidasAsync();

        return todas
            .Where(p => p.DataPartida >= inicio && p.DataPartida <= fim)
            .Where(p => string.IsNullOrWhiteSpace(nome)
                || p.JogadorBolinha.ToLower().Contains(nome)
                || p.JogadorX.ToLower().Contains(nome)
                || p.Vencedor.ToLower().Contains(nome))
            .OrderByDescending(p => p.DataPartida)
            .ToList();
    }
}
