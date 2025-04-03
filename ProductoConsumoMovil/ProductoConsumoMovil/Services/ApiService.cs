using ProductoConsumoMovil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductoConsumoMovil.Services
{
    class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string urlProducto = "https://localhost:7225/api/Producto";
        private const string urlCategoria = "https://localhost:7225/api/Categoria";

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            var response = await _httpClient.GetStringAsync(urlProducto);
            var lista = JsonSerializer.Deserialize<List<Producto>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return lista;
        }

        public async Task<List<Categoria>> GetCategoriaAsync()
        {
            var listaCategorias = await _httpClient.GetFromJsonAsync<List<Categoria>>("https://localhost:7225/api/Categoria");
            return listaCategorias;
        }

        public async Task<bool> PostProductoAsync(Producto producto)
        {
            var json = JsonSerializer.Serialize(producto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(urlProducto, content);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> PostCategoriaAsync(Categoria categoria)
        {
            var json = JsonSerializer.Serialize(categoria);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(urlCategoria, content);
            return response.IsSuccessStatusCode;
        }
    }
}
