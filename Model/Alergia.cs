using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IF4101_API_ESPIC_B76711_B72933.Model
{
    public class Alergia
    {
        public int Id_Alergia { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public String Fecha { get; set; }
        public String Centro_Salud { get; set; }
        public String Medicamento { get; set; }
        public String Nombre_Medico { get; set; }
        public int Codigoc_Medico { get; set; }
    }
}
