using appMovilTareas.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace appMovilTareas.Service
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://0807-2803-1800-1354-411-9597-f2d9-1f98-8a3.ngrok-free.app/api";
        private const string urlLogin = BaseUrl + "/Usuarios/login";

        public ApiService()
        {
            _httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });
        }

        public async Task<LoginResponse> Login(string documento, string clave)
        {
            var loginModel = new
            {
                Documento = documento,
                Clave = clave
            };

            var json = JsonSerializer.Serialize(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            Console.WriteLine($"🔵 Enviando solicitud de login a: {urlLogin}");
            Console.WriteLine($"🔹 Cuerpo: {json}");

            try
            {
                var response = await _httpClient.PostAsync(urlLogin, content);
                string responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"🟢 Respuesta del servidor: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<LoginResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    Console.WriteLine($"🔴 Error en la solicitud: {response.StatusCode} - {responseContent}");
                    return new LoginResponse { Success = false, Message = "Error en el inicio de sesión", IdCliente = 0 };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔴 Error: {ex.Message}");
                return new LoginResponse { Success = false, Message = "Error en la conexión con el servidor", IdCliente = 0 };
            }
        }

        public async Task<List<Tarea>> ObtenerTareasDelDia(int usuarioId)
        {
            string url = $"{BaseUrl}/Tareas/tareas-hoy/{usuarioId}";
            Console.WriteLine($"🔵 [API] Obteniendo tareas del usuario {usuarioId} desde {url}");

            try
            {
                var response = await _httpClient.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"🟢 Tareas recibidas: {json}");
                    return JsonSerializer.Deserialize<List<Tarea>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    Console.WriteLine($"🔴 Error al obtener tareas: {json}");
                    return new List<Tarea>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔴 Error en la conexión: {ex.Message}");
                return new List<Tarea>();
            }
        }
    }

    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int IdCliente { get; set; }
    }
}
