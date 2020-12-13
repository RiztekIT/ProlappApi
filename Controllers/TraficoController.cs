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
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;

namespace ProlappApi.Controllers
{
    [RoutePrefix("api/Trafico")]
    public class TraficoController : ApiController
    {

        //Obtener Usuario y Login 
        [Route("fletera")]
        public HttpResponseMessage GetFleteras()
        {
            DataTable table = new DataTable();

            string query = @"select * from fleteras";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Fleteras fleteras)
        {
            try
            {


                DataTable table = new DataTable();
                string query = @"
                                 insert into Fleteras values ('" + fleteras.Nombre + "' , '" + fleteras.Direccion +"')";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Fletera Agregada";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }

        public string Put(Fleteras fleteras)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @"update Fleteras set Nombre = '" + fleteras.Nombre + "',Direccion = '" + fleteras.Direccion + "' where idfletera=" + fleteras.IdFletera + "";
                                

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

        public string Delete(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                              exec dtBorrarProducto " + id;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Se Elimino Correctamente";
            }
            catch (Exception)
            {
                return "Se produjo un error";
            }
        }
    }
}
