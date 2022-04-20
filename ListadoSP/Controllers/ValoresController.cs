using ListadoSP.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ListadoSP.Controllers
{
    [ApiController]
    [Route("api/valores")]
    public class ValoresController : ControllerBase
    {
        private string connectionString;

        public ValoresController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public ActionResult<IEnumerable<Valor>> Get([FromQuery] int[] ids)
        {
            var valores = new List<Valor>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using(SqlCommand comando = new SqlCommand("Valores_ObtenerListado", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    var dt = new DataTable();
                    dt.Columns.Add("Id", typeof(int));

                    foreach (var id in ids)
                    {
                        dt.Rows.Add(id);
                    }

                    var parametro = comando.Parameters.AddWithValue("ListadoIds", dt);
                    parametro.SqlDbType = SqlDbType.Structured;

                    var reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        valores.Add(new Valor()
                        {
                            Id = int.Parse(reader["Id"].ToString()!),
                            Texto = reader["Valor"].ToString()!
                        });
                    }

                }
            }

            return valores;
        }
    }
}
