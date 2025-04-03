using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiProductos.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string? nombreCategoria { get; set; }
        public string? descripcion { get; set; }
        [JsonIgnore]
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
