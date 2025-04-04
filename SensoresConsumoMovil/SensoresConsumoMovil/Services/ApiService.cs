using System.Net.Http.Json;
using SensoresConsumoMovil.Models;
using System.Threading.Tasks;

namespace SensoresConsumoMovil.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7123/api/";
        private readonly IPreferencesService _preferencesService;

        public ApiService(IPreferencesService preferencesService = null)
        {
            _httpClient = new HttpClient();
            _preferencesService = preferencesService ?? new PreferencesService();
        }

        public async Task<AlertaNivel> GetAlertaNivelAsync()
        {
            try
            {
                // Intentar obtener del servidor
                var response = await _httpClient.GetFromJsonAsync<AlertaNivel>($"{_baseUrl}AlertaNivel");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching AlertaNivel: {ex.Message}");

                // Obtener del almacenamiento local si está disponible
                var nivelGuardado = _preferencesService.GetNivelAlerta();
                return new AlertaNivel
                {
                    Id = 0,
                    NivelAlerta = string.IsNullOrEmpty(nivelGuardado) ? "bajo" : nivelGuardado
                };
            }
        }

        public async Task<AlertaGPS> GetAlertaGPSAsync()
        {
            try
            {
                // Intentar obtener del servidor
                var response = await _httpClient.GetFromJsonAsync<AlertaGPS>($"{_baseUrl}AlertaGPS");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching AlertaGPS: {ex.Message}");

                // Obtener configuración local
                var activarGPS = _preferencesService.GetActivarGPS();
                var mensajeGPS = _preferencesService.GetMensajeGPS();

                return new AlertaGPS
                {
                    Id = 0,
                    ActivarGPS = activarGPS,
                    Mensaje = string.IsNullOrEmpty(mensajeGPS) ? "No disponible" : mensajeGPS
                };
            }
        }

        public async Task<AlertaSonido> GetAlertaSonidoAsync()
        {
            try
            {
                // Intentar obtener del servidor
                var response = await _httpClient.GetFromJsonAsync<AlertaSonido>($"{_baseUrl}AlertaSonido");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching AlertaSonido: {ex.Message}");

                // Obtener configuración local
                var mensajeEmergencia = _preferencesService.GetMensajeEmergencia();

                return new AlertaSonido
                {
                    Id = 0,
                    MensajeEmergencia = string.IsNullOrEmpty(mensajeEmergencia) ? "No disponible" : mensajeEmergencia
                };
            }
        }

        // Métodos para guardar la configuración del usuario
        public Task SaveAlertaNivelAsync(string nivel)
        {
            _preferencesService.SaveNivelAlerta(nivel);
            return Task.CompletedTask;
        }

        public Task SaveAlertaGPSAsync(bool activar, string mensaje)
        {
            _preferencesService.SaveActivarGPS(activar);
            _preferencesService.SaveMensajeGPS(mensaje);
            return Task.CompletedTask;
        }

        public Task SaveAlertaSonidoAsync(string mensajeEmergencia)
        {
            _preferencesService.SaveMensajeEmergencia(mensajeEmergencia);
            return Task.CompletedTask;
        }
    }
}