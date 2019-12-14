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
    public class FacturaController : ApiController
    {



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
                                Execute itInsertNuevoClientes " + factura.IdCliente + " , '"
                                + factura.Serie + "' , '" + factura.Folio + "' , '" 
                                + factura.Tipo + "' , '" + time.ToString(format) + "' , '" 
                                + factura.LugarDeExpedicion + "' , '" + factura.Certificado + "' , '" 
                                + factura.NumeroDeCertificado + "' , '" + factura.UUID + "' , '" 
                                + factura.UsoDelCFDI + "' , '"+ factura.Subtotal + "' , '" 
                                + factura.Descuento + "' , '" + factura.ImpuestosRetenidos + "' , '" 
                                + factura.ImpuestosTrasladados + "' , '" + factura.Total + "' , '" 
                                + factura.FormaDePago + "' , '" + factura.MetodoDePago + "' , '" 
                                + factura.Cuenta + "' , '" + factura.Moneda + "' , '" 
                                + factura.CadenaOriginal + "' , '" + factura.SelloDigitalSAT + "' , '" 
                                + factura.SelloDigitalCFDI + "' , '" + factura.NumeroDeSelloSAT + "' , '" 
                                + factura.RFCdelPAC + "' , '" + factura.Observaciones + "' , '" 
                                + time2.ToString(format) + "' , '" + factura.OrdenDeCompra + "' , '"  
                                + factura.TipoDeCambio + "' , '" + time3.ToString(format) + "' , '" 
                                + factura.CondicionesDePago + "' , '" + factura.Vendedor + "' , '" 
                                + factura.Estatus + "' , '" + factura.Ver + @"'
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Added Successfully";
            }
            catch (Exception exe)
            {
                return "Failed to Add" + exe;
            }
        }
        //hola ricardo
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



                return "Deleted Successfully";
            }
            catch (Exception ex)
            {
                return "Failed to Delete" + ex;
            }
        }

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
                                Execute itInsertNuevoClientes " + factura.Id + " , " + factura.IdCliente + " , '"
                                + factura.Serie + "' , '" + factura.Folio + "' , '"
                                + factura.Tipo + "' , '" + time.ToString(format) + "' , '"
                                + factura.LugarDeExpedicion + "' , '" + factura.Certificado + "' , '"
                                + factura.NumeroDeCertificado + "' , '" + factura.UUID + "' , '"
                                + factura.UsoDelCFDI + "' , '" + factura.Subtotal + "' , '"
                                + factura.Descuento + "' , '" + factura.ImpuestosRetenidos + "' , '"
                                + factura.ImpuestosTrasladados + "' , '" + factura.Total + "' , '"
                                + factura.FormaDePago + "' , '" + factura.MetodoDePago + "' , '"
                                + factura.Cuenta + "' , '" + factura.Moneda + "' , '"
                                + factura.CadenaOriginal + "' , '" + factura.SelloDigitalSAT + "' , '"
                                + factura.SelloDigitalCFDI + "' , '" + factura.NumeroDeSelloSAT + "' , '"
                                + factura.RFCdelPAC + "' , '" + factura.Observaciones + "' , '"
                                + time2.ToString(format) + "' , '" + factura.OrdenDeCompra + "' , '"
                                + factura.TipoDeCambio + "' , '" + time3.ToString(format) + "' , '"
                                + factura.CondicionesDePago + "' , '" + factura.Vendedor + "' , '"
                                + factura.Estatus + "' , '" + factura.Ver + @"'
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Updated Successfully";
            }
            catch (Exception exe)
            {
                return "Failed to Update" + exe;






            }
        }






    }
}
