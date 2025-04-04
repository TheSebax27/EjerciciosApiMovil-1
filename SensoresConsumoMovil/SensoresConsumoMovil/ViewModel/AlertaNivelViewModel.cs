using SensoresConsumoMovil.Models;
using SensoresConsumoMovil.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SensoresConsumoMovil.ViewModel
{
    public class AlertaNivelViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private AlertaNivel _alertaNivel;
        private string _statusMessage;
        private bool _isLoading;

        public AlertaNivelViewModel()
        {
            _apiService = new ApiService();
            CheckAlertaCommand = new Command(async () => await CheckAlerta());
            _alertaNivel = new AlertaNivel { NivelAlerta = "sin información" };
        }

        public AlertaNivel AlertaNivel
        {
            get => _alertaNivel;
            set
            {
                if (_alertaNivel != value)
                {
                    _alertaNivel = value;
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

        public ICommand CheckAlertaCommand { get; }

        private async Task CheckAlerta()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Consultando nivel de alerta...";

                var alerta = await _apiService.GetAlertaNivelAsync();
                if (alerta != null)
                {
                    AlertaNivel = alerta;
                    await ActivateVibration(alerta.NivelAlerta);
                    StatusMessage = $"Nivel de alerta: {alerta.NivelAlerta}";
                }
                else
                {
                    StatusMessage = "No se pudo obtener la alerta";
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

        private async Task ActivateVibration(string nivel)
        {
            try
            {
                TimeSpan duration;

                switch (nivel.ToLower())
                {
                    case "bajo":
                        duration = TimeSpan.FromSeconds(1);
                        break;
                    case "medio":
                        duration = TimeSpan.FromSeconds(3);
                        break;
                    case "alto":
                        duration = TimeSpan.FromSeconds(5);
                        break;
                    default:
                        return;
                }

                // Activate vibration
                if (duration > TimeSpan.Zero)
                {
                    // Removed problematic DeviceDisplay line
                    Vibration.Default.Vibrate(duration);
                    StatusMessage = $"Vibrando por {duration.TotalSeconds} segundos";

                    // Give time for vibration to complete
                    await Task.Delay(duration);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al vibrar: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}