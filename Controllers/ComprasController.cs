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
    [RoutePrefix("api/Compras")]
    public class ComprasController : ApiController
    {

        public HttpResponseMessage Get()
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

            string query = @"select * from Compras where IdCompra ="+id;

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

        //Obtener Folio y sumarle 1
        [Route("CompraFolio")]
        public string GetFolio()
        {
            string folio;
            DataRow row;
            DataTable table = new DataTable();

            string query = @"select MAX ( Compras.Folio) + 1 as Folio from Compras";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
                row = table.Rows[0];
                folio = row["folio"].ToString();
            }

            return folio;
        }

       
        //Obtener ID ultima Compra
        [Route("GetUltimoIdCompra")]
        public string GetUtimoIdCompra()
        {
            string IdCompra;
            DataRow row;
            DataTable table = new DataTable();

            string query = @"select TOP 1 Compras.idCompra from Compras order by Compras.IdCompra desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
                row = table.Rows[0];
                IdCompra = row["IdCompra"].ToString();
            }

            return IdCompra;
        }


        [Route("DeleteCompra/{id}")]
        public string Delete(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" exec dtBorrarCompra " + id;

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

        public string Post(Compras c)
        {
            try
            {

                DataTable table = new DataTable();
                DateTime time = c.FechaElaboracion;
                DateTime time2 = c.FechaPromesa;
                DateTime time3 = c.FechaEntrega;
    
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"exec itInsertNuevaCompra " + c.Folio + ",'" + c.PO + "'," + c.IdProveedor + ",'" + c.Proveedor + "','" + c.Subtotal + "','" +
                                 c.Total + "','" + c.Descuento + "','" + c.ImpuestosRetenidos + "','" + c.ImpuestosTrasladados + "','" + c.Moneda + "','" +
                                 c.Observaciones + "','" + c.TipoCambio + "','" + c.CondicionesPago + "','" +  c.PesoTotal + "','" +  c.Estatus + "'," + 
                                 c.Factura + ",'" + c.Ver + "','" + time.ToString(format) + "','" + time2.ToString(format) + "','" + time3.ToString(format) + "','" +
                                 c.Comprador + "','" + c.SubtotalDlls + "','" + c.TotalDlls + "','" + c.DescuentoDlls + "','" + c.ImpuestosTrasladadosDlls + @"'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Se Agrego Correctamente";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;

            }
        }

        public string Put(Compras c)
        {
            try
            {

                DataTable table = new DataTable();
                DateTime time = c.FechaElaboracion;
                DateTime time2 = c.FechaPromesa;
                DateTime time3 = c.FechaEntrega;

                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec etEditarCompra " + c.IdCompra + "," + c.Folio + ",'" + c.PO + "'," + c.IdProveedor + ",'" + c.Proveedor + "','" + c.Subtotal + "','" +
                                 c.Total + "','" + c.Descuento + "','" + c.ImpuestosRetenidos + "','" + c.ImpuestosTrasladados + "','" + c.Moneda + "','" +
                                 c.Observaciones + "','" + c.TipoCambio + "','" + c.CondicionesPago + "','" + c.PesoTotal + "','" + c.Estatus + "'," +
                                 c.Factura + ",'" + c.Ver + "','" + time.ToString(format) + "','" + time2.ToString(format) + "','" + time3.ToString(format) + "','" + c.Comprador + "','" 
                                 + c.SubtotalDlls + "','" + c.TotalDlls + "','" + c.DescuentoDlls + "','" + c.ImpuestosTrasladadosDlls + @"'";

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

        [Route("AddDetalleCompra")]
        public string PostAddDetalleCompra(DetalleCompra dc)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @"
                                exec itInsertNuevoDetalleCompra "+ dc.IdCompra + ",'" + dc.ClaveProducto + "','" + dc.Producto + "','" + 
                                dc.Cantidad + "','" + dc.PrecioUnitario + "','" + dc.CostoTotal + "','" + dc.IVA + "','" + dc.Unidad + "','" + 
                                dc.Observaciones + "','" + dc.PrecioUnitarioDlls + "','" + dc.CostoTotalDlls + "','" + dc.IVADlls + @"'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Se Agrego Correctamente";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;

            }
        }

        [Route("EditDetalleCompra")]
        public string PutDetalleCompra(DetalleCompra dc)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @"
                                exec etEditarDetalleCompra " + dc.IdDetalleCompra + "," + dc.IdCompra + ",'" + dc.ClaveProducto + "','" + dc.Producto + "','" +
                                dc.Cantidad + "','" + dc.PrecioUnitario + "','" + dc.CostoTotal + "','" + dc.IVA + "','" + dc.Unidad + "','" + dc.Observaciones + "','" +
                                dc.PrecioUnitarioDlls + "','" + dc.CostoTotalDlls + "','" + dc.IVADlls + @"'";

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

        [Route("DeleteDetalleCompra/{id}")]
        public string DeleteDetalleCompra(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" exec dtBorrarDetalleCompra " + id;

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
