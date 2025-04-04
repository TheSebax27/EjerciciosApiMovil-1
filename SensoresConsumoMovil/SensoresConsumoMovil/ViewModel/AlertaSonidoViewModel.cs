using SensoresConsumoMovil.Models;
using SensoresConsumoMovil.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

namespace SensoresConsumoMovil.ViewModel
{
    public class AlertaSonidoViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private AlertaSonido _alertaSonido;
        private string _statusMessage;
        private bool _isLoading;
        private string _selectedGravedad;
        private List<string> _nivelesGravedad;
        private string _mensajeEmergencia;
        private MediaElement _mediaElement;

        public AlertaSonidoViewModel()
        {
            _apiService = new ApiService();
            CheckSonidoCommand = new Command(async () => await CheckSonido());
            EnviarMensajeEmergenciaCommand = new Command(async () => await EnviarMensajeEmergencia());
            _alertaSonido = new AlertaSonido { MensajeEmergencia = "Sin información" };
            NivelesGravedad = new List<string> { "bajo", "medio", "alarma", "crítico" };
            SelectedGravedad = "bajo";
            MensajeEmergencia = "Mensaje de emergencia";
        }

        // Método para establecer el MediaElement desde la vista
        public void SetMediaElement(MediaElement mediaElement)
        {
            _mediaElement = mediaElement;
        }

        public List<string> NivelesGravedad
        {
            get => _nivelesGravedad;
            set
            {
                if (_nivelesGravedad != value)
                {
                    _nivelesGravedad = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedGravedad
        {
            get => _selectedGravedad;
            set
            {
                if (_selectedGravedad != value)
                {
                    _selectedGravedad = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MensajeEmergencia
        {
            get => _mensajeEmergencia;
            set
            {
                if (_mensajeEmergencia != value)
                {
                    _mensajeEmergencia = value;
                    OnPropertyChanged();
                }
            }
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
        public ICommand EnviarMensajeEmergenciaCommand { get; }

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
                    MensajeEmergencia = alerta.MensajeEmergencia;

                    if (alerta.MensajeEmergencia.ToLower().Contains("alarma"))
                    {
                        SelectedGravedad = "alarma";
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

        private async Task EnviarMensajeEmergencia()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Enviando mensaje de emergencia...";

                string mensajeCompleto = $"{SelectedGravedad}: {MensajeEmergencia}";
                AlertaSonido.MensajeEmergencia = mensajeCompleto;

                if (SelectedGravedad.ToLower().Contains("alarma") ||
                    SelectedGravedad.ToLower().Contains("crítico"))
                {
                    await PlayAlarmSound();
                }

                StatusMessage = $"Mensaje enviado: {mensajeCompleto}";
                OnPropertyChanged(nameof(AlertaSonido));
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al enviar mensaje: {ex.Message}";
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
                // Verificar que el MediaElement esté disponible
                if (_mediaElement == null)
                {
                    StatusMessage = "Error: MediaElement no inicializado";
                    return;
                }

                StatusMessage = "Reproduciendo sonido de alarma...";

                // Determinar el archivo de audio según la gravedad
                string audioFile;

                switch (SelectedGravedad.ToLower())
                {
                    case "alarma":
                        audioFile = "alarma_alta.mp3";
                        break;
                    case "crítico":
                        audioFile = "alarma_alta.mp3";
                        break;
                    case "medio":
                        audioFile = "alarma_media.mp3";
                        break;
                    default:
                        audioFile = "alarma_baja.mp3";
                        break;
                }

                // Intentar diferentes formas de cargar el audio
                try
                {
                    // Intento 1: Como recurso incrustado
                    _mediaElement.Source = MediaSource.FromResource(audioFile);
                    _mediaElement.Volume = 1.0;
                    _mediaElement.ShouldAutoPlay = false; // Establecer en false y luego reproducir manualmente

                    await MainThread.InvokeOnMainThreadAsync(() => {
                        _mediaElement.Play();
                    });

                    StatusMessage = $"Reproduciendo audio: {audioFile}";
                }
                catch (Exception ex1)
                {
                    try
                    {
                        // Intento 2: Con ruta completa
                        _mediaElement.Source = MediaSource.FromFile($"Resources/Raw/{audioFile}");

                        await MainThread.InvokeOnMainThreadAsync(() => {
                            _mediaElement.Play();
                        });

                        StatusMessage = $"Reproduciendo audio (alt): {audioFile}";
                    }
                    catch (Exception ex2)
                    {
                        StatusMessage = $"Error primario: {ex1.Message}, Error secundario: {ex2.Message}";
                    }
                }

                // Esperar un tiempo para que se reproduzca el audio
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