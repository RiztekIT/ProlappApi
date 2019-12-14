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
    public class ProveedorController : ApiController
    {


        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"exec stSelectTablaProveedores";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        public string Post(Proveedor proveedor)
        {
            try
            {

                
                DataTable table = new DataTable();
                string query = @"
                                Execute itInsertNuevoProveedor '" + proveedor.Nombre + "' , '" + proveedor.RFC + "' , '" + proveedor.RazonSocial + "' , '" + proveedor.Calle + "' , '" + proveedor.Colonia + "' , '" + proveedor.CP + "' , '" + proveedor.Ciudad + "' , '" + proveedor.Estado + "' , '" + proveedor.NumeroInterior + "' , '" +proveedor.NumeroExterior + @"'
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
                              exec dtBorrarProvedor " + id;

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

        public string Put(Proveedor proveedor)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @"
                                exec etEditarProveedor " + proveedor.IdProveedor + " , '" + proveedor.Nombre + "' , '" + proveedor.RFC + "' , '" + proveedor.RazonSocial + "' , '" + proveedor.Calle + "' , '" + proveedor.Colonia + "' , '" + proveedor.CP + "' , '" + proveedor.Ciudad + "' , '" + proveedor.Estado + "' , '" + proveedor.NumeroInterior + "' , '" + proveedor.NumeroExterior + @"'
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
