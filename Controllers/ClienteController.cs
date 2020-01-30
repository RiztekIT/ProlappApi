
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

        public string Delete(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                              exec dtBorrarCliente " + id;

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






    }
}
