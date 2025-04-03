using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiProductos.Models
{
    public class Producto
    {
        [Key]
        public int idProducto { get; set; }
        public string? codigoProducto { get; set; }
        public string? nombreProducto { get; set; }
        public string? descripcionProducto { get; set; }
        public double precioUnitario { get; set; }
        public int cantidad { get; set; }

        // Llave Foranea

        public int idCategoria { get; set; }
        [ForeignKey("idCategoria")]

        [JsonIgnore]
        public Categoria? categoria { get; set; }
    }
}
