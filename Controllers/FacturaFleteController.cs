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
    [RoutePrefix("api/FacturaFlete")]
    public class FacturaFleteController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from dbo.FacturaFlete";

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
        public HttpResponseMessage GetFacturaid(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from facturaflete where IDFacturaFlete = " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("MasterID/{id}")]
        public HttpResponseMessage GetMasterID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleOrdenDescarga where IdOrdenDescarga =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(FacturaFlete facturaflete)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @"
                                exec itInsertarNuevaFacturaFlete " + facturaflete.Fletera + " , '" + facturaflete.Factura + "', " + facturaflete.IDPedido + " , " + facturaflete.IDOrdenCarga + " , '" +
                                facturaflete.Subtotal + "' , '" + facturaflete.IVA + "' , '" + facturaflete.Total + "' , '" + facturaflete.Estatus + @"'";

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
