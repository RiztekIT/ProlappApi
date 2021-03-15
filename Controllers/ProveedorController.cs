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
    [RoutePrefix("api/Proveedor")]
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
        [Route("getProveedorId/{id}")]
        public HttpResponseMessage getProveedorId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Proveedores where IdProveedor = " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        public HttpResponseMessage Post(Proveedor proveedor)
        {
            try
            {

                
                DataTable table = new DataTable();
                string query = @"
                                Execute itInsertNuevoProveedor '" + proveedor.Nombre + "' , '" + proveedor.RFC + "' , '" + proveedor.RazonSocial + "' , '" + proveedor.Calle + "' , '" + proveedor.Colonia + "' , '" + proveedor.CP + "' , '" + proveedor.Ciudad + "' , '" + proveedor.Estado + "' , '" + proveedor.NumeroInterior + "' , '" + proveedor.NumeroExterior +
                                "' , '" + proveedor.ClaveProveedor + "' , '" + proveedor.Estatus + "' , '" + proveedor.LimiteCredito + "' , '" + proveedor.DiasCredito + "' , '"  + 
                                proveedor.MetodoPago + "' , '"  + proveedor.UsoCFDI + @"'
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return Request.CreateResponse(HttpStatusCode.OK, table);
            }
            catch (Exception exe)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, exe);
            }
        }

        public class Query
        {
            public string consulta { get; set; }
        }


        [Route("consulta")]
        public HttpResponseMessage PostServicios(Querys consulta)
        {
            DataTable table = new DataTable();

            string query = @"" + consulta.consulta + "";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
            //return consulta;
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



                return "Se Elimino Correctamente";
            }
            catch (Exception)
            {
                return "Se produjo un error";
            }
        }

        public string Put(Proveedor proveedor)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @"
                                exec etEditarProveedor " + proveedor.IdProveedor + " , '" + proveedor.Nombre + "' , '" + proveedor.RFC + "' , '" + proveedor.RazonSocial + "' , '" + proveedor.Calle + "' , '" + proveedor.Colonia + "' , '" + proveedor.CP + "' , '" + proveedor.Ciudad + "' , '" + proveedor.Estado + "' , '" + proveedor.NumeroInterior + "' , '" + proveedor.NumeroExterior +
                                "' , '" + proveedor.ClaveProveedor + "' , '" + proveedor.Estatus + "' , '" + proveedor.LimiteCredito + "' , '" + proveedor.DiasCredito + "' , '" +
                                proveedor.MetodoPago + "' , '" + proveedor.UsoCFDI + @"'
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
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;







            }
        }






    }
}
