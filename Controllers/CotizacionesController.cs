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
    [RoutePrefix("api/Cotizacion")]
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

        [Route("GetCotizacionesDetalleCotizaciones/{ClaveProducto}/{Id}")]
        public HttpResponseMessage GetCotizacionesDetalleCotizaciones(String ClaveProducto, int Id)
        {
            DataTable table = new DataTable();

            string query = @"
                             exec jnProductoDetalleProducto '" + ClaveProducto + "'," + Id + ";";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener detalles de Cotizaciones dependiendo el id de la cotizacion
        [Route("DetalleCotizacionesId/{id}")]
        public HttpResponseMessage GetDetalleCotizacionesId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleCotizaciones where IdCotizacion = " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        public string Put(Cotizaciones cotizaciones)
        {
            try
            {


                DataTable table = new DataTable();
                DateTime time = cotizaciones.FechaDeExpedicion;
                DateTime time2 = cotizaciones.Vigencia;
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec etEditarCotizacion " + cotizaciones.IdCotizacion + " , " +cotizaciones.IdCliente+ " , '" +cotizaciones.Nombre+ "' , '" +cotizaciones.RFC+ "' , '" +cotizaciones.Subtotal+"' , '" +cotizaciones.Total+ "' , '" +cotizaciones.Descuento+ "' , '"
                                +cotizaciones.SubtotalDlls+ "' , '" +cotizaciones.TotalDlls+ "' , '" +cotizaciones.DescuentoDlls+ "', '" +cotizaciones.Observaciones+ "' , '" +cotizaciones.Vendedor+ "' , '" +cotizaciones.Moneda+ "' , '" +time.ToString(format)+ "' , '" 
                                +cotizaciones.Flete+ "' , " +cotizaciones.Folio+ " , '" +cotizaciones.Telefono+ "' , '" +cotizaciones.Correo+ "' , " +cotizaciones.IdDireccion+ " , '" + cotizaciones.Estatus + "' , " +cotizaciones.TipoDeCambio+ " , '" +time2.ToString(format)+ "' ";

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
                DateTime time2 = cotizaciones.Vigencia;
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @" Execute itInsertNuevaCotizacion " + cotizaciones.IdCliente + " , '" + cotizaciones.Nombre + "' , '" + cotizaciones.RFC + "' , '" + cotizaciones.Subtotal + "' , '" + cotizaciones.Total + "' , '" + cotizaciones.Descuento + "' , '"
                                + cotizaciones.SubtotalDlls + "' , '" + cotizaciones.TotalDlls + "' , '" + cotizaciones.DescuentoDlls + "' , '" + cotizaciones.Observaciones + "' , '" + cotizaciones.Vendedor + "' , '" + cotizaciones.Moneda + "' , '" + time.ToString(format) +
                                "' , '" + cotizaciones.Flete + "' , " + cotizaciones.Folio + " , '" + cotizaciones.Telefono + "' , '" + cotizaciones.Correo + "' , " + cotizaciones.IdDireccion + " , '" + cotizaciones.Estatus + "' , " +cotizaciones.TipoDeCambio+ " , '" + time2.ToString(format) + "' "; 

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
        [Route("ProductoDetalleProducto/{ClaveProducto}/{Id}")]
        public HttpResponseMessage GetProductoDetalleProducto(String ClaveProducto, int Id)
        {
            DataTable table = new DataTable();

            string query = @"
                             exec jnProductoDetalleProductoCotizaciones '" + ClaveProducto + "'," + Id + ";";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("DeleteDetalleCotizacion/{id}")]
        public string DeleteDetalleCotizacion(int id)
        {
            try
            {

                DataTable table = new DataTable();


                string query = @"
                              exec dtBorrarDetalleCotizacion " + id;

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
        [Route("Folio")]
        public HttpResponseMessage GetFolio()
        {
            DataTable table = new DataTable();

            string query = @"select MAX(Folio) as Folio from Cotizaciones";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("UltimaCotizacion")]
        public HttpResponseMessage GetUtimoPedido()
        {
            DataTable table = new DataTable();

            string query = @"select MAX(IdCotizacion) as IdCotizacion from Cotizaciones";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("GetProspecto")]
        public HttpResponseMessage GetProspectos()
        {
            DataTable table = new DataTable();

            string query = @"select * From Prospecto";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("InsertProspecto")]
        public HttpResponseMessage PostProspecto(Prospecto prospecto)
        {
            DataTable table = new DataTable();

            string query = @" Exec itInsertNuevoProspecto '" +prospecto.Nombre + "' , '" +prospecto.Correo+ "' , " +prospecto.Telefono+ " , '" 
                            +prospecto.Direccion+ "' , '" +prospecto.Empresa+ "' , '" +prospecto.Estatus+ "' , " +prospecto.IdCotizacion+ " ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("UpdateProspecto")]
        public HttpResponseMessage PutProspecto(Prospecto prospecto)
        {
            DataTable table = new DataTable();

            string query = @" Exec etEditarProspecto " +prospecto.IdProspecto+ " , '" + prospecto.Nombre + "' , '" + prospecto.Correo + "' , " + prospecto.Telefono + " , '"
                            + prospecto.Direccion + "' , '" + prospecto.Empresa + "' , '" +prospecto.Estatus+ "' , " +prospecto.IdCotizacion+ " ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("BorrarProspecto/{id}")]
        public string DeleteProspecto(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" exec dtBorrarProspecto " + id;

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

    }


}
