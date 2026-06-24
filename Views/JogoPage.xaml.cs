using SubstitutivaJogoVelhaMVVM.ViewModels;

namespace SubstitutivaJogoVelhaMVVM.Views;

public partial class JogoPage : ContentPage
{
    public JogoPage()
    {
        InitializeComponent();

        // A tela só cria a ViewModel e entrega o serviço de banco.
        // As regras do jogo e do SQLite ficam fora da View.
        BindingContext = new JogoViewModel(App.Database);
    }
}
