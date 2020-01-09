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
    [RoutePrefix("api/ReciboPago")]
    public class ReciboPagoController : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from ReciboPago";

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
                                 Execute itInsertNuevoReciboPago " + ReciboPago.IdCliente + " , '"
                                + time.ToString(format) + "' , '" + time2.ToString(format) + "' , '"
                                + ReciboPago.FormaPago + "' , '" + ReciboPago.Moneda + "' , '"
                                + ReciboPago.TipoCambio + "' , '" + ReciboPago.Cantidad + "' , '"
                                + ReciboPago.Referencia + "' , '" + ReciboPago.UUID + "' , '"
                                + ReciboPago.Tipo + "' , '" + ReciboPago.Certificado + "' , '" + ReciboPago.NoCertificado + "' , '"
                                + ReciboPago.Cuenta + "' , '" + ReciboPago.CadenaOriginal + "' , '"
                                + ReciboPago.SelloDigitalSAT + "' , '" + ReciboPago.SelloDigitalCFDI + "' , '" + ReciboPago.NoSelloSAT + "' , '" 
                                + ReciboPago.RFCPAC + "' , '" + ReciboPago.Estatus + @"'
                                ";

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
                                 Execute etEditarReciboPago " + ReciboPago.Id + " , " + ReciboPago.IdCliente + " , '"
                                + time.ToString(format) + "' , '" + time2.ToString(format) + "' , '"
                                + ReciboPago.FormaPago + "' , '" + ReciboPago.Moneda + "' , '"
                                + ReciboPago.TipoCambio + "' , '" + ReciboPago.Cantidad + "' , '"
                                + ReciboPago.Referencia + "' , '" + ReciboPago.UUID + "' , '"
                                + ReciboPago.Tipo + "' , '" + ReciboPago.Certificado + "' , '" + ReciboPago.NoCertificado + "' , '"
                                + ReciboPago.Cuenta + "' , '" + ReciboPago.CadenaOriginal + "' , '"
                                + ReciboPago.SelloDigitalSAT + "' , '" + ReciboPago.SelloDigitalCFDI + "' , '" + ReciboPago.NoSelloSAT + "' , '"
                                + ReciboPago.RFCPAC + "' , '" + ReciboPago.Estatus + @"'
                                ";

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
                              exec dtBorrarReciboPago " + id;

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

            string query = @"select * from PagoCFDI";

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
                                 Execute itInsertNuevoPagoCFDI " + pagoCFDI.IdReciboPago + " , "
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
                                 Execute etEditarPagoCFDI " + pagoCFDI.Id + " , " + pagoCFDI.IdReciboPago + " , "
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

        [Route("PagoCFDI/{Id}")]
        public string DeletePagoCFDI(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                              exec dtBorrarPagoCFDI " + id;

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
