using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace appApiTareass.Models
{
    public class Tarea
    {
        [Key]
        public int IdTarea { get; set; }
        public string descripcion { get; set; }
        public DateTime FechaAsignada { get; set; }


        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        [JsonIgnore]
        public Usuario? UsuarioT { get; set; }
    }
}
