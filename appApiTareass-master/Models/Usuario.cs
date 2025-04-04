using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace appApiTareass.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public string clave { get; set; }
        [JsonIgnore]
        public ICollection<Tarea> TareasU { get; set; } = new List<Tarea>(); 


    }
}
