using SensoresConsumoMovil.Models;
using SensoresConsumoMovil.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Plugin.LocalNotification;

namespace SensoresConsumoMovil.ViewModel
{
    public class AlertaGpsViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private AlertaGPS _alertaGPS;
        private string _statusMessage;
        private bool _isLoading;
        private bool _isGpsActive;

        public AlertaGpsViewModel()
        {
            _apiService = new ApiService();
            CheckGpsCommand = new Command(async () => await CheckGps());
            ToggleGpsCommand = new Command(async () => await ToggleGps());
            _alertaGPS = new AlertaGPS { ActivarGPS = false, Mensaje = "Sin información" };
            IsGpsActive = false;
        }

        public AlertaGPS AlertaGPS
        {
            get => _alertaGPS;
            set
            {
                if (_alertaGPS != value)
                {
                    _alertaGPS = value;
                    IsGpsActive = value.ActivarGPS;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsGpsActive
        {
            get => _isGpsActive;
            set
            {
                if (_isGpsActive != value)
                {
                    _isGpsActive = value;
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

        public ICommand CheckGpsCommand { get; }
        public ICommand ToggleGpsCommand { get; }

        private async Task CheckGps()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Consultando estado del GPS...";

                var alerta = await _apiService.GetAlertaGPSAsync();
                if (alerta != null)
                {
                    AlertaGPS = alerta;
                    IsGpsActive = alerta.ActivarGPS;

                    if (alerta.ActivarGPS)
                    {
                        await ActivateGps(alerta.Mensaje);
                    }

                    StatusMessage = alerta.Mensaje;
                }
                else
                {
                    StatusMessage = "No se pudo obtener la alerta GPS";
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

        private async Task ToggleGps()
        {
            try
            {
                IsLoading = true;
                AlertaGPS.ActivarGPS = IsGpsActive;

                if (IsGpsActive)
                {
                    AlertaGPS.Mensaje = "GPS activado manualmente";
                    await ActivateGps("GPS activado manualmente");
                }
                else
                {
                    AlertaGPS.Mensaje = "GPS desactivado manualmente";
                }

                StatusMessage = AlertaGPS.Mensaje;
                OnPropertyChanged(nameof(AlertaGPS));
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al cambiar estado del GPS: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task ActivateGps(string mensaje)
        {
            try
            {
                // Check location permission first
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }

                if (status == PermissionStatus.Granted)
                {
                    // Send notification
                    var notification = new NotificationRequest
                    {
                        NotificationId = 100,
                        Title = "Activación del GPS",
                        Description = mensaje,
                        Schedule = new NotificationRequestSchedule
                        {
                            NotifyTime = DateTime.Now.AddSeconds(1)
                        }
                    };

                    await LocalNotificationCenter.Current.Show(notification);

                    // Try to get current location to activate GPS
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        StatusMessage = $"GPS activado: {location.Latitude}, {location.Longitude}";
                    }
                }
                else
                {
                    StatusMessage = "Se requiere permiso de ubicación";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al activar GPS: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}