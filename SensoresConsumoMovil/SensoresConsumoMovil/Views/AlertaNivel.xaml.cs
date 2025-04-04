using SensoresConsumoMovil.ViewModel;

namespace SensoresConsumoMovil.Views;

public partial class AlertaNivel : ContentPage
{
    public AlertaNivel()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Check if we have vibration permissions
        var status = await Permissions.CheckStatusAsync<Permissions.Vibrate>();
        if (status != PermissionStatus.Granted)
        {
            await Permissions.RequestAsync<Permissions.Vibrate>();
        }

        // Execute the command to check for alerts
        if (BindingContext is AlertaNivelViewModel viewModel)
        {
            await Task.Delay(500); // Small delay to ensure view is fully loaded
            viewModel.CheckAlertaCommand.Execute(null);
        }
    }
}