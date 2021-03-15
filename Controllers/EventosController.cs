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
    [RoutePrefix("api/Eventos")]
    public class EventosController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from Eventos";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("EventoID/{id}")]
        public HttpResponseMessage GetEventoId(int id)
        {
            DataTable table = new DataTable();

            string query = @"Select * from Eventos where IdEventos =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("DeleteEvento/{id}")]
        public string Delete(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                               Delete from eventos where Ideventos = " + id;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Se Elimino Correctamente";
            }
            catch (Exception ex)
            {
                return "Se produjo un error" + ex;
            }
        }


        public string Post(Evento evento)
        {
            try
            {
                DateTime time = evento.Fecha;
                string format = "yyyy-MM-dd HH:mm:ss";


                DataTable table = new DataTable();
                string query = @"

                                insert into Eventos (IdUsuario, Movimiento, Fecha, Autorizacion) 
                                    Values(" + evento.IdUsuario + " , '" + evento.Movimiento + "' , '"
                                    + time.ToString(format) + "' , '" + evento.Autorizacion + "')";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Evento Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }

        public string Put(Evento evento)
        {
            try
            {


                DataTable table = new DataTable();

                DateTime time = evento.Fecha;
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"update Eventos set 

                                IdUsuario = " + evento.IdUsuario + @",
                               Movimiento = '" + evento.Movimiento + @"',
                               Fecha = '" + time.ToString(format) + @"',
                               Autorizacion = '" + evento.Autorizacion + "' " +
                               "    where  IdEventos = " + evento.IdEventos + @"";


                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Actualizacion Exitosa";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;


            }
        }


    }
}
