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
    [RoutePrefix("api/Calendario")]
    public class CalendarioController : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from Calendar";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("getCalendario/{id}")]
        public HttpResponseMessage GetCalendarioID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Calendar where IdCalendario =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("getCalendarioNombreUsuario/{nombreUsuario}")]
        public HttpResponseMessage GetCalendarioNombreUsuario(string nombreUsuario)
        {
            DataTable table = new DataTable();

            string query = @"select * from Calendar where NombreUsuario = '"+nombreUsuario+"';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("getCalendarioModulo/{modulo}")]
        public HttpResponseMessage GetCalendarioModulo(string modulo)
        {
            DataTable table = new DataTable();

            string query = @"select * from Calendar where Modulo = '" + modulo + "';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("getCalendarioUsuarioModulo/{usuario}/{modulo}")]
        public HttpResponseMessage GetCalendarioUsuarioModulo(string usuario, string modulo)
        {
            DataTable table = new DataTable();

            string query = @"select * from Calendar where NombreUsuario = '" + usuario + "' and Modulo ='"+ modulo + "';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("getDetalleCalendario/{id}")]
        public HttpResponseMessage GetDetalleCalendarioID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleCalendar where IdCalendario = " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("getDetalleCalendarioIdDetalle/{id}")]
        public HttpResponseMessage GetDetalleCalendarioIdDetalle(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleCalendar where IdDetalleCalendario = " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("getCalendarioProceso/{usuario}/{modulo}/{proceso}")]
        public HttpResponseMessage GetCalendarioProceso(string usuario, string modulo, string proceso)
        {
            DataTable table = new DataTable();

            string query = @"select * from Calendar left join Procesos on Calendar.Modulo = Procesos.Area where Calendar.NombreUsuario = '"+usuario+"' and Calendar.Modulo = '"+modulo+"' and Procesos.NombreProceso = '"+proceso+"';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("getCalendarioUsuarioId/{id}")]
        public HttpResponseMessage GetCalendarioUsuarioId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Usuario where IdUsuario = "+ id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Calendario c)
        {
            try
            {

                DataTable table = new DataTable();
             
      

                string query = @"   insert into Calendar ( NombreCalendario, Modulo, NombreUsuario) VALUES ('"+c.NombreCalendario+"', '"+c.Modulo+"', '"+c.NombreUsuario+"')";

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
        public string Put(Calendario c)
        {
            try
            {

                DataTable table = new DataTable();



                string query = @" update Calendar set  NombreCalendario = '" + c.NombreCalendario + "', Modulo = '" + c.Modulo + "', NombreUsuario= '" + c.NombreUsuario + "' where IdCalendario = " + c.IdCalendario;

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
        [Route("AddDetalleCalendario")]
        public string PostDetalleCalendario(DetalleCalendario c)
        {
            try
            {

                DataTable table = new DataTable();
                DateTime time = c.Start;
                DateTime time2 = c.Endd;

                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @" insert into detalleCalendar (IdCalendario, Folio, Documento, Descripcion, Start, Endd, Title, Color, AllDay, ResizableBeforeStart, ResizableBeforeEnd, Draggable)
                                    VALUES ("+c.IdCalendario+","+c.Folio+",'"+c.Documento+"','"+c.Descripcion+"', '"+time.ToString(format)+"','"+time2.ToString(format)+
                                    "','"+c.Title+"','"+c.Color+"',"+c.AllDay+","+c.ResizableBeforeStart+","+c.ResizableBeforeEnd+","+c.Draggable+ @")";

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
        [Route("UpdateDetalleCalendario")]
        public string PutDetalleCalendario(DetalleCalendario dc)
        {
            try
            {

                DataTable table = new DataTable();

                DateTime time = dc.Start;
                DateTime time2 = dc.Endd;
                string format = "yyyy-MM-dd HH:mm:ss";


                string query = @" update DetalleCalendar set IdCalendario =" + dc.IdCalendario +
                   ", Folio =" + dc.Folio + ", Documento = '" + dc.Documento + "', Descripcion = '" + dc.Descripcion +
                   "', Start = '" + time.ToString(format) + "', Endd = '" + time2.ToString(format) + "', Title = '" + dc.Title + "', Color = '" + dc.Color +
                   "', AllDay =" + dc.AllDay + ", ResizableBeforeStart =" + dc.ResizableBeforeStart + ", ResizableBeforeEnd = " + dc.ResizableBeforeEnd + ", Draggable = " + dc.Draggable +
                   " where IdDetalleCalendario = " + dc.IdDetalleCalendario + @"";


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

        [Route("UpdateDetalleOrdenCarga")]
        public string PutDetalleOrdenCarga(DetalleOrdenCarga doc)
        {
            try
            {
                DataTable table = new DataTable();

                DateTime time = doc.FechaMFG;
                DateTime time2 = doc.FechaCaducidad;
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec etEditarDetalleOrdenCarga " + doc.IdDetalleOrdenCarga + " , " + doc.IdOrdenCarga + " , " + doc.IdOrdenCarga + " , '" + doc.ClaveProducto + "' , '" + doc.Producto + "' , '" + doc.Sacos +
                                "' , '" + doc.PesoxSaco + "' , '" + doc.Lote + "' , " + doc.IdProveedor + " , '" + doc.Proveedor + "' , '" + doc.PO
                                + "' , '" + time.ToString(format) + "' , '" + time2.ToString(format) + "' , '" + doc.Shipper + "' , '" + doc.USDA + "' , '" + doc.Pedimento + "' , '" +
                                doc.Saldo + @"'";

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



        [Route("DeleteCalendario/{id}")]
        public string DeleteCalendario(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @"delete Calendar where IdCalendario =" + id;

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



        [Route("DeleteDetalleCalendario/{id}")]
        public string DeleteDetalleCalendario(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" delete DetalleCalendar where IdDetalleCalendario =" + id;

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

    }
}
