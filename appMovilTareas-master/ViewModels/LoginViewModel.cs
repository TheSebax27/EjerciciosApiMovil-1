using System;
using System.Windows.Input;
using System.Threading.Tasks;
using appMovilTareas.Service;
using appMovilTareas.Views;
using Microsoft.Maui.Controls;

namespace appMovilTareas.ViewModels
{
    public class LoginViewModel : BindableObject
    {
        private readonly ApiService _apiService;
        private readonly INavigation _navigation;

        private string _documento;
        private string _clave;
        private string _mensaje;

        public string Documento
        {
            get => _documento;
            set
            {
                _documento = value;
                OnPropertyChanged();
            }
        }

        public string Clave
        {
            get => _clave;
            set
            {
                _clave = value;
                OnPropertyChanged();
            }
        }

        public string Mensaje
        {
            get => _mensaje;
            set
            {
                _mensaje = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(INavigation navigation)
        {
            _apiService = new ApiService();
            _navigation = navigation;
            LoginCommand = new Command(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            try
            {
                var response = await _apiService.Login(Documento, Clave);

                if (response.Success)
                {
                    Console.WriteLine($"🟢 Login exitoso. Usuario ID: {response.IdCliente}");
                    await _navigation.PushAsync(new TaskPage(response.IdCliente));
                }
                else
                {
                    Mensaje = "❌ Usuario o clave incorrectos";
                }
            }
            catch (Exception ex)
            {
                Mensaje = "❌ Error en el inicio de sesión";
                Console.WriteLine($"🔴 Error: {ex.Message}");
            }
        }
    }
}
