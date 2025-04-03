using ProductoConsumoMovil.ViewModel;

namespace ProductoConsumoMovil.Views;

public partial class ListarCategorias : ContentPage
{
    private CategoriasViewModel _viewModel;

    public ListarCategorias()
    {
        InitializeComponent();
        _viewModel = (CategoriasViewModel)BindingContext;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.CargarCategorias();
    }
}