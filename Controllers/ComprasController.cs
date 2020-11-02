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

            string query = @"select * from Compras order by FechaElaboracion desc";

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

        //Obtener sumatoria de importes de DetalleCompra por IdCompra
        [Route("GetDCsumatoria/{id}")]
        public HttpResponseMessage GetDCsumatoria(int id)
        {
       
            DataTable table = new DataTable();

            string query = @"select sum(CAST(DetalleCompra.CostoTotal AS float)) as CostoTotal, sum(CAST(DetalleCompra.CostoTotalDlls AS float)) as CostoTotalDlls from DetalleCompra where IdCompra ="+id;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
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
        //Eliminar todos los Detalles Compra por IdCompra
        [Route("DeleteAllDetalleCompras/{id}")]
        public string DeleteAllDetalleCompras(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @"delete DetalleCompra where IdCompra =" + id;

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
                                 c.Observaciones + "','" + c.TipoCambio + "','" + c.CondicionesPago + "','" + c.SacosTotales + "','" + c.PesoTotal + "','" +  c.Estatus + "'," + 
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
                        update Compras SET Folio =" + c.Folio + ", PO = '"+c.PO+"',  IdProveedor = "+c.IdProveedor+", Proveedor ='"+c.Proveedor+"', Subtotal = '"+c.Subtotal+"', Total = '"+c.Total+
                    "', Descuento = '"+c.Descuento+"', ImpuestosRetenidos = '"+c.ImpuestosRetenidos+"', ImpuestosTrasladados = '"+c.ImpuestosTrasladados+"', Moneda = '"+c.Moneda+
                    "', Observaciones ='"+c.Observaciones+"', TipoCambio = '"+c.TipoCambio+"', CondicionesPago = '"+c.CondicionesPago + "', SacosTotales = '" +c.SacosTotales + "', PesoTotal = '" + c.PesoTotal +
                    "', Estatus = '" + c.Estatus + "', Factura = " + c.Factura + ", Ver = '" + c.Ver + "', FechaElaboracion = '" + time.ToString(format) + "', FechaPromesa = '" + time2.ToString(format) +
                    "', FechaEntrega = '" + time3.ToString(format) + "', Comprador = '" + c.Comprador + "', SubtotalDlls = '" + c.SubtotalDlls + "', TotalDlls = '" + c.TotalDlls +
                    "', DescuentoDlls ='" + c.DescuentoDlls + "', ImpuestosTrasladadosDlls = '" + c.ImpuestosTrasladadosDlls + "' where IdCompra = " + c.IdCompra + @";";

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
                                dc.Cantidad + "','" + dc.PesoxSaco + "','" + dc.PrecioUnitario + "','" + dc.CostoTotal + "','" + dc.IVA + "','" + dc.Unidad + "','" + 
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
                                update DetalleCompra SET IdCompra = "+dc.IdCompra+", ClaveProducto = '"+dc.ClaveProducto+"', Producto = '"+dc.Producto+"', Cantidad ='"+dc.Cantidad+ "', PesoxSaco ='" + dc.PesoxSaco +
                                "', PrecioUnitario ='"+dc.PrecioUnitario+"', CostoTotal = '"+dc.CostoTotal+"', IVA ='"+dc.IVA+"', Unidad='"+dc.Unidad+"',Observaciones='"+dc.Observaciones+
                                "',PrecioUnitarioDlls='"+dc.PrecioUnitario+"',CostoTotalDlls='"+dc.CostoTotalDlls+"',IVADlls='"+dc.IVADlls+"' where IdDetalleCompra = "+dc.IdDetalleCompra+ @";";

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

        ////////////////////////////////////////////////////////// compras historial


            
            [Route("GetComprasFecha/{Fecha}/{Fecha1}")]
        public HttpResponseMessage GetComprasFecha(string Fecha, string Fecha1)
        {
            DataTable table = new DataTable();

            string query = @"select* from compras where FechaElaboracion between '"+Fecha+"' and '"+Fecha1+"' and(Estatus = 'Terminada' or estatus = 'Creada' or estatus = 'Transito') and ver LIKE '%[0-9]%'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        

            [Route("GetComprasHistorial")]
        public HttpResponseMessage GetComprasHistorial()
        {
            DataTable table = new DataTable();

            string query = @"select* from compras where ver LIKE '%[0-9]%'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetComprasODDOD/{id}")]
        public HttpResponseMessage GetComprasODDOD(int id)
        {
            DataTable table = new DataTable();

            string query = @"select* from compras left join OrdenDescarga on compras.ver = OrdenDescarga.IdOrdenDescarga left join detalleordendescarga on ordendescarga.idordendescarga = detalleordendescarga.IdOrdendescarga  where compras.ver =" + id;
            
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
