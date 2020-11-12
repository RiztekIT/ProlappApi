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
    [RoutePrefix("api/reportes")]
    public class ReportesController : ApiController
    {
        // ======================================================================== REPORTES PEDIDOS =================================================================================


        [Route("Pedido")]
        public HttpResponseMessage GetPedidos()
        {
            DataTable table = new DataTable();

            string query = @"exec stSelectTablaPedidos";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("PedidoId/{id}")]
        public HttpResponseMessage GetPedidoId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from pedidos where idPedido =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Join de Pedido con cliente
        [Route("PedidoClienteId/{id}")]
        public HttpResponseMessage GetPedidoClienteId(int id)
        {
            DataTable table = new DataTable();

            string query = @"Select Pedidos.*, Cliente.* from Pedidos LEFT JOIN Cliente ON Pedidos.IdCliente = Cliente.IdClientes where Pedidos.IdCliente = "+id+" order by FechaDeExpedicion desc ;";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener detalles de pedido dependiendo el id del pedido
        [Route("DetallePedidoId/{id}")]
        public HttpResponseMessage GetDetallePedidoId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetallePedidos where IdPedido =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("PedidoFechas/{fechaini}/{fechafinal}/{id}")]
        public HttpResponseMessage GetPedidoFechas(string fechaini, string fechafinal, int id)
        {
            DataTable table = new DataTable();

            string query = @"Select Pedidos.* ,Cliente.* from Pedidos LEFT JOIN Cliente ON Pedidos.IdCliente = Cliente.IdClientes where Pedidos.FechaDeExpedicion between '" + fechaini + "' and '" + fechafinal + "' and Pedidos.IdCliente ="+id+" order by Pedidos.folio asc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("PedidoFechasClienteEstatus/{fechaini}/{fechafinal}/{id}/{estatus}")]
        public HttpResponseMessage GetPedidoFechasClienteEstatus(string fechaini, string fechafinal, int id, string estatus)
        {
            DataTable table = new DataTable();

            string query = @"Select Pedidos.* ,Cliente.* from Pedidos LEFT JOIN Cliente ON Pedidos.IdCliente = Cliente.IdClientes where Pedidos.FechaDeExpedicion between '" + fechaini + "' and '" + fechafinal + "' and Pedidos.IdCliente =" + id + " and Pedidos.Estatus ='"+estatus+"' order by Pedidos.folio asc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ReportePedido/{id}")]
        public HttpResponseMessage GetReportePedido(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Pedidos where IdCliente= " + id + " order by FechaDeExpedicion asc";
          

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ReportePedidosClienteEstatus/{id}/{estatus}")]
        public HttpResponseMessage GetReportePedidosClienteEstatus(int id, string estatus)
        {
            DataTable table = new DataTable();

            string query = @"Select Pedidos.* ,Cliente.* from Pedidos LEFT JOIN Cliente ON Pedidos.IdCliente = Cliente.IdClientes where Pedidos.IdCliente= " + id + " and Pedidos.Estatus = '" + estatus + "' order by FechaDeExpedicion asc";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ReportePedidoU/{id}")]
        public HttpResponseMessage GetReportePedidoU(int id)
        {
            DataTable table = new DataTable();


            string query = @"select Idcliente, Folio,FechaDeExpedicion , FechaVencimiento, Total, Moneda, TotalDlls from Pedidos where IdCliente= " + id + " and Moneda = 'USD' and Estatus ='Cerrada' order by FechaDeExpedicion asc";
            
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ReportePedidoM/{id}")]
        public HttpResponseMessage GetReportePedidoM(int id)
        {
            DataTable table = new DataTable();


            string query = @"select Idcliente, Folio,FechaDeExpedicion , FechaVencimiento, Total, Moneda, TotalDlls from Pedidos where IdCliente= " + id + " and Moneda = 'MXN' and Estatus ='Cerrada' order by FechaDeExpedicion asc";
           
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        // ======================================================================== REPORTES PEDIDOS =================================================================================

        // ======================================================================== REPORTES COTIZACIONES =================================================================================

        [Route("cotizaciones")]
        public HttpResponseMessage GetCotizaciones()
        {
            DataTable table = new DataTable();

            string query = @"select * from cotizaciones";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
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

        [Route("CotizacionId/{id}")]
        public HttpResponseMessage GetCotizacionId(int id)
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
        public HttpResponseMessage GetDetalleCotizacionId(int id)
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

        [Route("CotizacionesJoinCliente/{id}")]
        public HttpResponseMessage GetCotizacionesJoinCliente( int id)
        {
            DataTable table = new DataTable();

            string query = @"Select Cotizaciones.* ,Cliente.* from Cotizaciones LEFT JOIN Cliente ON Cotizaciones.IdCliente = Cliente.IdClientes where Cotizaciones.IdCliente = " + id + " order by Cotizaciones.folio asc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [Route("CotizacionesFechasClienteEstatus/{fechaini}/{fechafinal}/{id}/{estatus}")]
        public HttpResponseMessage GetCotizacionesFechasClienteEstatus(string fechaini, string fechafinal, int id, string estatus)
        {
            DataTable table = new DataTable();

            string query = @"Select Cotizaciones.* ,Cliente.* from Cotizaciones LEFT JOIN Cliente ON Cotizaciones.IdCliente = Cliente.IdClientes where Cotizaciones.FechaDeExpedicion between '" + fechaini + "' and '" + fechafinal + "' and Cotizaciones.IdCliente = " + id+ " and Cotizaciones.estatus = '" + estatus+"' order by Cotizaciones.folio asc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("CotizacionesFechasCliente/{fechaini}/{fechafinal}/{id}")]   
        public HttpResponseMessage GetCotizacionesFechasCliente(string fechaini, string fechafinal, int id)
        {
            DataTable table = new DataTable();

            string query = @"Select Cotizaciones.* ,Cliente.* from Cotizaciones LEFT JOIN Cliente ON Cotizaciones.IdCliente = Cliente.IdClientes where Cotizaciones.FechaDeExpedicion between '" + fechaini + "' and '" + fechafinal + "' and Cotizaciones.IdCliente = " + id + "  order by Cotizaciones.folio asc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ReporteCotizaciones/{id}")]
        public HttpResponseMessage GetReporteCotizaciones(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Cotizaciones where IdCliente= " + id + "  order by FechaDeExpedicion asc";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("ReporteCotizacionesClienteEstatus/{id}/{estatus}")]
        public HttpResponseMessage GetReporteCotizacionesClienteEstatus(int id, string estatus)
        {
            DataTable table = new DataTable();

            string query = @"Select Cotizaciones.* ,Cliente.* from Cotizaciones LEFT JOIN Cliente ON Cotizaciones.IdCliente = Cliente.IdClientes where Cotizaciones.IdCliente= " + id + " and Cotizaciones.Estatus = '" + estatus+"' order by FechaDeExpedicion asc";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ReporteCotizacionesU/{id}")]
        public HttpResponseMessage GetReporteCotizacionesU(int id)
        {
            DataTable table = new DataTable();

            string query = @"select Idcliente, Folio,FechaDeExpedicion , Vigencia, Total, Moneda, TotalDlls,TipoDeCambio from Cotizaciones where IdCliente= " + id + "and Moneda = 'USD' and Estatus ='Cerrada' order by FechaDeExpedicion asc";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ReporteCotizacionesM/{id}")]
        public HttpResponseMessage GetReporteCotizacionesM(int id)
        {
            DataTable table = new DataTable();

            string query = @"select Idcliente, Folio,FechaDeExpedicion , Vigencia, Total, Moneda, TotalDlls,TipoDeCambio from Cotizaciones where IdCliente=" + id + "and Moneda = 'MXN' and Estatus ='Cerrada' order by FechaDeExpedicion asc";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }



        // ======================================================================== REPORTES COTIZACIONES =================================================================================

        // ======================================================================== REPORTES COMPRAS =================================================================================

        [Route("getComprasID/{id}")]
        public HttpResponseMessage GetCompras()
        {
            DataTable table = new DataTable();

            string query = @"select * from Compras";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("getComprasID/{id}")]
        public HttpResponseMessage GetComprasID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Compras where IdCompra =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("getComprasFolio/{folio}")]
        public HttpResponseMessage GetComprasFolio(int folio)
        {
            DataTable table = new DataTable();

            string query = @"select * from Compras where Folio =" + folio;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("getDetalleComprasID/{id}")]
        public HttpResponseMessage GetDetalleComprasID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleCompra where IdCompra =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }




        [Route("ComprasFechas/{fechaini}/{fechafinal}/{id}")]
        public HttpResponseMessage GetComprasFechas(string fechaini, string fechafinal, int id)
        {
            DataTable table = new DataTable();

            string query = @"Select compras.* ,Proveedores.* from compras LEFT JOIN Proveedores ON compras.IdProveedor = Proveedores.IdProveedor where FechaElaboracion between '"+fechaini+"' and '"+fechafinal+"' and compras.IdProveedor = "+id+" order by compras.folio asc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("ComprasFechas/{fechaini}/{fechafinal}/{id}/{estatus}")]
        public HttpResponseMessage GetComprasFechasEstatus(string fechaini, string fechafinal, int id, string estatus)
        {
            DataTable table = new DataTable();

            string query = @"Select compras.* ,Proveedores.* from compras LEFT JOIN Proveedores ON compras.IdProveedor = Proveedores.IdProveedor where FechaElaboracion between '" + fechaini + "' and '" + fechafinal + "' and compras.IdProveedor = " + id + " and compras.Estatus = '"+estatus+"' order by compras.folio asc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ReporteCompras/{id}")]
        public HttpResponseMessage GetReporteCompras(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Compras where IdProveedor=" + id + " order by FechaElaboracion asc";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ReporteComprasT/{id}")]
        public HttpResponseMessage GetReporteComprasT(int id)
        {
            DataTable table = new DataTable();

            string query = @"select IdProveedor, Folio,FechaElaboracion , FechaEntrega, Total, SacosTotales, PesoTotal, Moneda, TotalDlls, TipoCambio from Compras where IdProveedor=" + id + " and Estatus ='Transito' order by FechaElaboracion asc";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ReporteComprasStatus/{id}/{status}")]
        public HttpResponseMessage GetReporteComprasstatus(int id,string status)
        {
            DataTable table = new DataTable();

            string query = @"select IdProveedor, Folio,FechaElaboracion , FechaEntrega, Total, SacosTotales, PesoTotal, Moneda, TotalDlls, TipoCambio from Compras where IdProveedor= " + id + " and Estatus ='"+ status + "' order by FechaElaboracion asc";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        // ======================================================================== REPORTES COMPRAS =================================================================================

        // ======================================================================== REPORTES ORDEN CARGA =================================================================================

        [Route("GetReporteOrdenCargaCliente/{id}")]
        public HttpResponseMessage GetReporteOrdenCargaCliente(int id)
        {
            DataTable table = new DataTable();

            string query = @"Select OrdenCarga.* ,Cliente.* from OrdenCarga LEFT JOIN Cliente ON OrdenCarga.IdCliente = Cliente.IdClientes where OrdenCarga.IdCliente = " + id + " order by OrdenCarga.folio asc";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetReporteOrdenCargaClienteEstatus/{id}/{estatus}")]
        public HttpResponseMessage GetReporteOrdenCargaClienteEstatus(int id, string estatus)
        {
            DataTable table = new DataTable();

            string query = @"Select OrdenCarga.* ,Cliente.* from OrdenCarga LEFT JOIN Cliente ON OrdenCarga.IdCliente = Cliente.IdClientes where OrdenCarga.IdCliente = " + id + " and OrdenCarga.Estatus = '"+estatus+"' order by OrdenCarga.folio asc";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetReporteOrdenCargaFechaCliente/{fechaini}/{fechafinal}/{id}")]
        public HttpResponseMessage GetReporteOrdenCargaFechaCliente(string fechaini, string fechafinal, int id)
        {
            DataTable table = new DataTable();

            string query = @"Select OrdenCarga.* ,Cliente.* from OrdenCarga LEFT JOIN Cliente ON OrdenCarga.IdCliente = Cliente.IdClientes where OrdenCarga.FechaExpedicion between '"+fechaini+"' and '"+fechafinal+"' and OrdenCarga.IdCliente = "+id+" order by OrdenCarga.folio asc";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetReporteOrdenCargaFechaClienteEstatus/{fechaini}/{fechafinal}/{id}/{estatus}")]
        public HttpResponseMessage GetReporteOrdenCargaFechaClienteEstatus(string fechaini, string fechafinal, int id, string estatus)
        {
            DataTable table = new DataTable();

            string query = @"Select OrdenCarga.* ,Cliente.* from OrdenCarga LEFT JOIN Cliente ON OrdenCarga.IdCliente = Cliente.IdClientes where OrdenCarga.FechaExpedicion between '" + fechaini + "' and '" + fechafinal + "' and OrdenCarga.IdCliente = " + id + " and OrdenCarga.Estatus = '"+estatus+"' order by OrdenCarga.folio asc";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        // ======================================================================== REPORTES ORDEN CARGA =================================================================================





        // ======================================================================== REPORTES ORDEN DESCARGA =================================================================================

        [Route("GetReporteOrdenDescargaProveedor/{id}")]
        public HttpResponseMessage GetReporteOrdenDescargaCliente(int id)
        {
            DataTable table = new DataTable();

            string query = @"Select OrdenDescarga.* ,Proveedores.* from OrdenDescarga LEFT JOIN Proveedores ON OrdenDescarga.IdProveedor = Proveedores.IdProveedor where OrdenDescarga.IdProveedor = " + id + " order by OrdenDescarga.folio asc";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetReporteOrdeDescargaProveedorEstatus/{id}/{estatus}")]
        public HttpResponseMessage GetReporteOrdeDescargaClienteEstatus(int id, string estatus)
        {
            DataTable table = new DataTable();

            string query = @"Select OrdenDescarga.* ,Proveedores.* from OrdenDescarga LEFT JOIN Proveedores ON OrdenDescarga.IdProveedor = Proveedores.IdProveedor where OrdenDescarga.IdProveedor = " + id + " and OrdenDescarga.Estatus = '" + estatus + "' order by OrdenDescarga.folio asc";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetReporteOrdenDescargaFechaProveedor/{fechaini}/{fechafinal}/{id}")]
        public HttpResponseMessage GetReporteOrdenDescargaFechaCliente(string fechaini, string fechafinal, int id)
        {
            DataTable table = new DataTable();

            string query = @"Select OrdenDescarga.* ,Proveedores.* from OrdenDescarga LEFT JOIN Proveedores ON OrdenDescarga.IdProveedor = Proveedores.IdProveedor where OrdenDescarga.FechaExpedicion between '"+fechaini+"' and '"+fechafinal+"' and OrdenDescarga.IdProveedor = "+id+" order by OrdenDescarga.folio asc";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetReporteOrdenDescargaFechaProveedorEstatus/{fechaini}/{fechafinal}/{id}/{estatus}")]
        public HttpResponseMessage GetReporteOrdenDescargaFechaClienteEstatus(string fechaini, string fechafinal, int id, string estatus)
        {
            DataTable table = new DataTable();

            string query = @"Select OrdenDescarga.* ,Proveedores.* from OrdenDescarga LEFT JOIN Proveedores ON OrdenDescarga.IdProveedor = Proveedores.IdProveedor where OrdenDescarga.FechaExpedicion between '" + fechaini + "' and '" + fechafinal + "' and OrdenDescarga.IdProveedor = " + id + " and OrdenDescarga.Estatus = '"+estatus+"' order by OrdenDescarga.folio asc";    


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        // ======================================================================== REPORTES ORDEN DESCARGA =================================================================================



        // ======================================================================== REPORTES CALIDAD =================================================================================

   



//obtener incidencias por fechas y procedencia
        [Route("GetIncidenciasFechasProcedencia/{fechaini}/{fechafinal}/{procedencia}")]
        public HttpResponseMessage GetIncidenciasFechasProcedencia(string fechaini, string fechafinal, string procedencia)
        {
            DataTable table = new DataTable();

            string query = @"Select * from incidencias where FechaElaboracion between '" + fechaini + "' and '" + fechafinal + "' and Procedencia = '"+procedencia+"' order by FechaElaboracion desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //obtener incidencias por fechas
        [Route("GetIncidenciasFechas/{fechaini}/{fechafinal}")]
        public HttpResponseMessage GetIncidenciasFechas(string fechaini, string fechafinal)
        {
            DataTable table = new DataTable();

            string query = @"Select * from incidencias where FechaElaboracion between '" + fechaini + "' and '" + fechafinal + "' order by FechaElaboracion desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener incidencias por estatus y procedencia
        [Route("GetIncidenciasEstatusProcedencia/{estatus}/{procedencia}")]
        public HttpResponseMessage GetIncidenciasEstatus(string estatus, string procedencia)
        {
            DataTable table = new DataTable();

            string query = @"select * from Incidencias where Estatus = '" + estatus + "' and Procedencia = '" + procedencia+"' order by FechaElaboracion desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        //obtener incidencias por estatus 
        [Route("GetIncidenciasEstatus/{estatus}")]
        public HttpResponseMessage GetIncidenciasEstatus(string estatus)
        {
            DataTable table = new DataTable();

            string query = @"select * from Incidencias where Estatus = '"+estatus+"' order by FechaElaboracion desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener incidencias por fechas y procedencia y estatus
        [Route("GetIncidenciasFechasProcedenciaEstatus/{fechaini}/{fechafinal}/{procedencia}/{estatus}")]
        public HttpResponseMessage GetIncidenciasFechasProcedenciaEstatus(string fechaini, string fechafinal, string procedencia, string estatus)
        {
            DataTable table = new DataTable();

            string query = @"Select * from incidencias where FechaElaboracion between '" + fechaini + "' and '" + fechafinal + "' and Procedencia = '" + procedencia + "' and Estatus = '"+estatus+"' order by FechaElaboracion desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener incidencias por fechas y estatus
        [Route("GetIncidenciasFechasEstatus/{fechaini}/{fechafinal}/{estatus}")]
        public HttpResponseMessage GetIncidenciasFechasEstatus(string fechaini, string fechafinal, string estatus)
        {
            DataTable table = new DataTable();

            string query = @"Select * from incidencias where FechaElaboracion between '" + fechaini + "' and '" + fechafinal + "' and Estatus = '" + estatus + "' order by FechaElaboracion desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }





        // ======================================================================== REPORTES CALIDAD =================================================================================


        // ======================================================================== REPORTES IMPORTACION =================================================================================

        //obtener traspasos por Bodega Origen-Destino
        [Route("GetTraspasoBodegas/{origen}/{destino}")]
        public HttpResponseMessage GetTraspasoBodegas(string origen, string destino)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenCarga where Cliente = 'Traspaso' and Origen = '"+origen+"' and Destino = '"+destino+"' order by FechaExpedicion desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener traspasos por Bodega Origen-Destino y Fechas de Expedicion
        [Route("GetTraspasoBodegasFechas/{origen}/{destino}/{fecha1}/{fecha2}")]
        public HttpResponseMessage GetTraspasoBodegasFechas(string origen, string destino, string fecha1, string fecha2)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenCarga where Cliente = 'Traspaso' and Origen = '"+origen+"' and Destino = '"+destino+"' and FechaExpedicion between '"+fecha1+"' and '"+fecha2+"' order by FechaExpedicion  desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener traspasos por Bodega Origen-Destino y Estatus
        [Route("GetTraspasoBodegasEstatus/{origen}/{destino}/{estatus}")]
        public HttpResponseMessage GetTraspasoBodegasEstatus(string origen, string destino, string estatus)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenCarga where Cliente = 'Traspaso' and Origen = '" + origen + "' and Destino = '" + destino + "' and Estatus = '"+ estatus +"' order by FechaExpedicion desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener traspasos por Bodega Origen-Destino, Fechas de Expedicion y Estatus
        [Route("GetTraspasoBodegasFechasEstatus/{origen}/{destino}/{fecha1}/{fecha2}/{estatus}")]
        public HttpResponseMessage GetTraspasoBodegasFechas(string origen, string destino, string fecha1, string fecha2, string estatus)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenCarga where Cliente = 'Traspaso' and Origen = '" + origen + "' and Destino = '" + destino + "' and FechaExpedicion between '" + fecha1 + "' and '" + fecha2 + "' and Estatus = '"+ estatus +"' order by FechaExpedicion  desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        // DOCUMENTOS


        //obtener documentos
        [Route("GetDocumentos")]
        public HttpResponseMessage GetDocumentos()
        {
            DataTable table = new DataTable();

            string query = @"select * from Documentos order by Vigencia ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener TIPO Y MODULO documentos
        [Route("GetDocumentoTipoModuloFolio")]
        public HttpResponseMessage GetDocumentoTipoModulo()
        {
            DataTable table = new DataTable();

            string query = @"select Modulo, Tipo, Folio from Documentos group by Modulo, Tipo, Folio ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener por fecha de vigencia
        [Route("GetDocumentoFechas/{fecha1}/{fecha2}")]
        public HttpResponseMessage GetDocumentoTipoModulo(string fecha1, string fecha2)
        {
            DataTable table = new DataTable();

            string query = @"select * from Documentos where Vigencia between '"+fecha1+"' and '"+fecha2+"' order by Vigencia";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Documentos por Modulo y Tipo y Folio del Documento
        [Route("GetDocumentoModuloTipoFolio/{modulo}/{tipo}/{folio}")]
        public HttpResponseMessage GetDocumentoModuloTipoFolio(string modulo, string tipo, string folio)
        {
            DataTable table = new DataTable();

            string query = @"select * from Documentos where Folio = "+folio+" and Modulo = '"+modulo+"' and Tipo = '"+tipo+"' order by Vigencia";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Documentos por Modulo y Tipo y Folio del Documento y fecha vigencia
        [Route("GetDocumentoModuloTipoFolioFecha/{modulo}/{tipo}/{folio}/{fecha1}/{fecha2}")]
        public HttpResponseMessage GetDocumentoModuloTipoFolioFecha(string modulo, string tipo, string folio, string fecha1, string fecha2)
        {
            DataTable table = new DataTable();

            string query = @"select * from Documentos where Folio = " + folio + " and Modulo = '" + modulo + "' and Tipo = '" + tipo + "' and Vigencia between '"+fecha1+"' and '"+fecha2+"' order by Vigencia";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }



        // ======================================================================== REPORTES IMPORTACION =================================================================================

        // ======================================================================== REPORTES TRAFICO  =================================================================================


        //obtener reporte Trafico sin filtro
        [Route("GetTrafico")]
        public HttpResponseMessage GetTrafico()
        {
            DataTable table = new DataTable();

            string query = @"select * from FacturaFlete left join OrdenCarga on FacturaFlete.IDOrdenCarga = OrdenCarga.IdOrdenCarga order by FacturaFlete.Estatus asc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener reporte Trafico por Fletera
        [Route("GetTraficoFletera/{fletera}")]
        public HttpResponseMessage GetTraficoFletera(string fletera)
        {
            DataTable table = new DataTable();

            string query = @"select * from FacturaFlete left join OrdenCarga on FacturaFlete.IDOrdenCarga = OrdenCarga.IdOrdenCarga where FacturaFlete.Fletera = '"+fletera+"' order by FacturaFlete.Estatus asc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener reporte Trafico por Id Orden Carga
        [Route("GetTraficoOC/{id}")]
        public HttpResponseMessage GetTraficoOC(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from FacturaFlete left join OrdenCarga on FacturaFlete.IDOrdenCarga = OrdenCarga.IdOrdenCarga where FacturaFlete.IDOrdenCarga ="+ id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener reporte Trafico por Estatus
        [Route("GetTraficoEstatus/{estatus}")]
        public HttpResponseMessage GetTraficoEstatus(string estatus)
        {
            DataTable table = new DataTable();

            string query = @"select * from FacturaFlete left join OrdenCarga on FacturaFlete.IDOrdenCarga = OrdenCarga.IdOrdenCarga  where FacturaFlete.Estatus = '"+estatus+"'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener reporte Trafico por Fletera y Estatus
        [Route("GetTraficoFleteraEstatus/{fletera}/{estatus}")]
        public HttpResponseMessage GetTraficoFleteraEstatus(string fletera, string estatus)
        {
            DataTable table = new DataTable();

            string query = @"select * from FacturaFlete left join OrdenCarga on FacturaFlete.IDOrdenCarga = OrdenCarga.IdOrdenCarga where FacturaFlete.Fletera ='" + fletera+ "' and FacturaFlete.Estatus ='" + estatus+"'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener reporte Trafico por IDOrdenCarga y Fletera
        [Route("GetTraficoOCFletera/{id}/{fletera}")]
        public HttpResponseMessage GetTraficoOCFletera(int id, string fletera)  
        {
            DataTable table = new DataTable();

            string query = @"select * from FacturaFlete left join OrdenCarga on FacturaFlete.IDOrdenCarga = OrdenCarga.IdOrdenCarga where FacturaFlete.Fletera = '"+fletera+"' and FacturaFlete.IDOrdenCarga ="+id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener reporte Trafico por IDOrdenCarga y Estatus
        [Route("GetTraficoOCEstatus/{id}/{estatus}")]
        public HttpResponseMessage GetTraficoOCEstatus(int id, string estatus)
        {
            DataTable table = new DataTable();

            string query = @"select * from FacturaFlete left join OrdenCarga on FacturaFlete.IDOrdenCarga = OrdenCarga.IdOrdenCarga  where FacturaFlete.Estatus = '"+estatus+"' and FacturaFlete.IDOrdenCarga = " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener reporte Trafico por IDOrdenCarga , Estatus y Fletera
        [Route("GetTraficoOCEstatusFletera/{id}/{estatus}/{fletera}")]
        public HttpResponseMessage GetTraficoOCEstatusFletera(int id, string estatus, string fletera)
        {
            DataTable table = new DataTable();

            string query = @"select * from FacturaFlete left join OrdenCarga on FacturaFlete.IDOrdenCarga = OrdenCarga.IdOrdenCarga  where FacturaFlete.Estatus = '"+estatus+"' and FacturaFlete.IDOrdenCarga = "+id+" and FacturaFlete.Fletera = '"+fletera+"'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener informacion para grafica Trafico por IdCliente
        [Route("GetTraficoIdCliente/{id}")]
        public HttpResponseMessage GetTraficoIdCliente(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from FacturaFlete left join OrdenCarga on FacturaFlete.IDOrdenCarga = OrdenCarga.IDOrdenCarga where OrdenCarga.IdCliente ="+id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        // ======================================================================== REPORTES TRAFICO  =================================================================================

        // ======================================================================== REPORTES CXP PAGOS  =================================================================================


        //obtener pagos sin filtros
        [Route("GetPagosGeneral")]
        public HttpResponseMessage GetPagosGeneral()
        {
            DataTable table = new DataTable();

            string query = @"select * from pagos";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener pagos tipo de Documento
        [Route("GetPagosTipoDocumento/{documento}")]
        public HttpResponseMessage GetPagosTipoDocumento(string documento)
        {
            DataTable table = new DataTable();

            string query = @"select * from pagos where TipoDocumento = '"+documento+"'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener pagos fechas
        [Route("GetPagosFechas/{fecha1}/{fecha2}")]
        public HttpResponseMessage GetPagosFechas(string fecha1, string fecha2)
        {
            DataTable table = new DataTable();

            string query = @"select * from pagos where FechaPago between '"+fecha1+"' and '"+fecha2+"'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //obtener pagos por tipo documento y fechas
        [Route("GetPagosTipoDocumentoFechas/{documento}/{fecha1}/{fecha2}")]
        public HttpResponseMessage GetPagosFechas(string documento, string fecha1, string fecha2)
        {
            DataTable table = new DataTable();

            string query = @"select * from pagos where TipoDocumento = '"+documento+"' and FechaPago between '" + fecha1 + "' and '" + fecha2 + "'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        // ======================================================================== REPORTES CXP PAGOS  =================================================================================









    }
}
