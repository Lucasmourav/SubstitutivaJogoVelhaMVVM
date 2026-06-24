using System.Windows.Input;

namespace SubstitutivaJogoVelhaMVVM.ViewModels;

public class HomeViewModel : BaseViewModel
{
    public ICommand AbrirJogoCommand { get; }
    public ICommand AbrirHistoricoCommand { get; }

    public HomeViewModel()
    {
        // Navegação por rotas do Shell, como pedido no enunciado.
        AbrirJogoCommand = new Command(async () => await Shell.Current.GoToAsync("jogo"));
        AbrirHistoricoCommand = new Command(async () => await Shell.Current.GoToAsync("historico"));
    }
}
