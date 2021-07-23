using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IF4101_API_ESPIC_B76711_B72933.Model
{
    public class Vacuna
    {

        public int Id_Vacuna { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public String Fecha_Vacuna { get; set; }
        public String Fecha_Siquiente { get; set; }
        public String Centro_Salud { get; set; }

    }
}
