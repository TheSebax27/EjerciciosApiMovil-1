using SensoresConsumoMovil.ViewModel;

namespace SensoresConsumoMovil.Views;

public partial class AlertaGPS : ContentPage
{
    public AlertaGPS()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Execute the command to check for GPS alerts
        if (BindingContext is AlertaGpsViewModel viewModel)
        {
            await Task.Delay(500); // Small delay to ensure view is fully loaded
            viewModel.CheckGpsCommand.Execute(null);
        }
    }
}