using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductoConsumoMovil.Models
{
    class Producto
    {
        public int idProducto { get; set; }
        public string? codigoProducto { get; set; }
        public string? nombreProducto { get; set; }
        public string? descripcionProducto { get; set; }
        public double precioUnitario { get; set; }
        public int cantidad { get; set; }
        public int idCategoria { get; set; }
    }
}
