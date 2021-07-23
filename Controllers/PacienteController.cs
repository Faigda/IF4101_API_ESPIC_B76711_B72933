using IF4101_API_ESPIC_B76711_B72933.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IF4101_API_ESPIC_B76711_B72933.Controllers
{
    [ApiController]
    [Route("api/Pacientes")]
    public class PacienteController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public PacienteController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [Route("Lista")]
        [HttpGet]
        public async Task<ActionResult<List<Paciente>>> Lista()
        {

            List<Paciente> listapaciente = new List<Paciente>();

            SqlConnection cn = new SqlConnection(Configuration.GetConnectionString("Default"));



            var cmd = new SqlCommand("sp_OBTENER_LISTA_PACIENTES", cn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cn.Open();


            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Paciente p = new Paciente();

                    DataRow dr = dt.Rows[i];
                    string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                    ArrayList itm = new ArrayList(allColumns);

                    p.Cedula = Int32.Parse(itm[0].ToString());
                    p.Nombre = itm[1].ToString();
                    p.Apellido = itm[2].ToString();
                    p.Tipo_Sangre = itm[3].ToString();
                    p.Estado_Civil = itm[4].ToString();
                    p.Telefono = itm[5].ToString();
                    p.Domicilio = itm[6].ToString();
                    p.Contrasenna= itm[7].ToString();

                    listapaciente.Add(p);

                }

            };

            cn.Close();

            return listapaciente;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<LoginRespuesta>> Login(Login login)
        {
            SqlConnection cn = new SqlConnection(Configuration.GetConnectionString("Default"));

            var cmd = new SqlCommand("sp_LOGIN", cn);

            cn.Open();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@param_CEDULA", login.Cedula);
            cmd.Parameters.AddWithValue("@param_CONTRASENNA", login.Contrasenna);

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                DataRow dr = dt.Rows[0];
                string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                ArrayList itm = new ArrayList(allColumns);

                cn.Close();

                LoginRespuesta loginRespuesta = new LoginRespuesta();
                loginRespuesta.Cedula = login.Cedula;
                loginRespuesta.Estado = itm[0].ToString();

                return loginRespuesta;

            };

        }

        [Route("Registro")]
        [HttpPost]
        public async Task<ActionResult<RegistroRespuesta>> Registro(Paciente paciente)
        {

            SqlConnection cn = new SqlConnection(Configuration.GetConnectionString("Default"));

            cn.Open();
            var cmd = new SqlCommand("sp_REGISTRO", cn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@param_CEDULA", (int)paciente.Cedula);
            cmd.Parameters.AddWithValue("@param_NOMBRE", paciente.Nombre);
            cmd.Parameters.AddWithValue("@param_APELLIDO", paciente.Apellido);
            cmd.Parameters.AddWithValue("@param_TIPO_SANGRE", paciente.Tipo_Sangre);
            cmd.Parameters.AddWithValue("@param_ESTADO_CIVIL", paciente.Estado_Civil);
            cmd.Parameters.AddWithValue("@param_TELEFONO", paciente.Telefono);
            cmd.Parameters.AddWithValue("@param_DOMICILIO", paciente.Domicilio);
            cmd.Parameters.AddWithValue("@param_CONTRASENNA", paciente.Contrasenna);

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                DataRow dr = dt.Rows[0];
                string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                ArrayList itm = new ArrayList(allColumns);

                cn.Close();

                RegistroRespuesta registroRespuesta = new RegistroRespuesta();
                registroRespuesta.Respuesta = itm[0].ToString();

                return registroRespuesta;

            }
        }

        [Route("Datos/{cedula}")]
        [HttpGet]
        public async Task<ActionResult<Paciente>> Datos(int cedula)
        {

            SqlConnection cn = new SqlConnection(Configuration.GetConnectionString("Default"));

            var cmd = new SqlCommand("sp_OBTENER_PACIENTE", cn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@param_CEDULA", (int)cedula);

            cn.Open();


            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                Paciente paciente = new Paciente();

                DataRow dr = dt.Rows[0];
                string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                ArrayList itm = new ArrayList(allColumns);

                paciente.Cedula = Int32.Parse(itm[0].ToString());
                paciente.Nombre = itm[1].ToString();
                paciente.Apellido = itm[2].ToString();
                paciente.Tipo_Sangre = itm[3].ToString();
                paciente.Estado_Civil = itm[4].ToString();
                paciente.Telefono = itm[5].ToString();
                paciente.Domicilio = itm[6].ToString();
                paciente.Contrasenna = itm[7].ToString();

                cn.Close();

                return paciente;

            }
             
        }

        [Route("Actualizar")]
        [HttpPut]
        public async Task<ActionResult<ActualizarRespuesta>> Actualizar(Paciente paciente)
        {

            SqlConnection cn = new SqlConnection(Configuration.GetConnectionString("Default"));

            cn.Open();
            var cmd = new SqlCommand("sp_ACTUALIZAR_PACIENTE", cn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@param_CEDULA", (int)paciente.Cedula);
            cmd.Parameters.AddWithValue("@param_NOMBRE", paciente.Nombre);
            cmd.Parameters.AddWithValue("@param_APELLIDO", paciente.Apellido);
            cmd.Parameters.AddWithValue("@param_TIPO_SANGRE", paciente.Tipo_Sangre);
            cmd.Parameters.AddWithValue("@param_ESTADO_CIVIL", paciente.Estado_Civil);
            cmd.Parameters.AddWithValue("@param_TELEFONO", paciente.Telefono);
            cmd.Parameters.AddWithValue("@param_DOMICILIO", paciente.Domicilio);
            cmd.Parameters.AddWithValue("@param_CONTRASENNA", paciente.Contrasenna);

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                DataRow dr = dt.Rows[0];
                string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                ArrayList itm = new ArrayList(allColumns);

                cn.Close();

                ActualizarRespuesta actualizarRespuesta = new ActualizarRespuesta();
                actualizarRespuesta.Respuesta = itm[0].ToString();

                return actualizarRespuesta;

            }
        }

        [Route("Vacunas/{cedula}")]
        [HttpGet]
        public async Task<ActionResult<List<Vacuna>>> Vacunas(int cedula)
        {

            SqlConnection cn = new SqlConnection(Configuration.GetConnectionString("Default"));

            var cmd = new SqlCommand("sp_lista_vacuna_api", cn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@param_CEDULA", (int)cedula);

            cn.Open();


            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {

                List<Vacuna> listaVacunas = new List<Vacuna>();

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Vacuna vacuna = new Vacuna();

                    DataRow dr = dt.Rows[i];
                    string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                    ArrayList itm = new ArrayList(allColumns);

                    vacuna.Id_Vacuna = Int32.Parse(itm[0].ToString());
                    vacuna.Nombre = itm[1].ToString();
                    vacuna.Descripcion = itm[2].ToString();
                    vacuna.Fecha_Vacuna = itm[3].ToString();
                    vacuna.Fecha_Siquiente = itm[4].ToString();
                    vacuna.Centro_Salud= itm[5].ToString();

                    listaVacunas.Add(vacuna);

                }

                cn.Close();

                return listaVacunas;

            }

        }

        [Route("Vacunas/Detalle")]
        [HttpPost]
        public async Task<ActionResult<Vacuna>> VacunasDetalle(BuscarVacuna buscarVacuna)
        {

            SqlConnection cn = new SqlConnection(Configuration.GetConnectionString("Default"));

            var cmd = new SqlCommand("sp_vacunas_api_id", cn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@param_ID", buscarVacuna.Id_Vacuna);
            cmd.Parameters.AddWithValue("@param_CEDULA", buscarVacuna.Cedula);

            cn.Open();


            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                Vacuna vacuna = new Vacuna();

                DataRow dr = dt.Rows[0];
                string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                ArrayList itm = new ArrayList(allColumns);

                vacuna.Id_Vacuna = Int32.Parse(itm[0].ToString());
                vacuna.Nombre = itm[1].ToString();
                vacuna.Descripcion = itm[2].ToString();
                vacuna.Fecha_Vacuna = itm[3].ToString();
                vacuna.Fecha_Siquiente = itm[4].ToString();
                vacuna.Centro_Salud = itm[5].ToString();

                cn.Close();

                return vacuna;

            }

        }

        [Route("Citas/{cedula}")]
        [HttpGet]
        public async Task<ActionResult<List<Cita>>> Citas(int cedula)
        {

            SqlConnection cn = new SqlConnection(Configuration.GetConnectionString("Default"));

            var cmd = new SqlCommand("sp_OBTENER_LISTA_CITAS", cn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@param_CEDULA", (int)cedula);

            cn.Open();


            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {

                List<Cita> listaCitas = new List<Cita>();

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Cita cita = new Cita();

                    DataRow dr = dt.Rows[i];
                    string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                    ArrayList itm = new ArrayList(allColumns);

                    cita.Id_Cita = Int32.Parse(itm[0].ToString());
                    cita.Centro_Salud = itm[1].ToString();
                    cita.Fecha = itm[2].ToString();
                    cita.Especialidad = itm[3].ToString();
                    cita.Detalle = itm[4].ToString();
                    cita.Nombre = itm[5].ToString();
                    cita.Codigoc_Medico = Int32.Parse(itm[6].ToString());

                    listaCitas.Add(cita);

                }

                cn.Close();

                return listaCitas;

            }

        }

        [Route("Citas/Detalle")]
        [HttpPost]
        public async Task<ActionResult<Cita>> CitasDetalle(BuscarCita buscarCita)
        {

            SqlConnection cn = new SqlConnection(Configuration.GetConnectionString("Default"));

            var cmd = new SqlCommand("sp_OBTENER_CITA", cn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@param_CEDULA", buscarCita.Cedula);
            cmd.Parameters.AddWithValue("@param_ID", buscarCita.Id_Cita);

            cn.Open();


            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                Cita cita = new Cita();

                DataRow dr = dt.Rows[0];
                string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                ArrayList itm = new ArrayList(allColumns);

                cita.Id_Cita = Int32.Parse(itm[0].ToString());
                cita.Centro_Salud = itm[1].ToString();
                cita.Fecha = itm[2].ToString();
                cita.Especialidad = itm[3].ToString();
                cita.Detalle = itm[4].ToString();
                cita.Nombre = itm[5].ToString();
                cita.Codigoc_Medico = Int32.Parse(itm[6].ToString());

                cn.Close();

                return cita;

            }

        }

        [Route("Alergias/{cedula}")]
        [HttpGet]
        public async Task<ActionResult<List<Alergia>>> Alergias(int cedula)
        {

            SqlConnection cn = new SqlConnection(Configuration.GetConnectionString("Default"));

            var cmd = new SqlCommand("sp_OBTENER_LISTA_ALERGIAS", cn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@param_CEDULA", (int)cedula);

            cn.Open();


            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {

                List<Alergia> listaAlergias = new List<Alergia>();

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Alergia alergia = new Alergia();

                    DataRow dr = dt.Rows[i];
                    string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                    ArrayList itm = new ArrayList(allColumns);

                    alergia.Id_Alergia = Int32.Parse(itm[0].ToString());
                    alergia.Nombre= itm[1].ToString();
                    alergia.Descripcion = itm[2].ToString();
                    alergia.Fecha = itm[3].ToString();
                    alergia.Centro_Salud = itm[4].ToString();
                    alergia.Medicamento = itm[5].ToString();
                    alergia.Nombre_Medico = itm[6].ToString();
                    alergia.Codigoc_Medico = Int32.Parse(itm[7].ToString());

                    listaAlergias.Add(alergia);

                }

                cn.Close();

                return listaAlergias;

            }

        }

        [Route("Alergias/Detalle")]
        [HttpPost]
        public async Task<ActionResult<Alergia>> AlergiasDetalle(BuscarAlergia buscarAlergia)
        {

            SqlConnection cn = new SqlConnection(Configuration.GetConnectionString("Default"));

            var cmd = new SqlCommand("sp_OBTENER_ALERGIA", cn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@param_CEDULA", buscarAlergia.Cedula);
            cmd.Parameters.AddWithValue("@param_ID", buscarAlergia.Id_Alergia);

            cn.Open();

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                Alergia alergia = new Alergia();

                DataRow dr = dt.Rows[0];
                string[] allColumns = dr.ItemArray.Select(obj => obj.ToString()).ToArray();
                ArrayList itm = new ArrayList(allColumns);

                alergia.Id_Alergia = Int32.Parse(itm[0].ToString());
                alergia.Nombre = itm[1].ToString();
                alergia.Descripcion = itm[2].ToString();
                alergia.Fecha = itm[3].ToString();
                alergia.Centro_Salud = itm[4].ToString();
                alergia.Medicamento = itm[5].ToString();
                alergia.Nombre_Medico = itm[6].ToString();
                alergia.Codigoc_Medico = Int32.Parse(itm[7].ToString());

                cn.Close();

                return alergia;

            }

        }

    }
}
