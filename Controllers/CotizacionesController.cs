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
    [RoutePrefix("api/Cotizaciones")]
    public class CotizacionesController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"exec stSelectTablaCotizaciones";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("BorrarCotizacion/{id}")]
        public string Delete(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" exec dtBorrarCotizacion " + id;

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

        public string Put(Cotizaciones cotizaciones)
        {
            try
            {


                DataTable table = new DataTable();
                DateTime time = cotizaciones.FechaDeExpedicion;
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec etEditarCotizacion " + cotizaciones.IdCotizaciones + " , " +cotizaciones.IdCliente+ " , '" +cotizaciones.Nombre+ "' , '" +cotizaciones.RFC+ "' , '" +cotizaciones.Subtotal+"' , '" +cotizaciones.Total+ "' , '" +cotizaciones.Descuento+ "' , '"
                                +cotizaciones.SubtotalDlls+ "' , '" +cotizaciones.TotalDlls+ "' , '" +cotizaciones.DescuentoDlls+ "', '" +cotizaciones.Observaciones+ "' , '" +cotizaciones.Vendedor+ "' , '" +cotizaciones.Moneda+ "' , '" +time.ToString(format)+ "' , '" 
                                +cotizaciones.Flete+ "' , " +cotizaciones.Folio+ " , '" +cotizaciones.Telefono+ "' , '" +cotizaciones.Correo+ "' , " +cotizaciones.IdDireccion+ " , '" + cotizaciones.Estatus + "' ";

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

        public string Post(Cotizaciones cotizaciones)
        {
            try
            {


                DataTable table = new DataTable();
                DateTime time = cotizaciones.FechaDeExpedicion;
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @" Execute itInsertNuevaCotizacion " + cotizaciones.IdCliente + " , '" + cotizaciones.Nombre + "' , '" + cotizaciones.RFC + "' , '" + cotizaciones.Subtotal + "' , '" + cotizaciones.Total + "' , '" + cotizaciones.Descuento + "' , '"
                                + cotizaciones.SubtotalDlls + "' , '" + cotizaciones.TotalDlls + "' , '" + cotizaciones.DescuentoDlls + "' , '" + cotizaciones.Observaciones + "' , '" + cotizaciones.Vendedor + "' , '" + cotizaciones.Moneda + "' , '" + time.ToString(format) +
                                "' , '" + cotizaciones.Flete + "' , " + cotizaciones.Folio + " , '" + cotizaciones.Telefono + "' , '" + cotizaciones.Correo + "' , " + cotizaciones.IdDireccion + " , '" + cotizaciones.Estatus + "' "; 

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Cotizacion Agregada";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }
        [Route("Vendedor")]
        public HttpResponseMessage GetVendedor()
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
        [Route("CotizacionId/{id}")]
        public HttpResponseMessage GetPedidoId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Cotizaciones where IdCotizacion =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("DetalleCotizacionId/{id}")]
        public HttpResponseMessage GetDetallePedidoId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleCotizaciones where IdCotizacion =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("SumaImporte/{id}")]
        public HttpResponseMessage GetSumaImporte(int id)
        {
            DataTable table = new DataTable();

            string query = @"SELECT sum(CAST(Importe AS float)) as importe, sum(CAST(ImporteDlls AS float)) as ImporteDlls FROM DetalleCotizaciones where IdCotizacion = " + id;


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //Agregar Detalle Pedido
        [Route("InsertDetalleCotizacion")]
        public string PostDetallePedido(DetalleCotizacion dp)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"
                                Execute itInsertNuevoDetalleCotizacion " +dp.IdCotizacion+ " , '" +dp.ClaveProducto+ "' , '" +dp.Producto+ "' , '" +dp.Unidad+ "' , '" +dp.PrecioUnitario+ "' , '" +dp.PrecioUnitarioDlls+ "' , '" 
                                + dp.Cantidad + "' , '" + dp.Importe + "' , '" + dp.ImporteDlls + "' , '" + dp.Observaciones + "'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Detalle Pedido Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }
        //Editar Detalle Pedido
        [Route("EditDetalleCotizacion")]
        public string PutDetalleCotizacion(DetalleCotizacion dp)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"
                                Execute etEditarDetalleCotizacion " + dp.IdDetalleCotizacion + " , " +dp.IdCotizacion+ " , '" +dp.ClaveProducto+ "' , '" +dp.Producto+ "' , '" +dp.Unidad+ "' , '" +dp.PrecioUnitario+ "' , '" +dp.PrecioUnitarioDlls+ "' , '" 
                                +dp.Cantidad+ "' , '" +dp.Importe+ "' , '" +dp.ImporteDlls+ "' , '" +dp.Observaciones+ "'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Detalle Pedido Actualizado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error " + exe;
            }
        }

    }
}
