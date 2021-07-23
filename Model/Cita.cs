using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IF4101_API_ESPIC_B76711_B72933.Model
{
    public class Cita
    {
        public int Id_Cita { get; set; }
        public String Centro_Salud { get; set; }
        public String Fecha { get; set; }
        public String Especialidad { get; set; }
        public String Detalle { get; set; }
        public String Nombre { get; set; }
        public int Codigoc_Medico { get; set; }
    }
}
