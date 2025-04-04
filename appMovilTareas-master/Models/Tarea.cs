using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appMovilTareas.Models
{
    public class Tarea
    {
        public int IdTarea { get; set; }
        public string descripcion { get; set; }
        public DateTime FechaAsignada { get; set; }


        public int IdUsuario { get; set; }
    }
}
