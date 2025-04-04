using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensoresConsumoMovil.Models
{
    public class AlertaNivel
    {
        public int Id { get; set; }
        public string NivelAlerta { get; set; } // "bajo", "medio", "alto"
    }
}
