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
        public string PutDetalleCalendario(DetalleCalendario c)
        {
            try
            {

                DataTable table = new DataTable();
                DateTime time = c.Start;
                DateTime time2 = c.Endd;

                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @" update DetalleCalendar set IdCalendario ="+c.IdCalendario+
                    ", Folio ="+c.Folio+", Documento = '"+c.Documento+"', Descripcion = '"+c.Descripcion+
                    "', Start = '"+time.ToString(format)+"', Endd = '"+time2.ToString(format)+"', Title = '"+c.Title+"', Color = '"+c.Color+
                    "', AllDay ="+c.AllDay+", ResizableBeforeStart ="+c.ResizableBeforeStart+", ResizableBeforeEnd = "+c.ResizableBeforeEnd+", Draggable = " + c.Draggable +
                    " where IdDetalleCalendario = "+c.IdDetalleCalendario +@"";

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
