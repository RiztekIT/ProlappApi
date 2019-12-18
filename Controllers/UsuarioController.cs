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
    public class UsuarioController : ApiController
    {


        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from dbo.Usuario";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        public string Post(Usuario usuario)
        {
            try
            {

                //Las variables de fecha, son igualadas a un valor Datatime
                DateTime time = usuario.FechaUltimoAcceso;
                //Al momento de insertar los valores de las fechas, estan seran insertadas con el formato 'Format'
                string format = "yyyy-MM-dd HH:mm:ss";
                //De esta manera no causara error al tratar de insertar fechas en la base de datos SQL
                //time.ToString(format)
                DataTable table = new DataTable();
                string query = @"
                                Execute itInsertNuevoUsuario '" + usuario.Nombre + "' , '" + usuario.NombreUsuario + "' , '" + usuario.ApellidoPaterno + "' , '" + usuario.ApellidoMaterno + "' , '" + usuario.Correo + "' , '" + usuario.Telefono + "' , '" + usuario.Contra + "' , '" + time.ToString(format) + @"'
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Added Successfully";
            }
            catch (Exception ex)
            {
                return "Failed to Add" + ex;
            }
        }

        public string Delete(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                              exec dtBorrarUsuarios " + id;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Deleted Successfully";
            }
            catch (Exception)
            {
                return "Failed to Delete";
            }
        }

        public string Put(Usuario usuario)
        {
            try
            {
                //Las variables de fecha, son igualadas a un valor Datatime
                DateTime time = usuario.FechaUltimoAcceso;
                //Al momento de insertar los valores de las fechas, estan seran insertadas con el formato 'Format'
                string format = "yyyy-MM-dd HH:mm:ss";
                //De esta manera no causara error al tratar de insertar fechas en la base de datos SQL
                //time.ToString(format)


                DataTable table = new DataTable();

                string query = @"
                                exec etEditarUsuario " + usuario.IdUsuario + ",'" + usuario.Nombre + "','"+ usuario.NombreUsuario + "','" + usuario.ApellidoPaterno + "','" + usuario.ApellidoMaterno + "','" + usuario.Correo + "'," + usuario.Telefono + ",'" + usuario.Contra + "' , '" + time.ToString(format) + @"'
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Updated Successfully";
            }
            catch (Exception)
            {
                return "Failed to Update";
            }
        }




    }
}
