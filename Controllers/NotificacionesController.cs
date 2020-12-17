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
    [RoutePrefix("api/Notificaciones")]
    public class NotificacionesController : ApiController
    {


        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from Notificaciones";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetNotificacionId/{id}")]
        public HttpResponseMessage GetNotificacionId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Notificaciones where IdNotificacion = " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetDetalleNotificacionIdUsuario/{id}")]
        public HttpResponseMessage GetDetalleNotificacionIdUsuario(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleNotificacion where IdUsuarioDestino = " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetDetalleNotificacionIdUsuarioBandera/{id}/{bandera}")]
        public HttpResponseMessage GetDetalleNotificacionIdUsuarioBandera(int id, int bandera)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleNotificacion where IdUsuarioDestino = "+id+" and BanderaLeido = "+bandera;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetDetalleNotificacion")]
        public HttpResponseMessage GetDetalleNotificacion()
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleNotificacion";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetDetalleNotificacionId/{id}")]
        public HttpResponseMessage GetDetalleNotificacionId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleNotificacion where IdDetalleNotificacion = " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public HttpResponseMessage Post(Notificaciones n)
        {
           
            

                DateTime time = n.FechaEnvio;

                string format = "yyyy-MM-dd HH:mm:ss";

                DataTable table = new DataTable();

                string query = @"insert into Notificaciones (Folio, IdUsuario, Usuario, Mensaje, ModuloOrigen, FechaEnvio) OUTPUT inserted.* VALUES((select MAX(folio)+1 from notificaciones)," + n.IdUsuario+", '"+n.Usuario+"', '"+n.Mensaje+"', '"+n.ModuloOrigen+"', '"+time.ToString(format)+"')";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return Request.CreateResponse(HttpStatusCode.OK, table);
          
        }

        public string Put(Notificaciones n)
        {
            try
            {
                DateTime time = n.FechaEnvio;

                string format = "yyyy-MM-dd HH:mm:ss";

                DataTable table = new DataTable();

                string query = @"update Notificaciones set Folio = "+n.Folio+", IdUsuario = "+n.IdUsuario+", Usuario = '"+n.Usuario+"', Mensaje = '"+n.Mensaje+"', ModuloOrigen = '"+n.ModuloOrigen+"', FechaEnvio = '"+time.ToString(format)+"' where IdNotificacion = "+n.IdNotificacion+";";

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

        [Route("BorrarNotificacion/{id}")]
        public string PostBorrarNotificacion(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @"Delete Notificaciones where IdNotificacion = "+id;

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
        [Route("AddDetalleNotificacion")]
        public string PostDetalleNotificacion(DetalleNotificacion n)
        {
            try
            {

                DateTime time = n.FechaLeido;

                string format = "yyyy-MM-dd HH:mm:ss";

                DataTable table = new DataTable();

                string query = @"insert into DetalleNotificacion (IdNotificacion, IdUsuarioDestino, UsuarioDestino, BanderaLeido, FechaLeido) VALUES ("+n.IdNotificacion+", "+n.IdUsuarioDestino+", '"+n.UsuarioDestino+"', "+n.BanderaLeido+", '"+time.ToString(format)+"')";

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
        [Route("UpdateDetalleNotificacion")]
        public string PutDetalleNotificacion(DetalleNotificacion n)
        {
            try
            {
                DateTime time = n.FechaLeido;

                string format = "yyyy-MM-dd HH:mm:ss";

                DataTable table = new DataTable();

                string query = @"update DetalleNotificacion set IdNotificacion = "+n.IdNotificacion+", IdUsuarioDestino = "+n.IdUsuarioDestino+", UsuarioDestino = '"+n.UsuarioDestino+"', BanderaLeido = "+n.BanderaLeido+", FechaLeido = '"+time.ToString(format)+"' where IdDetalleNotificacion = "+n.IdDetalleNotificacion+";";

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

        [Route("BorrarDetalleNotificacion/{id}")]
        public string PostBorrarDetalleNotificacion(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @"Delete DetalleNotificacion where IdDetalleNotificacion = "+id;

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


        [Route("GetNotificacionJNDetalleNotificacionIdUsuario/{id}")]
        public HttpResponseMessage GetNotificacionJNDetalleNotificacionIdUsuario(int id)
        {
            DataTable table = new DataTable();

            string query = @"select* from notificaciones left join DetalleNotificacion on notificaciones.IdNotificacion = DetalleNotificacion.IdNotificacion where detallenotificacion.IdUsuarioDestino =  " + id;

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
