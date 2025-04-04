using System.Net.Http.Json;
using SensoresConsumoMovil.Models;

namespace SensoresConsumoMovil.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://a1ae-191-95-55-24.ngrok-free.app/api/"; // URL base corregida

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<AlertaNivel> GetAlertaNivelAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<AlertaNivel>($"{_baseUrl}AlertaNivel");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching AlertaNivel: {ex.Message}");
                return new AlertaNivel { Id = 0, NivelAlerta = "alto" }; // Valor por defecto
            }
        }

        public async Task<AlertaGPS> GetAlertaGPSAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<AlertaGPS>($"{_baseUrl}AlertaGPS");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching AlertaGPS: {ex.Message}");
                return new AlertaGPS { Id = 0, ActivarGPS = true, Mensaje = "No disponible" }; // Valor por defecto
            }
        }

        public async Task<AlertaSonido> GetAlertaSonidoAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<AlertaSonido>($"{_baseUrl}AlertaSonido");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching AlertaSonido: {ex.Message}");
                return new AlertaSonido { Id = 0, MensajeEmergencia = "disponible" }; // Valor por defecto
            }
        }
    }
}