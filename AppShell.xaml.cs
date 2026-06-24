using SubstitutivaJogoVelhaMVVM.Views;

namespace SubstitutivaJogoVelhaMVVM;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Rotas exigidas no enunciado: toda troca de tela usa Shell.Current.GoToAsync("rota").
        // Se o professor pedir outra tela, basta criar a Page e registrar a nova rota aqui.
        Routing.RegisterRoute("jogo", typeof(JogoPage));
        Routing.RegisterRoute("historico", typeof(HistoricoPage));
    }
}
