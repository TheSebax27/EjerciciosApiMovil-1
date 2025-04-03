using ProductoConsumoMovil.Models;
using ProductoConsumoMovil.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductoConsumoMovil.ViewModel
{
    public class CategoriasViewModel : BindableObject
    {
        private readonly ApiService _apiService;
        public ObservableCollection<Categoria> Categorias { get; set; }
        public ICommand CargarCategoriasCommand { get; set; }
        public ICommand AgregarCategoriasCommand { get; set; }

        private Categoria _nuevaCategoria = new Categoria();
        public Categoria nuevaCategoria
        {
            get => _nuevaCategoria;
            set
            {
                _nuevaCategoria = value;
                OnPropertyChanged(nameof(nuevaCategoria));
            }
        }

        public CategoriasViewModel()
        {
            _apiService = new ApiService();
            Categorias = new ObservableCollection<Categoria>();
            CargarCategoriasCommand = new Command(async () => await CargarCategorias());
            AgregarCategoriasCommand = new Command(async () => await AgregarCategoria());
        }

        public async Task AgregarCategoria()
        {
            if (string.IsNullOrWhiteSpace(nuevaCategoria.nombreCategoria))
            {
                await App.Current.MainPage.DisplayAlert("Error", "El nombre de la categoría es obligatorio", "Ok");
                return;
            }

            bool success = await _apiService.PostCategoriaAsync(nuevaCategoria);
            if (success)
            {
                await App.Current.MainPage.DisplayAlert("Registro", "Categoría registrada correctamente", "Ok");
                nuevaCategoria = new Categoria();
                await CargarCategorias();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error al registrar la categoría", "Ok");
            }
        }

        public async Task CargarCategorias()
        {
            try
            {
                var categorias = await _apiService.GetCategoriaAsync();
                Categorias.Clear();
                foreach (var item in categorias)
                {
                    Categorias.Add(item);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al cargar categorías: {ex.Message}", "Ok");
            }
        }
    }
}