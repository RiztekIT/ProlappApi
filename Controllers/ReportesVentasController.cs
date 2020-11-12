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
    [RoutePrefix("api/ReporteVentas")]
    public class ReportesVentasController : ApiController
    {
        [Route("Fechas/{fechaini}/{fechafinal}")]
        public HttpResponseMessage GetFacturaFecha(string fechaini, string fechafinal)
        {
            DataTable table = new DataTable();

            //string query = @"Select Factura.* ,Cliente.* from Factura LEFT JOIN Cliente ON Factura.IdCliente = Cliente.IdClientes where FechaDeExpedicion between '" + fechaini + "' and '" + fechafinal + "' and (Factura.Estatus='Timbrada' or Factura.Estatus='Pagada') order by Factura.folio asc";
            string query = @"Select Factura.* ,Cliente.*, SUM(CONVERT(float,PagoCFDI.cantidad)), ReciboPago.FechaPago, DetalleFactura.ClaveProducto, DetalleFactura.Producto , DetalleFactura.PrecioUnitario, DetalleFactura.PrecioUnitarioDlls,DetalleFactura.Cantidad from Factura LEFT JOIN Cliente ON Factura.IdCliente = Cliente.IdClientes left join PagoCFDI on Factura.Id=PagoCFDI.IdFactura left join ReciboPago on ReciboPago.Id = PagoCFDI.IdReciboPago left join DetalleFactura on DetalleFactura.IdFactura=Factura.Id where FechaDeExpedicion between '" + fechaini + "' and '" + fechafinal + "' and (Factura.Estatus='Timbrada' or Factura.Estatus='Pagada') group by Factura.Id, Factura.IdCliente, Factura.Serie, Factura.Folio, Factura.Tipo, Factura.FechaDeExpedicion, Factura.LugarDeExpedicion, Factura.Certificado, Factura.NumeroDeCertificado, Factura.UUID, Factura.UsoDelCFDI, Factura.Subtotal, Factura.Descuento, Factura.ImpuestosRetenidos, Factura.ImpuestosTrasladados, Factura.Total, Factura.FormaDePago, Factura.MetodoDePago, Factura.Cuenta, Factura.Moneda, Factura.CadenaOriginal, Factura.SelloDigitalSAT, Factura.SelloDigitalCFDI, Factura.NumeroDeSelloSAT, Factura.RFCDelPAC, Factura.Observaciones, Factura.FechaVencimiento, Factura.OrdenDeCompra, Factura.TipoDeCambio, Factura.FechaDeEntrega, Factura.CondicionesDePago, Factura.Vendedor, Factura.Estatus, Factura.Ver, Factura.Usuario, Factura.SubtotalDlls, Factura.ImpuestosTrasladadosDlls, Factura.TotalDlls, Cliente.IdClientes, Cliente.Nombre, Cliente.RFC, Cliente.RazonSocial, Cliente.Calle, Cliente.Colonia, Cliente.CP, Cliente.Ciudad, Cliente.Estado, Cliente.NumeroInterior, Cliente.NumeroExterior, Cliente.ClaveCliente, Cliente.Estatus, Cliente.LimiteCredito, Cliente.DiasCredito, Cliente.MetodoPago, Cliente.UsoCFDI, Cliente.IdApi, Cliente.MetodoPagoCliente, Cliente.Vendedor, ReciboPago.FechaPago, DetalleFactura.ClaveProducto, DetalleFactura.Producto , DetalleFactura.PrecioUnitario, DetalleFactura.PrecioUnitarioDlls,DetalleFactura.Cantidad order by Factura.folio asc ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp2"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("DetalleFactura/{id}")]
        public HttpResponseMessage GetDetalleFacturaId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleFactura where IdFactura =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp2"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
    }
}
