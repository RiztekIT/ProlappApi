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
    [RoutePrefix("api/Incidencias")]
    public class IncidenciasController : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from Incidencias";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetIncidenciaId/{id}")]
        public HttpResponseMessage GetIncidenciaId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Incidencias where IdIncidencia = " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("GetIncidenciaFolio/{folio}")]
        public HttpResponseMessage GetIncidenciaFolio(int folio)
        {
            DataTable table = new DataTable();

            string query = @"select * from Incidencias where Folio = " + folio;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("GetIncidenciaProcedencia/{procedencia}")]
        public HttpResponseMessage GetIncidenciaProcedencia(string procedencia)
        {
            DataTable table = new DataTable();

            string query = @"select * from Incidencias where Procedencia = '"+procedencia+"'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("GetIncidenciaEstatus/{estatus}")]
        public HttpResponseMessage GetIncidenciaEstatus(string estatus)
        {
            DataTable table = new DataTable();

            string query = @"select * from Incidencias where Estatus = '" + estatus + "'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetOrdenCargaFolio/{folio}")]
        public HttpResponseMessage GetOrdenCargaFolio(int folio)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenCarga where Folio ="+folio;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("GetOrdenDescargaFolio/{folio}")]
        public HttpResponseMessage GetOrdenDescargaFolio(int folio)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenDescarga where Folio =" + folio;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("GetListOrdenesCargaId/{id}")]
        public HttpResponseMessage GetListOrdenesCargaId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleOrdenCarga where IdOrdenCarga ="+id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("GetListOrdenesDescargaId/{id}")]
        public HttpResponseMessage GetListOrdenesDescargaId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleOrdenDescarga where IdOrdenDescarga =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Folio y sumarle 1
        [Route("GetNewFolio")]
        public string GetNewFolio()
        {
            string folio;
            DataRow row;
            DataTable table = new DataTable();

            string query = @"select MAX ( Incidencias.Folio) + 1 as Folio from Incidencias";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
                row = table.Rows[0];
                folio = row["folio"].ToString();
            }

            return folio;
        }

        public string Post(Incidencias i)
        {
            try
            {

                DateTime time = i.FechaElaboracion;
                DateTime time2 = i.FechaFinalizacion;

                string format = "yyyy-MM-dd HH:mm:ss";

                DataTable table = new DataTable();

                string query = @"
                                Insert into Incidencias (Folio, FolioProcedencia, TipoIncidencia, Procedencia, IdDetalle, Cantidad, Estatus, FechaElaboracion, FechaFinalizacion, Observaciones) 
                                        values ("+i.Folio+", "+i.FolioProcedencia+", '"+i.TipoIncidencia+"', '"+i.Procedencia+"', "+i.IdDetalle+", '"+i.Cantidad+"', '"+i.Estatus+ "', '"+time.ToString(format)+"','"+ time2.ToString(format) + "','"+i.Observaciones+@"');";

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

        public string Put(Incidencias i)
        {
            try
            {

                DateTime time = i.FechaElaboracion;
                DateTime time2 = i.FechaFinalizacion;

                string format = "yyyy-MM-dd HH:mm:ss";

                DataTable table = new DataTable();

                string query = @"
                               update Incidencias set Folio = " + i.Folio + ", FolioProcedencia =" + i.FolioProcedencia+ ", TipoIncidencia = '" + i.TipoIncidencia + "', Procedencia= '" + i.Procedencia + "', IdDetalle =  " + i.IdDetalle +
                               ", Cantidad =  '" + i.Cantidad + "', Estatus = '" + i.Estatus + "', FechaElaboracion = '" + time.ToString(format) + "', FechaFinalizacion = '" + time2.ToString(format) + "', Observaciones = '" + i.Observaciones + @"'";

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

        //Borrar Incidencia por ID
        [Route("BorrarIncidenciaId/{id}")]
        public string DeleteIndicenciaId(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" delete from Incidencias where IdIncidencia ="+id;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Se elimino Correctamente";
            }
            catch (Exception exe)
            {
                return "Error al Eliminar " + exe;
            }
        }


    }
}
