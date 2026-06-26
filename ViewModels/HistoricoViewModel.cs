using System.Collections.ObjectModel;
using System.Windows.Input;
using SubstitutivaJogoVelhaMVVM.Models;
using SubstitutivaJogoVelhaMVVM.Services;

namespace SubstitutivaJogoVelhaMVVM.ViewModels;

public class HistoricoViewModel : BaseViewModel
{
    private readonly DatabaseService _database;

    // Valores iniciais dos filtros: mostra o último mês quando a tela abre.
    private DateTime _dataInicio = DateTime.Today.AddDays(-30);
    private DateTime _dataFim = DateTime.Today;
    private string _nomeJogadorFiltro = string.Empty;
    private string _mensagemResultado = "Use os filtros e toque em Buscar.";

    // ObservableCollection notifica a CollectionView quando itens entram ou saem da lista.
    public ObservableCollection<Partida> Partidas { get; } = new();

    public DateTime DataInicio
    {
        get => _dataInicio;
        set => SetProperty(ref _dataInicio, value);
    }

    public DateTime DataFim
    {
        get => _dataFim;
        set => SetProperty(ref _dataFim, value);
    }

    public string NomeJogadorFiltro
    {
        get => _nomeJogadorFiltro;
        set => SetProperty(ref _nomeJogadorFiltro, value);
    }

    public string MensagemResultado
    {
        get => _mensagemResultado;
        set => SetProperty(ref _mensagemResultado, value);
    }

    public ICommand BuscarCommand { get; }
    public ICommand LimparCommand { get; }
    public ICommand VoltarCommand { get; }

    public HistoricoViewModel(DatabaseService database)
    {
        _database = database;
        BuscarCommand = new Command(async () => await BuscarAsync());
        LimparCommand = new Command(async () => await LimparFiltrosAsync());
        VoltarCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public async Task BuscarAsync()
    {
        // A ViewModel valida o intervalo antes de consultar o banco.
        if (DataFim.Date < DataInicio.Date)
        {
            MensagemResultado = "A data final não pode ser menor que a data inicial.";
            return;
        }

        var resultado = await _database.FiltrarPartidasAsync(DataInicio, DataFim, NomeJogadorFiltro);

        // Atualiza a mesma coleção para a CollectionView refletir os resultados na tela.
        Partidas.Clear();
        foreach (var partida in resultado)
            Partidas.Add(partida);

        MensagemResultado = resultado.Count == 1
            ? "1 partida encontrada."
            : $"{resultado.Count} partidas encontradas.";
    }

    private async Task LimparFiltrosAsync()
    {
        NomeJogadorFiltro = string.Empty;
        DataInicio = DateTime.Today.AddDays(-30);
        DataFim = DateTime.Today;
        await BuscarAsync();
    }
}
