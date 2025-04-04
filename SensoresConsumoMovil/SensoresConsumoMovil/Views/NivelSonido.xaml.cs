using SensoresConsumoMovil.ViewModel;

namespace SensoresConsumoMovil.Views;

public partial class NivelSonido : ContentPage
{
    public NivelSonido()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Execute the command to check for sound alerts
        if (BindingContext is AlertaSonidoViewModel viewModel)
        {
            await Task.Delay(500); // Small delay to ensure view is fully loaded
            viewModel.CheckSonidoCommand.Execute(null);
        }
    }
}