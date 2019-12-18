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
    public class ClienteController : ApiController
    {




        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"exec stSelectTablaCliente";

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
                                cliente.MetodoPago + "' , '" + cliente.UsoCFDI + @"'
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
            catch (Exception exe)
            {
                return "Failed to Add" + exe;
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



                return "Deleted Successfully";
            }
            catch (Exception)
            {
                return "Failed to Delete";
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
                                cliente.MetodoPago + "' , '" + cliente.UsoCFDI + @"'
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
            catch (Exception exe)
            {
                return "Failed to Update" + exe;







            }
        }






    }
}
