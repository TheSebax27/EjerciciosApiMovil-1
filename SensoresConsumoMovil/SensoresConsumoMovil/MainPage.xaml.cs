namespace SensoresConsumoMovil;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnAlertaNivelClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.AlertaNivel());
    }

    private async void OnAlertaGPSClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.AlertaGPS());
    }

    private async void OnAlertaSonidoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.NivelSonido());
    }
}