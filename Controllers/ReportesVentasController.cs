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

            string query = @"Select Factura.* ,Cliente.* from Factura LEFT JOIN Cliente ON Factura.IdCliente = Cliente.IdClientes where FechaDeExpedicion between '" + fechaini + "' and '" + fechafinal + "' and (Factura.Estatus='Timbrada' or Factura.Estatus='Pagada') order by Factura.folio asc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
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
