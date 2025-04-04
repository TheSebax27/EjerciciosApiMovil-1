using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensoresConsumoMovil.Models
{
    public class AlertaGPS
    {
        public int Id { get; set; }
        public bool ActivarGPS { get; set; }
        public string Mensaje { get; set; }
    }
}
