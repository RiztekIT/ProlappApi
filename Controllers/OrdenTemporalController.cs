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
    [RoutePrefix("api/OrdenTemporal")]
    public class OrdenTemporalController : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from dbo.OrdenTemporal";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener orden Temporal por OrdenCargaID, LOTE Y CLAVE PRODUCTO
        [Route("OrdenTemporal/{id}/{lote}/{clave}")]
        public HttpResponseMessage GetOrdenTemporal(int id, string lote, string clave)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenTemporal where IdOrdenCarga  =" + id + " and Lote = '" + lote + "' and ClaveProducto = '" + clave + "';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("BorrarOrdenTemporal/{id}")]
        public string Delete(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" exec dtBorrarOrdenTemporal " + id;

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


        public string Post(OrdenTemporal ot)
        {
            try
            {

                DataTable table = new DataTable();

                DateTime time = ot.FechaCaducidad;
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec itInsertNuevaOrdenTemporal " + ot.IdTarima + " , " + ot.IdOrdenCarga + " , " + ot.IdOrdenDescarga +
                                " , '" + ot.QR + "' , '" + ot.ClaveProducto + "' , '" + ot.Lote + "' , '" + ot.Sacos +
                                "' , '" + ot.Producto + "' , '" + ot.PesoTotal + "' , '" + time.ToString(format) + "' , '" + ot.Comentarios + @"'";

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

        public string Put(OrdenTemporal ot)
        {
            try
            {

                DataTable table = new DataTable();
                DateTime time = ot.FechaCaducidad;
                string format = "yyyy-MM-dd HH:mm:ss";
                string query = @"
                                exec itInsertNuevaOrdenTemporal " + ot.IdOrdenTemporal + " , " + ot.IdTarima + " , " + ot.IdOrdenCarga + " , " + ot.IdOrdenDescarga +
                                " , '" + ot.QR + "' , '" + ot.ClaveProducto + "' , '" + ot.Lote + "' , '" + ot.Sacos +
                                "' , '" + ot.Producto + "' , '" + ot.PesoTotal + "' , '" + time.ToString(format) + "' , '" + ot.Comentarios +@"'";

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

        //Obtener Orden Temporal por OrdenCargaID
        [Route("OrdenTemporalID/{id}")]
        public HttpResponseMessage GetOrdenTemporalID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenTemporal where IdOrdenCarga  =" + id + ";";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Orden Temporal por OrdenDescargaID
        [Route("OrdenTemporalIDOD/{id}")]
        public HttpResponseMessage GetOrdenTemporalIDOD(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenTemporal where IdOrdenDescarga  =" + id + ";";

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
