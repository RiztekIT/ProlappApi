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

            string query = @"Select Pedidos.* ,Cliente.* from Pedidos LEFT JOIN Cliente ON Pedidos.IdCliente = Cliente.IdClientes where FechaDeExpedicion between '" + fechaini + "' and '" + fechafinal + "' and Pedidos.IdCliente ="+id+" order by Pedidos.folio asc";

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

            string query = @"Select Pedidos.* ,Cliente.* from Pedidos LEFT JOIN Cliente ON Pedidos.IdCliente = Cliente.IdClientes where FechaDeExpedicion between '" + fechaini + "' and '" + fechafinal + "' and Pedidos.IdCliente =" + id + " and Pedidos.Estatus ='"+estatus+"' order by Pedidos.folio asc";

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

        [Route("CotizacionesJoinCliente{id}")]
        public HttpResponseMessage GetCotizacionesJoinCliente( int id)
        {
            DataTable table = new DataTable();

            string query = @"Select Cotizaciones.* ,Cliente.* from Cotizaciones LEFT JOIN Cliente ON Cotizaciones.IdCliente = Cliente.IdClientes where IdCliente = " + id + " order by Cotizaciones.folio asc";

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

            string query = @"Select Cotizaciones.* ,Cliente.* from Cotizaciones LEFT JOIN Cliente ON Cotizaciones.IdCliente = Cliente.IdClientes where FechaDeExpedicion between '" + fechaini + "' and '" + fechafinal + "' and IdCliente = "+id+" and estatus = '"+ estatus+"' order by Cotizaciones.folio asc";

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

            string query = @"Select Cotizaciones.* ,Cliente.* from Cotizaciones LEFT JOIN Cliente ON Cotizaciones.IdCliente = Cliente.IdClientes where FechaDeExpedicion between '" + fechaini + "' and '" + fechafinal + "' and IdCliente = " + id + "  order by Cotizaciones.folio asc";

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












    }
}
