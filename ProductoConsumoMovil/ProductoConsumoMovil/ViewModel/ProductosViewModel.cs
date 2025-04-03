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
    class ProductosViewModel : BindableObject
    {
        private readonly ApiService _apiService;
        public ObservableCollection<Producto> Productos { get; set; }
        public ObservableCollection<Categoria> Categorias { get; set; }

        public ICommand CargarProductosCommand { get; set; }

        public ICommand AgregarProductosCommand { get; set; }

        public Categoria oCategoria { get; set; }

        public Producto nuevoProducto { get; set; } = new Producto();



        public ProductosViewModel()
        {
            _apiService = new ApiService();
            Productos = new ObservableCollection<Producto>();
            CargarProductosCommand = new Command(async () => await CargarProductos());
            AgregarProductosCommand = new Command(async () => await AgregarProducto());
            Categorias = new ObservableCollection<Categoria>();
            _ = CargarCategorias();

        }

        public async Task AgregarProducto()
        {
            nuevoProducto.idCategoria = oCategoria.IdCategoria;
            bool success = await _apiService.PostProductoAsync(nuevoProducto);

            if (success)
            {
                await App.Current.MainPage.DisplayAlert("Registro", "Producto Registrado correctamente", "Ok");
                nuevoProducto = new Producto();
                oCategoria = null;
                OnPropertyChanged(nameof(nuevoProducto));
                OnPropertyChanged(nameof(oCategoria));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Registro", "Error al registrar el producto", "Ok");
            }
        }

        public async Task CargarProductos()
        {
            var productos = await _apiService.GetProductosAsync();
            Productos.Clear();

            foreach (var item in productos)
            {
                Productos.Add(item);
            }

        }

        public async Task CargarCategorias()
        {
            var categorias = await _apiService.GetCategoriaAsync();
            Categorias.Clear();
            foreach (var item in categorias)
            {
                Categorias.Add(item);
            }
        }
    }
}