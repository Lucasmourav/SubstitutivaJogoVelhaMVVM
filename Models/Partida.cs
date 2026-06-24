using SQLite;

namespace SubstitutivaJogoVelhaMVVM.Models;

// Modelo representa a tabela do SQLite.
// Cada propriedade vira uma coluna dentro da tabela Partida.
public class Partida
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    // Nome informado para quem joga com bolinha (O).
    [NotNull]
    public string JogadorBolinha { get; set; } = string.Empty;

    // Nome informado para quem joga com X.
    [NotNull]
    public string JogadorX { get; set; } = string.Empty;

    // Data e hora exata em que a partida foi finalizada e salva.
    public DateTime DataPartida { get; set; }

    // Pode ser o nome de um jogador ou a palavra "Empate".
    [NotNull]
    public string Vencedor { get; set; } = string.Empty;

    // X, O ou - quando empatar.
    [NotNull]
    public string SimboloVencedor { get; set; } = string.Empty;

    // Quantidade total de jogadas feitas até terminar.
    public int QuantidadeJogadas { get; set; }
}
