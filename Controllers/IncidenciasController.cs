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

        public string Post(Incidencias i)
        {
            try
            {

                DateTime time = i.FechaElaboracion;
                DateTime time2 = i.FechaFinalizacion;

                string format = "yyyy-MM-dd HH:mm:ss";

                DataTable table = new DataTable();

                string query = @"
                                Insert into Incidencias (Folio, TipoIncidencia, Procedencia, IdDetalle, Cantidad, Estatus, FechaElaboracion, FechaFinalizacion, Observaciones) 
                                        values ("+i.Folio+",'"+i.TipoIncidencia+"', '"+i.Procedencia+"', "+i.IdDetalle+", '"+i.Cantidad+"', '"+i.Estatus+ "', '"+time.ToString(format)+"','"+ time2.ToString(format) + "','"+i.Observaciones+@"');";

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
                               update Incidencias set Folio = " + i.Folio + ", TipoIncidencia = '" + i.TipoIncidencia + "', Procedencia= '" + i.Procedencia + "', IdDetalle =  " + i.IdDetalle +
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


    }
}
