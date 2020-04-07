using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ProlappApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ProlappApi.Controllers
{
    [RoutePrefix("api/TraspasoTarima")]
    public class TraspasoTarimaController : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from dbo.TraspasoTarima";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("BorrarTraspasoTarima/{id}")]
        public string Delete(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" exec dtBorrarTraspasoTarima " + id;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Se elimino Correctamente";
            }
            catch (Exception)
            {
                return "Error al Eliminar";
            }
        }

        public string Post(TraspasoTarima Tt)
        {
            try
            {

                DataTable table = new DataTable();
                DateTime time = Tt.FechaTraspaso;
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec itInsertNuevoTraspasoTarima " + Tt.IdOrigenTarima + " , " + Tt.IdDestinoTarima + " , '" +
                                Tt.ClaveProducto + "' , '" + Tt.Producto + "' , '" + Tt.Lote + "' , '" +
                                Tt.Sacos + "' , '" + time.ToString(format) + "' , " + Tt.IdUsuario + " , '" + Tt.Usuario + @"'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Se Agrego Correctamente";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;

            }
        }

        public string Put(TraspasoTarima Tt)
        {
            try
            {

                DataTable table = new DataTable();
                DateTime time = Tt.FechaTraspaso;
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec etEditarTraspasoTarima " + Tt.IdTraspasoTarima + " , " + Tt.IdOrigenTarima + " , " + Tt.IdDestinoTarima + " , '" +
                                Tt.ClaveProducto + "' , '" + Tt.Producto + "' , '" + Tt.Lote + "' , '" +
                                Tt.Sacos + "' , '" + time.ToString(format) + "' , " + Tt.IdUsuario + " , '" + Tt.Usuario + @"'";

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

    }
}
