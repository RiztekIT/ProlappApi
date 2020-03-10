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

    [RoutePrefix("api/Vendedor")]
    public class VendedorController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from Vendedor";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("{id}")]
        public HttpResponseMessage GetVendedorId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Vendedor where IdVendedor = "+ id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        public string Post(Vendedor vendedor)
        {
            try
            {


                DataTable table = new DataTable();
                string query = " exec itInsertNuevoVendedor '" + vendedor.Nombre +  "'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Vendedor Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }
        [Route("{id}")]
        public string Delete(int Id)
        {
            try
            {


                DataTable table = new DataTable();

                    
                string query = @"
                              exec dtBorrarVendedor " + Id;

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

        public string Put(Vendedor vendedor)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @"
                                exec etEditarVendedor " + vendedor.IdVendedor + " , '" + vendedor.Nombre + "'";

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
    }
}
