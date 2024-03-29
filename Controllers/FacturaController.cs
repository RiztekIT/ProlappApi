﻿using System;
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
    [RoutePrefix("api/Factura")]
    public class FacturaController : ApiController
    {

        
        //Select de tabla factura
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"exec stSelectTablaFactura";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("FacturaCliente")]
        public HttpResponseMessage GetFacturaCliente()
        {
            DataTable table = new DataTable();

            string query = @"exec jnFacturaCliente";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("FacturaidCliente/{id}")]
        public HttpResponseMessage GetFacturaidCliente(int id)
        {
            DataTable table = new DataTable();

            string query = @"Select Factura.* ,Cliente2.* from Factura LEFT JOIN Cliente2 ON Factura.IdCliente = Cliente2.IdClientes where Factura.IdCliente=" + id + "  order by FechaDeExpedicion DESC";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [Route("FacturaCliente/{id}")]
        public HttpResponseMessage GetFacturaClienteId(int id)
        {
            DataTable table = new DataTable();

            string query = @"Select Factura.* ,Cliente2.* from Factura LEFT JOIN Cliente2 ON Factura.IdCliente = Cliente2.IdClientes where id =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("FacturaFechas/{fechaini}/{fechafinal}")]
        public HttpResponseMessage GetFacturaFecha(string fechaini, string fechafinal)
        {
            DataTable table = new DataTable();

            string query = @"Select Factura.* ,Cliente2.* from Factura LEFT JOIN Cliente2 ON Factura.IdCliente = Cliente2.IdClientes where FechaDeExpedicion between '"+fechaini+"' and '"+fechafinal+ "' and (Factura.Estatus='Timbrada' or Factura.Estatus='Pagada') order by Factura.folio asc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [Route("FacturaFechasReporte/{id}/{fechaini}/{fechafinal}")]
        public HttpResponseMessage GetFacturaFechasReporte(int id, string fechaini, string fechafinal)
        {
            DataTable table = new DataTable();

            string query = @"Select Factura.* ,Cliente.* from Factura LEFT JOIN Cliente ON Factura.IdCliente = Cliente.IdClientes where Factura.IdCliente = " + id + " and Factura.FechaDeExpedicion between '" + fechaini + "' and '" + fechafinal + "' and (Factura.Estatus='Timbrada' or Factura.Estatus='Pagada' or Factura.Estatus='Cancelada') order by Factura.folio asc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("FacturaClienteFolio/{id}")]
        public HttpResponseMessage GetFacturaClienteFolio(int id)
        {
            DataTable table = new DataTable();

            string query = @"Select Factura.* ,Cliente2.* from Factura LEFT JOIN Cliente2 ON Factura.IdCliente = Cliente2.IdClientes where Folio =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [Route("id/{id}")]
        public HttpResponseMessage GetFactura(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Factura where Id=" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Recibo pago JOIN pagos CFDI correspondientes a esa Factura
        [Route("PagoCFDI/{id}")]
        public HttpResponseMessage GetPagoCFDI(int id)
        {
            DataTable table = new DataTable();

            string query = @" select PagoCFDI.*, ReciboPago.* from PagoCFDI left join ReciboPago ON PagoCFDI.IdReciboPago = ReciboPago.Id where IdFactura =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }



        //Select Ultima Factura Creada
        [Route("UltimaFactura")]
        public HttpResponseMessage GetUltimaFactura()
        {
            DataTable table = new DataTable();

            
            //string query = @"select MAX (Factura.Id) + 1 as Id from Factura";
            string query = @"select IDENT_CURRENT( 'Factura' ) as Id from Factura";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //Select Productos
        [Route("getProductos")]
        public HttpResponseMessage GetProductos()
        {
            DataTable table = new DataTable();

            string query = @"select * from producto where Estatus='ActivoFact'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //Select  de Cierto Detalle de Factura
        [Route("DetalleFactura/{id}")]
        public HttpResponseMessage GetDetalleFacturaId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleFactura where IdFactura =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        // Select Detalle Factura por id
        [Route("DetalleFacturaProducto/{id}")]
        public HttpResponseMessage GetDetalleFacturaProductoId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select DetalleFactura.*, Producto.* from DetalleFactura LEFT JOIN Producto on DetalleFactura.ClaveProducto=Producto.ClaveProducto where IdFactura =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //Select Folio 
        [Route("Folio")]
        public string GetFolio()
        {
            string folio;
            DataRow row;
            DataTable table = new DataTable();

            string query = @"select MAX ( Factura.Folio) + 1 as Folio from Factura";

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
        //Select detalle factura
        [Route("DetalleFactura")]
        public HttpResponseMessage GetDetalleFactura()
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleFactura";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Select Cliente dependiendo el IdFactura
        [Route("FacturaClienteID/{id}")]
        public HttpResponseMessage GetFacturaClienteID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Cliente2 where IdClientes =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Insert a Factura 
        public string Post(Factura factura)
        {
            try
            {


                DataTable table = new DataTable();
                //Las variables de fecha, son igualadas a un valor Datatime
                DateTime time = factura.FechaDeExpedicion;
                DateTime time2 = factura.FechaVencimiento;
                DateTime time3 = factura.FechaDeEntrega;
                //Al momento de insertar los valores de las fechas, estan seran insertadas con el formato 'Format'
                string format = "yyyy-MM-dd HH:mm:ss";
                //De esta manera no causara error al tratar de insertar fechas en la base de datos SQL
                //time.ToString(format)
                string query = @"
                                Execute itInsertNuevaFactura " + factura.IdCliente + " , '"
                                + factura.Serie + "' , '" + factura.Folio + "' , '" 
                                + factura.Tipo + "' , '" + time.ToLocalTime().ToString(format) + "' , '" 
                                + factura.LugarDeExpedicion + "' , '" + factura.Certificado + "' , '" 
                                + factura.NumeroDeCertificado + "' , '" + factura.UUID + "' , '" 
                                + factura.UsoDelCFDI + "' , '"+ factura.Subtotal + "' , '" + factura.SubtotalDlls + "' , '" 
                                + factura.Descuento + "' , '" + factura.ImpuestosRetenidos + "' , '" 
                                + factura.ImpuestosTrasladados + "' , '" + factura.ImpuestosTrasladadosDlls + "' , '"  + factura.Total + "' , '" + factura.TotalDlls + "' , '" 
                                + factura.FormaDePago + "' , '" + factura.MetodoDePago + "' , '" 
                                + factura.Cuenta + "' , '" + factura.Moneda + "' , '" 
                                + factura.CadenaOriginal + "' , '" + factura.SelloDigitalSAT + "' , '" 
                                + factura.SelloDigitalCFDI + "' , '" + factura.NumeroDeSelloSAT + "' , '" 
                                + factura.RFCdelPAC + "' , '" + factura.Observaciones + "' , '" 
                                + time2.ToLocalTime().ToString(format) + "' , '" + factura.OrdenDeCompra + "' , '"  
                                + factura.TipoDeCambio + "' , '" + time3.ToLocalTime().ToString(format) + "' , '" 
                                + factura.CondicionesDePago + "' , '" + factura.Vendedor + "' , '" 
                                + factura.Estatus + "' , '" + factura.Ver + "' , '" + factura.Usuario + "'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Factura Agregada";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }
       
        //Editar Factura
        public string Put(Factura factura)
        {
            try
            {


                DataTable table = new DataTable();
                //Las variables de fecha, son igualadas a un valor Datatime
                DateTime time = factura.FechaDeExpedicion;
                DateTime time2 = factura.FechaVencimiento;
                DateTime time3 = factura.FechaDeEntrega;
                //Al momento de insertar los valores de las fechas, estan seran insertadas con el formato 'Format'
                string format = "yyyy-MM-dd HH:mm:ss";
                //De esta manera no causara error al tratar de insertar fechas en la base de datos SQL
                //time.ToString(format)

                string query = @"
                                Execute etEditarFactura " + factura.Id + " , " + factura.IdCliente + " , '"
                                + factura.Serie + "' , '" + factura.Folio + "' , '"
                                + factura.Tipo + "' , '" + time.ToLocalTime().ToString(format) + "' , '"
                                + factura.LugarDeExpedicion + "' , '" + factura.Certificado + "' , '"
                                + factura.NumeroDeCertificado + "' , '" + factura.UUID + "' , '"
                                + factura.UsoDelCFDI + "' , '" + factura.Subtotal + "' , '" + factura.SubtotalDlls + "' , '" 
                                + factura.Descuento + "' , '" + factura.ImpuestosRetenidos + "' , '"
                                + factura.ImpuestosTrasladados + "' , '" + factura.ImpuestosTrasladadosDlls + "' , '" + factura.Total + "' , '" + factura.TotalDlls + "' , '" 
                                + factura.FormaDePago + "' , '" + factura.MetodoDePago + "' , '"
                                + factura.Cuenta + "' , '" + factura.Moneda + "' , '"
                                + factura.CadenaOriginal + "' , '" + factura.SelloDigitalSAT + "' , '"
                                + factura.SelloDigitalCFDI + "' , '" + factura.NumeroDeSelloSAT + "' , '"
                                + factura.RFCdelPAC + "' , '" + factura.Observaciones + "' , '"
                                + time2.ToLocalTime().ToString(format) + "' , '" + factura.OrdenDeCompra + "' , '"
                                + factura.TipoDeCambio + "' , '" + time3.ToLocalTime().ToString(format) + "' , '"
                                + factura.CondicionesDePago + "' , '" + factura.Vendedor + "' , '"
                                + factura.Estatus + "' , '" + factura.Ver + "' , '" + factura.Usuario + @"'
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Actualizacion Exitosa";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;







            }
        }
        [Route("Cancelar/{id}")]
        public string PutCancel(int id)
        {
            try
            {


                DataTable table = new DataTable();
                //Las variables de fecha, son igualadas a un valor Datatime
               
                //Al momento de insertar los valores de las fechas, estan seran insertadas con el formato 'Format'
               
                //De esta manera no causara error al tratar de insertar fechas en la base de datos SQL
                //time.ToString(format)

                string query = @"update Factura set Estatus='Cancelada' where Id="+id;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Cancelacion Exitosa";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;







            }
        }

        //Insert DetalleFactura
        [Route("InsertDetalleFactura")]
        public string PostDetalleFactura(DetalleFactura factura)
        {
            try
            {


                DataTable table = new DataTable();
               // string query = @"
                //                Execute itInsertNuevaDetalleFacturaId " + factura.IdFactura + " , '"
                  //              + factura.ClaveProducto + "' , '" + factura.Producto + "' , '" + factura.Unidad + "' , '" 
                    //            + factura.ClaveSat + "' , '" + factura.PrecioUnitario + "' , '" + factura.PrecioUnitarioDlls + "' , '" 
                      //          + factura.Cantidad + "' , '" + factura.Importe + "' , '" + factura.ImporteDlls + "' , '" 
                        //        + factura.Observaciones + "' , '" + factura.TextoExtra + "' , '"
                          //      + factura.ImporteIVA + "' , '" + factura.ImporteIVADlls + @"'
                            //    ";

                string query = @"insert into DetalleFactura values ("+ factura.IdFactura+",'"+ factura.ClaveProducto+ "','"+ factura.Producto+ "','"+ factura.Unidad+ "','"+ factura.ClaveSat+ "','"+ factura.PrecioUnitario+ "','"+ factura.Cantidad+ "','"+ factura.Importe+ "','"+ factura.Observaciones+ "','"+ factura.TextoExtra+ "','"+ factura.PrecioUnitarioDlls+ "','"+ factura.ImporteDlls+ "','"+ factura.ImporteIVA+ "','"+ factura.ImporteIVADlls+ "');";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Producto Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }
        //Editar Factura
        [Route("UpdateDetalleFactura")]
        public string PutDetalleFactura(DetalleFactura detalleFactura)
        {
            try
            {


                DataTable table = new DataTable();
                string query = @"
                                Execute etEditarDetalleFactura " + detalleFactura.IdDetalle + " , '"
                                + detalleFactura.ClaveProducto + "' , '" + detalleFactura.Producto + "' , '" + detalleFactura.Unidad + "' , '"
                                + detalleFactura.ClaveSat + "' , '" + detalleFactura.PrecioUnitario + "' , '" + detalleFactura.PrecioUnitarioDlls + "' , '" 
                                + detalleFactura.Cantidad + "' , '" + detalleFactura.Importe + "' , '"  + detalleFactura.ImporteDlls + "' , '" 
                                + detalleFactura.Observaciones + "' , '" + detalleFactura.TextoExtra + "' , '"
                                + detalleFactura.ImporteIVA + "' , '" + detalleFactura.ImporteIVADlls + @"'
                                ";

                query = @"update DetalleFactura set ClaveProducto = '" + detalleFactura.ClaveProducto + "', Producto = '" + detalleFactura.Producto + "', Unidad = '" + detalleFactura.Unidad + "', ClaveSAT = '" + detalleFactura.ClaveSat + "', PrecioUnitario = '" + detalleFactura.PrecioUnitario + "', PrecioUnitarioDlls = '" + detalleFactura.PrecioUnitarioDlls + "', Cantidad = '" + detalleFactura.Cantidad + "', Importe = '" + detalleFactura.Importe + "', ImporteDlls = '" + detalleFactura.ImporteDlls + "', Observaciones = '" + detalleFactura.Observaciones + "', TextoExtra = '" + detalleFactura.TextoExtra + "',ImporteIVA = '" + detalleFactura.ImporteIVA + "' where IdDetalle=" + detalleFactura.IdDetalle + ";";


                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))

                


                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Actualizacion Existosa";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;

            }
        }
        //Borrar Factura incluyendo detalles de factura
        public string Delete(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                              exec dtBorrarFactura " + id;

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
        //Delete Detalle Factura en  especifico
        [Route("DeleteDetalleFactura/{id}")]
        public string DeleteDetalleFactura(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                              exec dtBorrarDetalleFactura " + id;

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

        [Route("DeleteFacturaCreada")]
        public string DeleteFacturaCreada()
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"delete from factura where Estatus='Creada';";

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


        [Route("Pagada/{UUID}")]
        public string PutEstatusDetalle(string UUID)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @" exec etEditarEstatusFacturaPagada '" + UUID + "'";

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

        [Route("Reporte/{id}")]
        public HttpResponseMessage GetReporte(int id)
        {
            DataTable table = new DataTable();

            //string query = @"select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio, TotalDlls from Factura where IdCliente=" + id + " union all select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio, TotalDlls from NotaCredito where IdCliente= " + id +" order by FechaDeExpedicion desc ";
            string query = @"select Idcliente, Folio, Tipo, FechaDeExpedicion, FechaVencimiento, Total, Moneda, TipoDeCambio, TotalDlls from Factura where IdCliente=" + id + " and Estatus ='Timbrada'  order by FechaDeExpedicion asc ";
            query = @"select Factura.Idcliente, Factura.Folio, Factura.Tipo, Factura.FechaDeExpedicion, Factura.FechaVencimiento, Factura.Total, Factura.Moneda, Factura.TipoDeCambio, Factura.TotalDlls, sum(convert(float,PagoCFDI.cantidad)) as pagos, sum(convert(float,NotaCredito.Total)) as NCTotal,sum(convert(float,NotaCredito.TotalDlls)) as NCTotalDlls from Factura left join PagoCFDI on Factura.Id=PagoCFDI.IdFactura left join NotaCredito on NotaCredito.IdFactura=Factura.Id where Factura.IdCliente=" + id + " and Factura.Estatus ='Timbrada' group by Factura.Idcliente, Factura.Folio, Factura.Tipo, Factura.FechaDeExpedicion, Factura.FechaVencimiento, Factura.Total, Factura.Moneda, Factura.TipoDeCambio, Factura.TotalDlls order by Factura.FechaDeExpedicion ";
            // string query = @"select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio, TotalDlls from Factura where IdCliente=" + id + " union all select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio, TotalDlls from NotaCredito where IdCliente= " + id + " and Estatus ='Timbrada'  order by FechaDeExpedicion desc ";

            // string query = @"select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio from Factura where IdCliente= " + id + " union all select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio from NotaCredito where IdCliente= " + id + " order by FechaDeExpedicion desc ";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("ReporteU/{id}")]
        public HttpResponseMessage GetReporteM(int id)
        {
            DataTable table = new DataTable();


            string query = @"select Idcliente, Folio, Tipo, FechaDeExpedicion, FechaVencimiento, Total, Moneda, TipoDeCambio, TotalDlls from Factura where IdCliente=" + id + " and Moneda = 'USD' and Estatus ='Timbrada' order by FechaDeExpedicion asc ";
            query = @"select Factura.Idcliente, Factura.Folio, Factura.Tipo, Factura.FechaDeExpedicion, Factura.FechaVencimiento, Factura.Total, Factura.Moneda, Factura.TipoDeCambio, Factura.TotalDlls, sum(convert(float,PagoCFDI.cantidad)) as pagos, sum(convert(float,NotaCredito.Total)) as NCTotal,sum(convert(float,NotaCredito.TotalDlls)) as NCTotalDlls from Factura left join PagoCFDI on Factura.Id=PagoCFDI.IdFactura left join NotaCredito on NotaCredito.IdFactura=Factura.Id where Factura.IdCliente=" + id + " and Factura.Moneda = 'USD' and Factura.Estatus ='Timbrada' group by Factura.Idcliente, Factura.Folio, Factura.Tipo, Factura.FechaDeExpedicion, Factura.FechaVencimiento, Factura.Total, Factura.Moneda, Factura.TipoDeCambio, Factura.TotalDlls order by Factura.FechaDeExpedicion ";
            // string query = @"select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio, TotalDlls from Factura where IdCliente=" + id + " and Moneda = 'USD' union all select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio, TotalDlls from NotaCredito where IdCliente= " + id + " and Moneda = 'USD' order by FechaDeExpedicion desc ";

            // string query = @"select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio, TotalDlls from Factura where IdCliente=" + id + " and Moneda = 'USD' and Estatus ='Timbrada' order by FechaDeExpedicion desc ";
            // string query = @"select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio, TotalDlls from Factura where IdCliente=" + id + " and Moneda = 'USD' union all select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio, TotalDlls from NotaCredito where IdCliente= " + id + " and Moneda = 'USD' order by FechaDeExpedicion desc ";




            //string query = @"select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio, TotalDlls from Factura where IdCliente=" + id + " and Moneda = 'USD' and Estatus ='Timbrada'  order by FechaDeExpedicion desc ";
            // string query = @"select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio from Factura where IdCliente= " + id + " union all select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio from NotaCredito where IdCliente= " + id + " order by FechaDeExpedicion desc ";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("ReporteM/{id}")]
        public HttpResponseMessage GetReporteU(int id)
        {
            DataTable table = new DataTable();

            string query = @"select Idcliente, Folio, Tipo, FechaDeExpedicion, FechaVencimiento, Total, Moneda, TipoDeCambio, TotalDlls from Factura where IdCliente=" + id + " and Moneda = 'MXN' and Estatus ='Timbrada'  order by FechaDeExpedicion asc ";
            query = @"select Factura.Idcliente, Factura.Folio, Factura.Tipo, Factura.FechaDeExpedicion, Factura.FechaVencimiento, Factura.Total, Factura.Moneda, Factura.TipoDeCambio, Factura.TotalDlls, sum(convert(float,PagoCFDI.cantidad)) as pagos, sum(convert(float,NotaCredito.Total)) as NCTotal,sum(convert(float,NotaCredito.TotalDlls)) as NCTotalDlls from Factura left join PagoCFDI on Factura.Id=PagoCFDI.IdFactura left join NotaCredito on NotaCredito.IdFactura=Factura.Id where Factura.IdCliente=" + id + " and Factura.Moneda = 'MXN' and Factura.Estatus ='Timbrada' group by Factura.Idcliente, Factura.Folio, Factura.Tipo, Factura.FechaDeExpedicion, Factura.FechaVencimiento, Factura.Total, Factura.Moneda, Factura.TipoDeCambio, Factura.TotalDlls order by Factura.FechaDeExpedicion ";
            // string query = @"select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio from Factura where IdCliente= " + id + " union all select Idcliente, Folio, Tipo, FechaDeExpedicion, Total, Moneda, TipoDeCambio from NotaCredito where IdCliente= " + id + " order by FechaDeExpedicion desc ";


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
