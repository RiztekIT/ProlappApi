using ProlappApi.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProlappApi.Controllers
{
    [RoutePrefix("api/Direccion")]
    public class DireccionController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from PrecioLeche order by FechaPrecio DESC";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(HistoricoLeche hl)
        {
            try
            {

                DataTable table = new DataTable();


                DateTime time = hl.FechaPrecio;

                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec itInsertarHistoricoLeche " + hl.IdPrecio + " , " + hl.PrecioLeche + " , " + time.ToString(format) +
                                " , '" + hl.FechaPrecio + @"'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Se Actualizo Correctamente";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;

            }
        }

        //obtener PrecioLeche de dia especifico
        [Route("Direccion/{fecha}")]
        public HttpResponseMessage Get(DateTime Fecha)
        {
            DataTable table = new DataTable();

            string query = @"select * from PrecioLeche where FechaPrecio =" + Fecha + ";";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }




    }
}