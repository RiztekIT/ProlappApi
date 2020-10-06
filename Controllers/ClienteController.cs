
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
    [RoutePrefix("api/Cliente")]
    public class ClienteController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from Cliente order by Nombre";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("ID")]
        public HttpResponseMessage GetID()
        {
            DataTable table = new DataTable();

            string query = @"select * from Cliente order by IdClientes";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("IDN")]
        public HttpResponseMessage GetIDN()
        {
            DataTable table = new DataTable();

            string query = @"select * from Cliente where IdClientes<>78 order by IdClientes";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("Contacto")]
            public HttpResponseMessage GetContacto()
            {
                DataTable table = new DataTable();

                string query = @"select Cliente.*, contactoClientes.* from Cliente left join ContactoClientes on Cliente.IdClientes=ContactoClientes.idcliente order by Nombre";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return Request.CreateResponse(HttpStatusCode.OK, table);
            }
            [Route("Facturar")]
        public HttpResponseMessage GetFacturar()
        {
            DataTable table = new DataTable();

            string query = @"select * from Cliente where IdApi <> '' and Estatus ='Activo' order by Nombre";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("Id/{id}")]
        public HttpResponseMessage GetCliente(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Cliente where idClientes =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        public string Post(Cliente cliente)
        {
            try
            {


                DataTable table = new DataTable();
                string query = @"
                                Execute itInsertNuevoClientes '" + cliente.Nombre + "' , '" + cliente.RFC + "' , '" + cliente.RazonSocial + "' , '" + cliente.Calle + "' , '" + cliente.Colonia + "' , '" + cliente.CP + "' , '" + cliente.Ciudad + "' , '" + cliente.Estado + "' , '" + cliente.NumeroInterior + "' , '" + cliente.NumeroExterior +
                                "' , '" + cliente.ClaveCliente + "' , '" + cliente.Estatus + "' , '" + cliente.LimiteCredito + "' , '" + cliente.DiasCredito + "' , '" +
                                cliente.MetodoPago + "' , '" + cliente.UsoCFDI + "' , '" + cliente.IdApi + "' , '" + cliente.MetodoPagoCliente + "' , " + cliente.Vendedor + @"
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Cliente Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }

        [Route("DeleteCliente/{id}")]
        public string Delete(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                              Delete from Cliente where IdClientes = " + id;

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

        public string Put(Cliente cliente)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @"
                                exec etEditarCliente " + cliente.IdClientes + " , '" + cliente.Nombre + "' , '" + cliente.RFC + "' , '" + cliente.RazonSocial + "' , '" + cliente.Calle + "' , '" + cliente.Colonia + "' , '" + cliente.CP + "' , '" + cliente.Ciudad + "' , '" + cliente.Estado + "' , '" + cliente.NumeroInterior + "' , '" + cliente.NumeroExterior +
                                "' , '" + cliente.ClaveCliente + "' , '" + cliente.Estatus + "' , '" + cliente.LimiteCredito + "' , '" + cliente.DiasCredito + "' , '" +
                                cliente.MetodoPago + "' , '" + cliente.UsoCFDI + "' , '" + cliente.IdApi + "' , '" + cliente.MetodoPagoCliente + "' , " + cliente.Vendedor + @"
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Actualizacion Existosa";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;







            }
        }
        [Route("UID")]
        public string PostUID(Cliente cliente)
            {
                try
                {


                    DataTable table = new DataTable();

                    string query = @"update cliente set IdApi = "+cliente.IdApi+" where RFC='"+cliente.RFC+"'";

                    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                    using (var cmd = new SqlCommand(query, con))
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        da.Fill(table);
                    }



                    return "UID Actualizado";
                }
                catch (Exception exe)
                {
                    return "Failed to Update" + exe;







                }


            }


        ////////////////////////////////////////////////////////////////////////LOGIN DE CLIENTE /////////////////////////////////////////////////////////////////////////////////////

        //Obtener Usuario y Login 
        [Route("login")]

        public HttpResponseMessage Getlogin()
        {
            DataTable table = new DataTable();

            string query = @"select loginclientes.*, clientelogin.* from loginclientes left join clientelogin on clientelogin.RFC=loginclientes.RFC";

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
        [Route("{fecha}")]

        public HttpResponseMessage Getlogin(string fecha)
        {
            DataTable table = new DataTable();

            string query = @"select loginclientes.*, clientelogin.* from loginclientes left join clientelogin on clientelogin.RFC = loginclientes.RFC where fechainiciosesion between '" + fecha + "' and DATEADD(DAY,1,'" + fecha + "')";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("login")]
        public object PostAut(ClienteLogin clientelogin)
        {
            DataTable table = new DataTable();


            string query = @"select * from dbo.ClienteLogin where RFC ='" + clientelogin.RFC + "' and Contra='" + clientelogin.Contra + "'";

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
                    var RFC = table.Rows[0].Field<string>("RFC");
                    string format = "yyyy-MM-dd HH:mm:ss";
                    var fecha = DateTime.Now;
                    string query2 = @"insert into loginclientes values('" + RFC + "','" + jwtTokenString + "','" + fecha.ToString(format) + "','Dispositivo');";
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

        [Route("rfc/{RFC}")]

        public HttpResponseMessage GetloginRFC(string RFC)
        {
            DataTable table = new DataTable();

            string query = @"select* from Cliente where RFC =  '" + RFC + "'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        


        //////////////////////////////////////////////////////////////////////// FIN LOGIN DE CLIENTE /////////////////////////////////////////////////////////////////////////////////////


        ////////////////////////////////////////////////////////////////////////LOGIN CLIENTE MODULOS /////////////////////////////////////////////////////////////////////////////////////
        [Route("factura/{id}")]
        public HttpResponseMessage GetClienteFactura(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from factura left join cliente on factura.idCliente = cliente.idClientes where cliente.idClientes =" + id + "and factura.estatus = 'timbrada'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ordencompra/{id}")]
        public HttpResponseMessage GetClienteOrdenCompra(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from pedidos left join cliente on pedidos.idCliente = cliente.idClientes where cliente.idClientes =" + id+ "and pedidos.Estatus = 'cerrada'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("complementodepago/{id}")]
        public HttpResponseMessage GetClientecomplementodepago(int id)
        {
            DataTable table = new DataTable();

            string query = @"select* from ReciboPago left join cliente on ReciboPago.idCliente = cliente.idClientes where cliente.idClientes =" + id + "and ReciboPago.Estatus = 'Timbrada'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        /////////////////////////////////////////////////////////////////////// FIN CLIENTE MODULOS /////////////////////////////////////////////////////////////////////////////////////












    }
}
