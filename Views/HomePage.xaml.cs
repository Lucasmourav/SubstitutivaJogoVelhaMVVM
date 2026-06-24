using SubstitutivaJogoVelhaMVVM.ViewModels;

namespace SubstitutivaJogoVelhaMVVM.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();

        // Code-behind usado apenas para conectar a View com a ViewModel.
        // A regra de negócio fica toda dentro da ViewModel.
        BindingContext = new HomeViewModel();
    }
}
