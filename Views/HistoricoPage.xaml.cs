using SubstitutivaJogoVelhaMVVM.ViewModels;

namespace SubstitutivaJogoVelhaMVVM.Views;

public partial class HistoricoPage : ContentPage
{
    private readonly HistoricoViewModel _viewModel;

    public HistoricoPage()
    {
        InitializeComponent();
        _viewModel = new HistoricoViewModel(App.Database);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Ao abrir a tela, já carrega os registros salvos.
        // A busca em si continua na ViewModel.
        await _viewModel.BuscarAsync();
    }
}
