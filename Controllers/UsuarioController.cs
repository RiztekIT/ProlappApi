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
using Microsoft.AspNet.SignalR;
using ProlappApi.Hubs;

namespace ProlappApi.Controllers
{
    public class UsuarioController : ApiController
    {


        private readonly IHubContext<AlertasHub> hub;

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

        //Obtener Usuario y Login 
        [Route("api/usuario/login")]

        public HttpResponseMessage Getlogin()
        {
            DataTable table = new DataTable();

            string query = @"select login.*, usuario.* from login left join Usuario on Usuario.NombreUsuario=login.username";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Usuario logeado en x semana
        [Route("api/usuario/loginSemana/{fecha1}/{fecha2}")]

        public HttpResponseMessage GetloginSemana(string fecha1, string fecha2)
        {
            DataTable table = new DataTable();

            string query = @" select username from login where fechainiciosesion between '"+fecha1+"' and DATEADD(DAY,1,'"+fecha2+"') group by username";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Usuario y Login por fechas 
        [Route("api/usuario/loginFechas/{fecha1}/{fecha2}")]

        public HttpResponseMessage GetloginFechas(string fecha1, string fecha2)
        {
            DataTable table = new DataTable();

            string query = @"select login.*, usuario.* from login left join Usuario on Usuario.NombreUsuario=login.username where fechainiciosesion between '"+fecha1+"' and DATEADD(DAY,1,'"+fecha2+"')";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Usuario y Login por fechas 
        [Route("api/usuario/loginFechasId/{fecha1}/{fecha2}/{id}")]

        public HttpResponseMessage GetloginFechasId(string fecha1, string fecha2)
        {
            DataTable table = new DataTable();

            string query = @"select login.*, usuario.* from login left join Usuario on Usuario.NombreUsuario=login.username where where usuario.IdUsuario = 1 and fechainiciosesion between '" + fecha1 + "' and DATEADD(DAY,1,'" + fecha2 + "')";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Usuario USERNAME y Login por fechas 
        [Route("api/usuario/loginFechasUser/{fecha1}/{fecha2}/{user}")]

        public HttpResponseMessage GetloginFechasUser(string fecha1, string fecha2, string user)
        {
            DataTable table = new DataTable();

            string query = @"select login.*, usuario.* from login left join Usuario on Usuario.NombreUsuario=login.username  where fechainiciosesion between '" + fecha1 + "' and DATEADD(DAY,1,'" + fecha2 + "')  and login.username = '"+user+"'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        //Obtener EarlistDate por Fechas y username
        [Route("api/usuario/loginEarliestDatesUser/{diaLimite}/{diaInicio}/{diaFin}/{mesInicio}/{mesFin}/{yearInicio}/{yearFin}/{user}")]

        public HttpResponseMessage GetloginEarliestDatesUser(int diaLimite, int diaInicio, int diaFin, int mesInicio, int mesFin, int yearInicio, int yearFin, string user)
        {
            DataTable table = new DataTable();

            //  string query = @"DECLARE @output TABLE (fecha DATETIME, device varchar(500)) declare @fechaInicio int = "+diaInicio+" declare @fechaFinal int = "+diaFin+
            //    " while @fechaInicio <= @fechaFinal begin insert into @output(fecha, device) select  TOP 1 MIN(login.fechainiciosesion) as fecha, login.dispositivo from login where login.fechainiciosesion between '"+mesInicio+
            //   "/'+ CAST(@fechaInicio as varchar(10)) +'/"+yearInicio+"' and DATEADD(DAY, 1, '"+mesFin+"/'+ CAST(@fechaInicio as varchar(10)) +'/"+yearFin+"') and login.username = '"+user+"' group by login.dispositivo set @fechaInicio = @fechaInicio + 1; end; SELECT * FROM @output";
            string query = "DECLARE @output TABLE (fecha DATETIME, device varchar(500)) declare @diaLimite int = "+diaLimite+" declare @fechaInicio int = "+diaInicio+" declare @fechaFinal int = "+diaFin+" declare @contador int = 0 " +
                "declare @mesInicio varchar(5) = "+mesInicio+" declare @mesFinal varchar(5) = "+mesFin+" declare @yearInicio varchar(5) = "+yearInicio+" declare @yearFinal varchar(5) = "+yearFin+
                " while @contador <= 6 begin insert into @output(fecha, device) select TOP 1 MIN(login.fechainiciosesion) as fecha, login.dispositivo from login where login.fechainiciosesion between +" +
                "@mesInicio + '/' + CAST(@fechaInicio as varchar(10)) + '/' + @yearInicio  and DATEADD(DAY, 1,   +@mesInicio + '/' + CAST(@fechaInicio as varchar(10)) + '/' + @yearInicio) and login.username = '"+user+"'" +
                " group by login.dispositivo set @fechaInicio = @fechaInicio + 1; if (@fechaInicio > @diaLimite) begin set @fechaInicio = 1 set @mesInicio = @mesFinal end set @contador = @contador + 1; end; " +
                "if (@mesInicio = 12 and @fechaInicio = 31) begin set @yearInicio = @yearFinal end SELECT* FROM @output";

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
        public object PostAut(Usuario usuario)
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

                    DataTable table2 = new DataTable();
                    var nombreusuario = table.Rows[0].Field<string>("NombreUsuario");
                    string format = "yyyy-MM-dd HH:mm:ss";
                    var fecha = DateTime.Now;
                    string query2 = @"insert into login values('"+nombreusuario+"','"+ jwtTokenString + "','"+fecha.ToLocalTime().ToString(format)+"','"+usuario.Dispositivo+"');";
                    using (var cmd2 = new SqlCommand(query2, con))
              
                    using (var da2 = new SqlDataAdapter(cmd2))
                    {
                        cmd2.CommandType = CommandType.Text;
                        da2.Fill(table2);
                    }
                        
                    return jwtTokenString;
                   // return jwtTokenString;
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
                                Execute itInsertNuevoUsuario '" + usuario.Nombre + "' , '" + usuario.NombreUsuario + "' , '" + usuario.ApellidoPaterno + "' , '" + usuario.ApellidoMaterno + "' , '" + usuario.Correo + "' , '" + usuario.Telefono + "' , '" + usuario.Contra + "' , '" + time.ToLocalTime().ToString(format) + @"'
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
                                exec etEditarUsuario " + usuario.IdUsuario + ",'" + usuario.Nombre + "','"+ usuario.NombreUsuario + "','" + usuario.ApellidoPaterno + "','" + usuario.ApellidoMaterno + "','" + usuario.Correo + "'," + usuario.Telefono + ",'" + usuario.Contra + "' , '" + time.ToLocalTime().ToString(format) + @"'
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
