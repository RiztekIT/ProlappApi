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
    [RoutePrefix("api/OrdenCarga")]
    public class OrdenCargaController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from dbo.OrdenCarga";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("DetalleOrdenCarga/{id}")]
        public HttpResponseMessage GetDetalleOrdenCargaId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleOrdenCarga where IdOrdenCarga  =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("BorrarOrdenCarga/{id}")]
        public string Delete(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" exec dtBorrarOrdenCarga " + id;

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
        public string Post(OrdenCarga ordencarga)
        {
            try
            {

                DataTable table = new DataTable();

                DateTime time = ordencarga.FechaOrden;

                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec itInsertarNuevaOrdenCarga " + ordencarga.NumSalida + " , '" + time.ToString(format) + "' , " +
                                ordencarga.IdCliente + " , '" + ordencarga.Cliente + "', '" + ordencarga.Producto + "' , '" + ordencarga.Fletera + "' , '" +
                                ordencarga.NumCaja + "','" + ordencarga.EnviarA + "' , '" + ordencarga.Sacos + "' , '" + ordencarga.Kilos + "' , '" + ordencarga.Notas +
                                "' , '" + ordencarga.Usuario + @"'";

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


        public string Put(OrdenCarga ordencarga)
        {
            try
            {


                DataTable table = new DataTable();

                DateTime time = ordencarga.FechaOrden;

                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec etEditarOrdenCarga " + ordencarga.IdOrdenCarga + " , " + ordencarga.NumSalida + " , '" + time.ToString(format) + "' , " +
                                ordencarga.IdCliente + " , '" + ordencarga.Cliente + "', '" + ordencarga.Producto + "' , '" + ordencarga.Fletera + "' , '" +
                                ordencarga.NumCaja + "','" +ordencarga.EnviarA +"' , '" + ordencarga.Sacos + "' , '" + ordencarga.Kilos + "' , '" +ordencarga.Notas+
                                "' , '" + ordencarga.Usuario + @"'";

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

        [Route("EstatusDetalle")]
        public string PutEstatusDetalle(OrdenCarga ordencarga)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @" exec etEditarEstatusDetalleCarga" + ordencarga.IdOrdenCarga + " , '" + ordencarga.Estatus  +"'";

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
