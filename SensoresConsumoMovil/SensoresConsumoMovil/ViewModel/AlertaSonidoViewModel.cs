using SensoresConsumoMovil.Models;
using SensoresConsumoMovil.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Core.Views;

namespace SensoresConsumoMovil.ViewModel
{
    public class AlertaSonidoViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private AlertaSonido _alertaSonido;
        private string _statusMessage;
        private bool _isLoading;

        public AlertaSonidoViewModel()
        {
            _apiService = new ApiService();
            CheckSonidoCommand = new Command(async () => await CheckSonido());
            _alertaSonido = new AlertaSonido { MensajeEmergencia = "Sin información" };
        }

        public AlertaSonido AlertaSonido
        {
            get => _alertaSonido;
            set
            {
                if (_alertaSonido != value)
                {
                    _alertaSonido = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand CheckSonidoCommand { get; }

        private async Task CheckSonido()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Consultando alertas de sonido...";
                var alerta = await _apiService.GetAlertaSonidoAsync();
                if (alerta != null)
                {
                    AlertaSonido = alerta;
                    if (alerta.MensajeEmergencia.ToLower().Contains("alarma"))
                    {
                        await PlayAlarmSound();
                    }
                    StatusMessage = alerta.MensajeEmergencia;
                }
                else
                {
                    StatusMessage = "No se pudo obtener la alerta de sonido";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task PlayAlarmSound()
        {
            try
            {
                // Opción 1: Usando MediaElement de CommunityToolkit.Maui
                var mediaElement = new MediaElement
                {
                    Source = MediaSource.FromResource("alarm.mp3"),
                    ShouldAutoPlay = true,
                    Volume = 1.0
                };

                // Añadir a la página actual (asumiendo que tienes acceso a la página)
                // Application.Current.MainPage.Content = mediaElement; // Esto es solo un ejemplo

                // O reproduzca el audio mediante otros medios sin usar MediaElement
                // Por ejemplo, puedes usar DependencyService con una interfaz personalizada

                StatusMessage = "Reproduciendo sonido de alarma";
                // Dar tiempo para reproducir el sonido
                await Task.Delay(3000);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al reproducir sonido: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}