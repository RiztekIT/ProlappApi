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
    [RoutePrefix("api/Forwards")]
    public class ForwardsController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from Forwards";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
    

        [Route("GetForwardID/{id}")]
        public HttpResponseMessage getfwd(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Forwards where IdForward = " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("BorrarForward/{id}")]
        public string Delete(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" delete from OrdenCarga where IdOrdenCarga = " + id;

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

        public string Post(Forwards forward)
        {
            try
            {

                DataTable table = new DataTable();


                DateTime time = forward.FechaCierre;
                DateTime time2 = forward.FechaCierre;

                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec itInsertNuevaForward '" + time.ToString(format) + "' , '" + time2.ToString(format) + "' , '" +
                                forward.CantidadDlls + "' , '" + forward.TipoCambio + "', '" + forward.CantidadMXN + "' , '" + forward.Garantia + "' , '" +
                                forward.GarantiaPagada + "' , '" + forward.CantidadPendiente + "' , '" + forward.Destino + "' , '" + forward.Promedio + "' , '" + forward.Estatus + @"';";

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

        public string Put(Forwards forward)
        {
            try
            {


                DataTable table = new DataTable();

                DateTime time = forward.FechaCierre;
                DateTime time2 = forward.FechaCierre;

                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                               exec etEditarOrdenDescarga " + forward.IdForward + " , '"+ time.ToString(format) + "' , '" + time2.ToString(format) + "' , '" +
                                forward.CantidadDlls + "' , '" + forward.TipoCambio + "', '" + forward.CantidadMXN + "' , '" + forward.Garantia + "' , '" +
                                forward.GarantiaPagada + "' , '" + forward.CantidadPendiente + "' , '" + forward.Destino + "' , '" + forward.Promedio + "' , '" + forward.Estatus + @"'";

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

        [Route("GetUltimoForward")]
        public HttpResponseMessage getUltimoForward()
        {
            DataTable table = new DataTable();

            string query = @"select MAX(IdForward) as IdForward from Forwards";

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
