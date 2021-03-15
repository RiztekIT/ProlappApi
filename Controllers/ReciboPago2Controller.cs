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
    [RoutePrefix("api/ReciboPago2")]
    public class ReciboPago2Controller : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from ReciboPago2";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ReciboPagoCliente")]
        public HttpResponseMessage GetReciboPagoCliente()
        {
            DataTable table = new DataTable();

            string query = @"Select ReciboPago2.*, Cliente.* from ReciboPago2 LEFT JOIN Cliente ON ReciboPago2.IdCliente = Cliente.IdClientes order by FechaExpedicion DESC";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Select Id Ultimo Pago Recibo
        [Route("UltimoReciboPago")]
        public HttpResponseMessage GetUltimoReciboPago()
        {
            DataTable table = new DataTable();


            string query = @"select IDENT_CURRENT( 'ReciboPago2' ) as Id from ReciboPago2";
            //string query = @"select MAX(ReciboPago.Id) + 1 as Id from ReciboPago";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Select ReciboPago por ID
        [Route("ReciboPagoId/{id}")]
        public HttpResponseMessage GetReciboPagoId(int id)
        {
            DataTable table = new DataTable();


            string query = @"Select * from ReciboPago2 where Id =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Select Todas las facturas de IdCliente
        [Route("FacturaIdCliente/{id}")]
        public HttpResponseMessage GetFacturaIdCliente(int id)
        {
            DataTable table = new DataTable();


            string query = @"Select * from Factura2 where Factura2.IdCliente =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Select de Facturas dependiendo Cliente, estatus timbrada, El saldo del PagoCFDI es mayor a 0
        [Route("FacturaPagoCFDI/{id}/{folio}")]
        public HttpResponseMessage GetFacturaPagoCFDI(int id, int folio)
        {
            DataTable table = new DataTable();


            string query = @" Select Factura2.Folio, SUM(CONVERT(float, PagoCFDI2.Cantidad)) as Quantity, (CONVERT(float, Factura2.Total) - SUM(CONVERT(float, PagoCFDI2.Cantidad))) as Saldo, Factura2.Total, Factura2.Id, MAX(PagoCFDI2.NoParcialidad) as NoParcialidad
                                from PagoCFDI2 left join Factura2 ON Factura2.Id = PagoCFDI2.IdFactura
                                where PagoCFDI2.IdFactura IN ( select Factura2.Id from Factura2 
                                where Factura2.Estatus = 'Timbrada' and Factura2.IdCliente = " + id + @")
                                and Factura2.Folio=" + folio + @"
                                group by PagoCFDI2.IdFactura, Factura2.Total, Factura2.Folio, Factura2.Id
                                Having ((CONVERT(float, Factura2.Total)) - (  SUM(CONVERT(float, PagoCFDI2.Cantidad))  )) >=0";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("FacturaPagoCFDIDlls/{id}/{folio}")]
        public HttpResponseMessage GetFacturaPagoCFDIDlls(int id, int folio)
        {
            DataTable table = new DataTable();


            string query = @" Select Factura2.Folio, SUM(CONVERT(float, PagoCFDI2.Cantidad)) as Quantity, (CONVERT(float, Factura2.TotalDlls) - SUM(CONVERT(float, PagoCFDI2.Cantidad))) as Saldo, Factura2.TotalDlls as Total, Factura2.Id, MAX(PagoCFDI2.NoParcialidad) as NoParcialidad
                                from PagoCFDI2 left join Factura2 ON Factura2.Id = PagoCFDI2.IdFactura
                                where PagoCFDI2.IdFactura IN ( select Factura2.Id from Factura2 
                                where Factura2.Estatus = 'Timbrada' and Factura2.IdCliente = " + id + @")
                                and Factura2.Folio=" + folio + @"
                                group by PagoCFDI2.IdFactura, Factura2.TotalDlls, Factura2.Folio, Factura2.Id
                                Having ((CONVERT(float, Factura2.TotalDlls)) - (  SUM(CONVERT(float, PagoCFDI2.Cantidad))  )) >=0";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Select de las facturas dependiendo el cliente y estatus timbrada (Se ejecutara cuando no haya ningun PagoCFDI correspondiente a ese recibo de pago)
        [Route("FacturaPrimerPagoCFDI/{id}")]
        public HttpResponseMessage GetFacturaPrimerPagoCFDI(int id)
        {
            DataTable table = new DataTable();


            string query = @" Select * from Factura2
                             where Factura2.IdCliente = " + id + @"
                             and Factura2.Estatus = 'Timbrada' ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //Obtener el Ultimo NoParcialidad y sumarle 1
        [Route("UltimoNoParcialidad/{id}")]
        public HttpResponseMessage GetUltimoNoParcialidad(int id)
        {
            DataTable table = new DataTable();


            string query = @"  Select MAX((PagoCFDI2.NoParcialidad)) + 1 as NoParcialidad from PagoCFDI2 where PagoCFDI2.IdFactura = " + id + "";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Select de PagosCFDI
        [Route("ReciboPagoCFDI/{id}")]
        public HttpResponseMessage GetReciboPagoCFDI(int id)
        {
            DataTable table = new DataTable();


            string query = @"  select PagoCFDI2.*,Factura2.* from PagoCFDI2 left join Factura2 on PagoCFDI2.IdFactura=Factura2.Id where PagoCFDI2.IdReciboPago = " + id + " order by PagoCFDI2.Id ASC";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        public string Post(ReciboPago ReciboPago)
        {
            try
            {

                //Las variables de fecha, son igualadas a un valor Datatime
                DateTime time = ReciboPago.FechaExpedicion;
                DateTime time2 = ReciboPago.FechaPago;
                //Al momento de insertar los valores de las fechas, estan seran insertadas con el formato 'Format'
                string format = "yyyy-MM-dd HH:mm:ss";
                //De esta manera no causara error al tratar de insertar fechas en la base de datos SQL


                DataTable table = new DataTable();
                string query = @"
                                 Execute itInsertNuevoReciboPago2 " + ReciboPago.IdCliente + " , '"
                                + time.ToString(format) + "' , '" + time2.ToString(format) + "' , '"
                                + ReciboPago.FormaPago + "' , '" + ReciboPago.Moneda + "' , '"
                                + ReciboPago.TipoCambio + "' , '" + ReciboPago.Cantidad + "' , '"
                                + ReciboPago.Referencia + "' , '" + ReciboPago.UUID + "' , '"
                                + ReciboPago.Tipo + "' , '" + ReciboPago.Certificado + "' , '" + ReciboPago.NoCertificado + "' , '"
                                + ReciboPago.Cuenta + "' , '" + ReciboPago.CadenaOriginal + "' , '"
                                + ReciboPago.SelloDigitalSAT + "' , '" + ReciboPago.SelloDigitalCFDI + "' , '" + ReciboPago.NoSelloSAT + "' , '"
                                + ReciboPago.RFCPAC + "' , '" + ReciboPago.Estatus + "','" + ReciboPago.folio + "'";
                                

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Recibo de Pago Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }

        //put
        public string Put(ReciboPago ReciboPago)
        {
            try
            {

                //Las variables de fecha, son igualadas a un valor Datatime
                DateTime time = ReciboPago.FechaExpedicion;
                DateTime time2 = ReciboPago.FechaPago;
                //Al momento de insertar los valores de las fechas, estan seran insertadas con el formato 'Format'
                string format = "yyyy-MM-dd HH:mm:ss";
                //De esta manera no causara error al tratar de insertar fechas en la base de datos SQL


                DataTable table = new DataTable();
                string query = @"
                                 Execute etEditarReciboPago2 " + ReciboPago.Id + " , " + ReciboPago.IdCliente + " , '"
                                + time.ToString(format) + "' , '" + time2.ToString(format) + "' , '"
                                + ReciboPago.FormaPago + "' , '" + ReciboPago.Moneda + "' , '"
                                + ReciboPago.TipoCambio + "' , '" + ReciboPago.Cantidad + "' , '"
                                + ReciboPago.Referencia + "' , '" + ReciboPago.UUID + "' , '"
                                + ReciboPago.Tipo + "' , '" + ReciboPago.Certificado + "' , '" + ReciboPago.NoCertificado + "' , '"
                                + ReciboPago.Cuenta + "' , '" + ReciboPago.CadenaOriginal + "' , '"
                                + ReciboPago.SelloDigitalSAT + "' , '" + ReciboPago.SelloDigitalCFDI + "' , '" + ReciboPago.NoSelloSAT + "' , '"
                                + ReciboPago.RFCPAC + "' , '" + ReciboPago.Estatus + "','" + ReciboPago.folio + "'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Recibo de Pago Actualizado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }



        public string Delete(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                              delete from ReciboPago2 where Id = " + id;

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

        [Route("PagoCFDI")]
        public HttpResponseMessage GetPagoCFDI()
        {
            DataTable table = new DataTable();

            string query = @"select * from PagoCFDI2";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Seleccionar el total de cierta Factura dependiendo  el id del PagoCFDI
        [Route("TotalFactura/{id}")]
        public HttpResponseMessage GetTotalFactura(int id)
        {
            DataTable table = new DataTable();

            string query = @"select Total from Factura2 where id =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener pagocfdi en base a idFactura
        [Route("PagoCFDIFacturaID/{id}")]
        public HttpResponseMessage GetPagoCFDIFacturaID(int id)
        {
            DataTable table = new DataTable();

            //string query = @"select * from PagoCFDI where IdFactura =" + id + "order by Id ASC";
            string query = @"select PagoCFDI2.*, ReciboPago2.* from PagoCFDI2 left join ReciboPago2 on PagoCFDI2.IdReciboPago=ReciboPago2.Id where PagoCFDI2.IdFactura =" + id + "order by ReciboPago2.Estatus ASC";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        //Join PagoCFDI con Facturas, donde el ID  de la factura sea el mismo y coincida con el IdRecibo
        [Route("PagoCFDIFactura/{id}")]
        public HttpResponseMessage GetPagoCFDIFactura(int id)
        {
            DataTable table = new DataTable();

            /* string query = @"exec jnPagoCFDIFactura" + id;*/
            string query = @"Select PagoCFDI2.*, Factura2.* from PagoCFDI2 LEFT JOIN Factura2 ON PagoCFDI2.IdFactura = Factura2.Id where IdReciboPago =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("PagoCFDI")]
        public string PostPagoCFDI(PagoCFDI pagoCFDI)
        {
            try
            {

                DataTable table = new DataTable();
                string query = @"
                                 Execute itInsertNuevoPagoCFDI2 " + pagoCFDI.IdReciboPago + " , "
                                + pagoCFDI.IdFactura + " , '" + pagoCFDI.UUID + "' , '"
                                + pagoCFDI.Cantidad + "' , '" + pagoCFDI.NoParcialidad + "' , '"
                                + pagoCFDI.Saldo + @"'
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "PagoCFDI Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }



        [Route("PagoCFDI")]
        public string PutPagoCFDI(PagoCFDI pagoCFDI)
        {
            try
            {

                DataTable table = new DataTable();
                string query = @"
                                 Execute etEditarPagoCFDI2 " + pagoCFDI.Id + " , " + pagoCFDI.IdReciboPago + " , "
                                + pagoCFDI.IdFactura + " , '" + pagoCFDI.UUID + "' , '"
                                + pagoCFDI.Cantidad + "' , '" + pagoCFDI.NoParcialidad + "' , '"
                                + pagoCFDI.Saldo + @"'
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "PagoCFDI Actualizado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }

        [Route("CancelarPagoCFDI")]
        public string PutCancelarPagoCFDI(int id)
        {
            try
            {

                DataTable table = new DataTable();
                string query = @"update PagoCFDI2 set saldo=convert(float,Cantidad)+convert(float,saldo), cantidad=0 where IdReciboPago=" + id + "";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "PagoCFDI Cancelado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }

        [Route("PagoCFDI/{Id}")]
        public string DeletePagoCFDI(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                              delete from PagoCFDI2 where Id = " + id;

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

        [Route("DeletePagoCreado")]
        public string DeletePagoCreado()
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"delete from ReciboPago2 where Estatus='Creada';";

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

    }
}
