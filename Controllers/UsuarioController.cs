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
        //Obtener Usuario por NombreUsuario
        [Route("api/usuario/userinfo/{nombre}")]
        public HttpResponseMessage GetUserInfo(string nombre)
        {
            DataTable table = new DataTable();

            string query = @"select * from Usuario where NombreUsuario ='" + nombre +"';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("api/usuario/login/")]
        public string PostAut(Usuario usuario)
        {
            DataTable table = new DataTable();
            

            string query = @"select * from dbo.Usuario where NombreUsuario ='" +usuario.NombreUsuario+"' and contra='"+usuario.Contra+"'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
                if (table.Rows.Count > 0)
                {
                    //var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
                    var secretKey = "RiztekTKey123456";
                    var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
                    var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
                    var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];
                    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
                    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "Ivan2019") });
                    IdentityModelEventSource.ShowPII = true;
                    
                    var tokenhandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                    var jwtSecurityToken = tokenhandler.CreateJwtSecurityToken(
                        audience: audienceToken,
                        issuer: issuerToken,
                        subject: claimsIdentity,
                        notBefore: DateTime.UtcNow,
                        expires: DateTime.UtcNow.AddMinutes(1),
                        signingCredentials: signingCredentials
                        );
                    var jwtTokenString = tokenhandler.WriteToken(jwtSecurityToken);
                    return jwtTokenString;
                }
                else
                {
                    return "Error";
                }
            }

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



                return "Usuario Agregado";
            }
            catch (Exception ex)
            {
                return "Se produjo un error" + ex;
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



                return "Se Elimino Correctamente";
            }
            catch (Exception)
            {
                return "Se produjo un error";
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



                return "Se Actualizo Correctamente";
            }
            catch (Exception)
            {
                return "Se produjo un error";
            }
        }




    }
}
