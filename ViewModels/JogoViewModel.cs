using System.Windows.Input;
using SubstitutivaJogoVelhaMVVM.Models;
using SubstitutivaJogoVelhaMVVM.Services;

namespace SubstitutivaJogoVelhaMVVM.ViewModels;

public class JogoViewModel : BaseViewModel
{
    private readonly DatabaseService _database;
    private readonly string[] _casas = new string[9];

    private string _nomeJogadorBolinha = string.Empty;
    private string _nomeJogadorX = string.Empty;
    private string _jogadorAtual = "X";
    private string _mensagem = "Informe os nomes e inicie uma partida.";
    private int _quantidadeJogadas;
    private bool _partidaIniciada;
    private bool _partidaEncerrada;

    public string Casa0 => _casas[0];
    public string Casa1 => _casas[1];
    public string Casa2 => _casas[2];
    public string Casa3 => _casas[3];
    public string Casa4 => _casas[4];
    public string Casa5 => _casas[5];
    public string Casa6 => _casas[6];
    public string Casa7 => _casas[7];
    public string Casa8 => _casas[8];

    public string NomeJogadorBolinha
    {
        get => _nomeJogadorBolinha;
        set => SetProperty(ref _nomeJogadorBolinha, value);
    }

    public string NomeJogadorX
    {
        get => _nomeJogadorX;
        set => SetProperty(ref _nomeJogadorX, value);
    }

    public string JogadorAtual
    {
        get => _jogadorAtual;
        set => SetProperty(ref _jogadorAtual, value);
    }

    public string Mensagem
    {
        get => _mensagem;
        set => SetProperty(ref _mensagem, value);
    }

    public int QuantidadeJogadas
    {
        get => _quantidadeJogadas;
        set => SetProperty(ref _quantidadeJogadas, value);
    }

    public bool PartidaIniciada
    {
        get => _partidaIniciada;
        set
        {
            if (SetProperty(ref _partidaIniciada, value))
                OnPropertyChanged(nameof(TabuleiroAtivo));
        }
    }

    public bool PartidaEncerrada
    {
        get => _partidaEncerrada;
        set
        {
            if (SetProperty(ref _partidaEncerrada, value))
                OnPropertyChanged(nameof(TabuleiroAtivo));
        }
    }

    public bool TabuleiroAtivo => PartidaIniciada && !PartidaEncerrada;

    public ICommand IniciarPartidaCommand { get; }
    public ICommand JogarCommand { get; }
    public ICommand NovaPartidaCommand { get; }
    public ICommand AbrirHistoricoCommand { get; }

    public JogoViewModel(DatabaseService database)
    {
        _database = database;

        IniciarPartidaCommand = new Command(IniciarPartida);
        JogarCommand = new Command(async parametro => await JogarAsync(parametro));
        NovaPartidaCommand = new Command(LimparTabuleiro);
        AbrirHistoricoCommand = new Command(async () => await Shell.Current.GoToAsync("historico"));
    }

    private void IniciarPartida()
    {
        if (string.IsNullOrWhiteSpace(NomeJogadorBolinha) || string.IsNullOrWhiteSpace(NomeJogadorX))
        {
            Mensagem = "Preencha o nome dos dois jogadores antes de iniciar.";
            return;
        }

        if (NomeJogadorBolinha.Trim().Equals(NomeJogadorX.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            Mensagem = "Use nomes diferentes para facilitar o filtro no histórico.";
            return;
        }

        LimparTabuleiro();
        PartidaIniciada = true;
        PartidaEncerrada = false;
        JogadorAtual = "X"; // No jogo da velha, X costuma iniciar.
        Mensagem = $"Partida iniciada. Vez de {NomeJogadorX} (X).";
    }

    private void LimparTabuleiro()
    {
        for (int i = 0; i < _casas.Length; i++)
            DefinirCasa(i, string.Empty);

        QuantidadeJogadas = 0;
        JogadorAtual = "X";
        PartidaEncerrada = false;
        PartidaIniciada = false;
        Mensagem = "Informe os nomes e inicie uma partida.";
        OnPropertyChanged(nameof(TabuleiroAtivo));
    }

    private async Task JogarAsync(object? parametro)
    {
        if (!TabuleiroAtivo)
            return;

        if (!TryObterPosicao(parametro, out int posicao))
        {
            Mensagem = "Jogada inválida.";
            return;
        }

        // Não permite jogar em casa já ocupada.
        if (!string.IsNullOrWhiteSpace(_casas[posicao]))
            return;

        DefinirCasa(posicao, JogadorAtual);
        QuantidadeJogadas++;

        if (VerificarVitoria(JogadorAtual))
        {
            string vencedorNome = JogadorAtual == "X" ? NomeJogadorX : NomeJogadorBolinha;
            await FinalizarPartidaAsync(vencedorNome, JogadorAtual);
            return;
        }

        if (QuantidadeJogadas == 9)
        {
            await FinalizarPartidaAsync("Empate", "-");
            return;
        }

        // Alterna o jogador atual.
        JogadorAtual = JogadorAtual == "X" ? "O" : "X";
        string nomeDaVez = JogadorAtual == "X" ? NomeJogadorX : NomeJogadorBolinha;
        Mensagem = $"Vez de {nomeDaVez} ({JogadorAtual}). Jogadas: {QuantidadeJogadas}.";
    }

    private bool TryObterPosicao(object? parametro, out int posicao)
    {
        posicao = parametro switch
        {
            int valor => valor,
            string texto when int.TryParse(texto, out var valor) => valor,
            _ => -1
        };

        return posicao >= 0 && posicao < _casas.Length;
    }

    private void DefinirCasa(int posicao, string simbolo)
    {
        _casas[posicao] = simbolo;
        OnPropertyChanged($"Casa{posicao}");
    }

    private bool VerificarVitoria(string simbolo)
    {
        // Todas as combinações possíveis de vitória: linhas, colunas e diagonais.
        int[,] combinacoes =
        {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8},
            {0, 4, 8}, {2, 4, 6}
        };

        for (int i = 0; i < combinacoes.GetLength(0); i++)
        {
            int a = combinacoes[i, 0];
            int b = combinacoes[i, 1];
            int c = combinacoes[i, 2];

            if (_casas[a] == simbolo && _casas[b] == simbolo && _casas[c] == simbolo)
                return true;
        }

        return false;
    }

    private async Task FinalizarPartidaAsync(string vencedor, string simboloVencedor)
    {
        PartidaEncerrada = true;
        OnPropertyChanged(nameof(TabuleiroAtivo));

        var partida = new Partida
        {
            JogadorBolinha = NomeJogadorBolinha.Trim(),
            JogadorX = NomeJogadorX.Trim(),
            DataPartida = DateTime.Now,
            Vencedor = vencedor,
            SimboloVencedor = simboloVencedor,
            QuantidadeJogadas = QuantidadeJogadas
        };

        await _database.SalvarPartidaAsync(partida);

        Mensagem = simboloVencedor == "-"
            ? $"Empate salvo no SQLite com {QuantidadeJogadas} jogadas."
            : $"{vencedor} venceu com {QuantidadeJogadas} jogadas. Partida salva no SQLite.";
    }
}
