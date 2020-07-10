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

                string format = "yyyy-MM-dd";

                string query = @"
                                exec itInsertarHistoricoLeche " + hl.PrecioLeche + " , " + hl.VarianteDiaAnterior + 
                                " , '" + time.ToString(format) + @"'";

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
        [Route("{fecha}")]
        public HttpResponseMessage Get(string fecha)
        {
            DataTable table = new DataTable();



            string query = @"select * from PrecioLeche where FechaPrecio ="+ "'" + fecha + "'"+ "; ";

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