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
    [RoutePrefix("api/NotaCredito")]
    public class NotaCreditoController : ApiController
    {

        //Select de tabla Nota Credito
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"Select * from NotaCredito";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Notas de Credito en base a una factura en especifico 
        [Route("NotaCreditoID/{id}")]
        public HttpResponseMessage GetNotaCreditoID(int id)
        {
            DataTable table = new DataTable();

            string query = @"Select * from NotaCredito where IdFactura =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("NotaDetalle")]
        public HttpResponseMessage GetFacturaCliente()
        {
            DataTable table = new DataTable();

            string query = @"exec jnNotaDetalleCredito";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Insert Nota Credito
        public string Post(NotaCredito nc)
        {
            try
            {


                DataTable table = new DataTable();
                //Las variables de fecha, son igualadas a un valor Datatime
                DateTime time = nc.FechaDeExpedicion;
                DateTime time2 = nc.FechaVencimiento;
                DateTime time3 = nc.FechaDeEntrega;
                //Al momento de insertar los valores de las fechas, estan seran insertadas con el formato 'Format'
                string format = "yyyy-MM-dd HH:mm:ss";
                //De esta manera no causara error al tratar de insertar fechas en la base de datos SQL
                //time.ToString(format)
                string query = @"
                                 insert into NotaCredito (IdCliente, IdFactura, Serie, Folio, Tipo, FechaDeExpedicion, LugarDeExpedicion,
                                Certificado, NumeroDeCertificado, UUID, UsoDelCFDI, Subtotal, Descuento, ImpuestosRetenidos, ImpuestosTrasladados,
                                Total, FormaDePago, MetodoDePago, Cuenta, Moneda, CadenaOriginal, SelloDigitalSAT, SelloDigitalCFDI, NumeroDeSelloSAT,
RFCDelPAC, Observaciones, FechaVencimiento, OrdenDeCompra, TipoDeCambio, FechaDeEntrega, CondicionesDePago, Vendedor,
Estatus, Ver, Usuario, SubtotalDlls, ImpuestosTrasladadosDlls, TotalDlls, Relacion)
values (" + nc.IdCliente + "," + nc.IdFactura + ", '" + nc.Serie + "','" + nc.Folio + "', '" + nc.Tipo + "', '" + time.ToString(format) + "', '" + nc.LugarDeExpedicion + "', '" + nc.Certificado + "', '" + nc.NumeroDeCertificado + "', '" + nc.UUID + "', '" + nc.UsoDelCFDI + "' , '" + nc.Subtotal + "','" + nc.Descuento +
"', '" + nc.ImpuestosRetenidos + "', '" + nc.ImpuestosTrasladados + "', '" + nc.Total + "', '" + nc.FormaDePago + "', '" + nc.MetodoDePago + "', '" + nc.Cuenta + "', '" + nc.Moneda + "', '" + nc.CadenaOriginal + "', '" + nc.SelloDigitalSAT + "', '" + nc.SelloDigitalCFDI + "', '" + nc.NumeroDeSelloSAT +
"', '" + nc.RFCdelPAC + "', '" + nc.Observaciones + "', '" + time2.ToString(format) + "', '" + nc.OrdenDeCompra + "', '" + nc.TipoDeCambio + "', '" + time3.ToString(format) + "', '" + nc.CondicionesDePago + "', '" + nc.Vendedor + "', '" + nc.Estatus + "', '" + nc.Ver + "', '" + nc.Usuario + "','" +
nc.SubtotalDlls + "', '" + nc.ImpuestosTrasladadosDlls + "', '" + nc.TotalDlls + "','"+ nc.Relacion +"' );";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Nota Credito Agregada";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }
        //Actualizar Nota Credito
        public string Put(NotaCredito nc)
        {
            try
            {
                DataTable table = new DataTable();
                DateTime time = nc.FechaDeExpedicion;
                DateTime time2 = nc.FechaVencimiento;
                DateTime time3 = nc.FechaDeEntrega;
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                update NotaCredito set IdCliente = "+ nc.IdCliente +", IdFactura ="+ nc.IdFactura+ ", Serie = '"+nc.Serie+"', Folio ="+ nc.Folio +", Tipo = '"+ nc.Tipo+ "', FechaDeExpedicion = '"+ time.ToString(format) +"', LugarDeExpedicion = '" +nc.LugarDeExpedicion+"', Certificado= '"+nc.Certificado+ "', NumeroDeCertificado = '"+nc.NumeroDeCertificado+
                                "',UUID='"+nc.UUID+"', UsoDelCFDI='"+nc.UsoDelCFDI+"', Subtotal='"+nc.Subtotal+"', Descuento= '"+nc.Descuento+"', ImpuestosRetenidos = '"+nc.ImpuestosRetenidos+"', ImpuestosTrasladados = '"+nc.ImpuestosTrasladados+"', Total = '"+nc.Total+"', FormaDePago = '"+nc.FormaDePago+"', MetodoDePago = '"+nc.MetodoDePago+"',"+
                                "Cuenta= '"+nc.Cuenta+"', Moneda='"+ nc.Moneda+"', CadenaOriginal='"+nc.CadenaOriginal+"', SelloDigitalSAT='"+nc.SelloDigitalSAT+"', SelloDigitalCFDI = '"+nc.SelloDigitalCFDI+"', NumeroDeSelloSAT = '"+nc.NumeroDeSelloSAT+"', RFCDelPAC = '"+nc.RFCdelPAC+"', Observaciones='"+nc.Observaciones+"', FechaVencimiento='"+time2.ToString(format)+
                    "',OrdenDeCompra ='"+nc.OrdenDeCompra+"', TipoDeCambio='"+nc.TipoDeCambio+"', FechaDeEntrega='"+time3.ToString(format)+"', CondicionesDePago='"+nc.CondicionesDePago+"', Vendedor='"+nc.Vendedor+"', Estatus='"+nc.Estatus+"', Ver='"+nc.Ver+"', Usuario='"+nc.Usuario+"', SubtotalDlls='"+nc.SubtotalDlls+"', ImpuestosTrasladadosDlls='"+nc.ImpuestosTrasladadosDlls+"',TotalDlls='"+nc.TotalDlls+"', Relacion='"+ nc.Relacion+ "' where IdNotaCredito ="+ nc.IdNotaCredito+";";

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

        //Eliminar Nota Credito
        public string Delete(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                              Delete NotaCredito where IdNotaCredito = " + id + ";";

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
        [Route("DeleteNotaCreada")]
        public string DeleteFacturaCreada()
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"delete from NotaCredito where Estatus='Creada';";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Se Elimino Correctamente";
            }
            catch (Exception ex)
            {
                return "Se produjo un error" + ex;
            }
        }
        //Eliminar todas las notas de credito correspondientes a cierta nota de credito
        [Route("DeleteAllDetalleNotaCredito/{id}")]
        public string DeleteAllDetalleNotaCredito(int id)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"delete from DetalleNotaCredito where IdNotaCredito="+id;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Se Elimino Correctamente";
            }
            catch (Exception ex)
            {
                return "Se produjo un error" + ex;
            }
        }

        //Obtener SUMA de cantidades de detalles en base a una factura
        [Route("SumaCantidades/{id}/{clave}")]
        public HttpResponseMessage GetUltimaNotaCredito(int id, string clave)
        {
            DataTable table = new DataTable();

            string query = @"select sum(Convert(float,Cantidad)) as Cantidad from DetalleNotaCredito where IdNotaCredito in (select IdNotaCredito from NotaCredito where IdFactura="+ id +") and ClaveProducto='"+ clave +"'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Ultima Nota Credito
        [Route("UltimaNotaCredito")]
        public HttpResponseMessage GetUltimaNotaCredito()
        {
            DataTable table = new DataTable();

            string query = @"select MAX(NotaCredito.IdNotaCredito) as Id from NotaCredito;";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener DetalleNotaCredito en base a IDNOTACREDITO
        [Route("DetalleNotaCreditoID/{id}")]
        public HttpResponseMessage GetDetalleNotaCreditoID(int id)
            {
            DataTable table = new DataTable();

            string query = @"select * from DetalleNotaCredito where IdNotaCredito ="+ id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        //Obtener Detalle Nota Credito
        [Route("DetalleNotaCredito")]
        public HttpResponseMessage GetDetalleNotaCredito()
        {
            DataTable table = new DataTable();

            string query = @"Select * from DetalleNotaCredito;";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Ultima Nota de credito de una factura en especifico por Id Factura
        [Route("UltimaNotaCreditoFacturaID/{id}")]
        public HttpResponseMessage GetUltimaNotaCreditoFacturaID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select TOP 1 * from NotaCredito where IdFactura ="+ id + " order by IdNotaCredito DESC";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //GET detalle Nota Credito por ID
        [Route("GetDetalleNotaCredito/{id}")]
        public HttpResponseMessage GetDetalleFacturaId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleNotaCredito where IdNotaCredito =" + id;


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Get ultimo Folio Nota Credito y sumarle 1
        [Route("GetUltimoFolio")]
        public HttpResponseMessage GetUltimoFolio()
        {
            DataTable table = new DataTable();

            string query = @"select MAX (Folio) + 1 as Folio from NotaCredito";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("NCClienteFolio/{id}")]
        public HttpResponseMessage GetFacturaClienteFolio(int id)
        {
            DataTable table = new DataTable();

            string query = @"Select NotaCredito.* ,Cliente.* from NotaCredito LEFT JOIN Cliente ON NotaCredito.IdCliente = Cliente.IdClientes where Folio =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Insertar Detalle Nota Credito
        [Route("InsertDetalleNotaCredito")]
        public string PostDetalleNotaCredito(DetalleNotaCredito d)
        {
            try
            {


                DataTable table = new DataTable();
                string query = @"
                              insert into DetalleNotaCredito (IdNotaCredito, ClaveProducto, Producto, Unidad, ClaveSAT, PrecioUnitario, Cantidad, Importe, Observaciones, TextoExtra, PrecioUnitarioDlls, ImporteDlls, ImporteIVA, ImporteIVADlls)
values ("+d.IdNotaCredito+", '"+d.ClaveProducto+"', '"+d.Producto+"', '"+d.Unidad+"', '"+d.ClaveSat+"', '"+d.PrecioUnitario+"', '"+d.Cantidad+"', '"+d.Importe+"', '"+d.Observaciones+"', '"+d.TextoExtra+"', '"+d.PrecioUnitarioDlls+"', '"+d.ImporteDlls+"','"+d.ImporteIVA+"', '"+d.ImporteIVADlls+"');";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Detalle Nota Credito Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }

        //actualizar Detalle Nota Credito
        [Route("UpdateDetalleNotaCredito")]
        public string PutDetalleNotaCredito(DetalleNotaCredito d)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                                update DetalleNotaCredito set IdNotaCredito =" + d.IdNotaCredito + ", ClaveProducto='" + d.ClaveProducto + "', Producto='" + d.Producto + "', Unidad='" + d.Unidad + "', ClaveSAT='" + d.ClaveSat + "', PrecioUnitario='" + d.PrecioUnitario + "', Cantidad='" + d.Cantidad + "', Importe='" + d.Importe + "', Observaciones='" + d.Observaciones + "',TextoExtra='" + d.TextoExtra +
                                "', PrecioUnitarioDlls='" + d.PrecioUnitarioDlls + "', ImporteDlls='" + d.ImporteDlls + "', ImporteIVA='" + d.ImporteIVA + "', ImporteIVADlls='" + d.ImporteIVADlls + "' where IdDetalleNotaCredito =" + d.IdDetalleNotaCredito + ";";

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

        //Eliminar Detalle Nota Credito
        [Route("DeleteDetalleNotaCredito/{id}")]
        public string DeleteNotaDetalleCredito(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                              Delete DetalleNotaCredito where IdDetalleNotaCredito = " + id + ";";

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


        //FIN

    }
}
