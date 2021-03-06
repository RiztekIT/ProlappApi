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
    [RoutePrefix("api/OrdenTemporal")]
    public class OrdenTemporalController : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from dbo.OrdenTemporal";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener orden Temporal por OrdenCargaID, LOTE Y CLAVE PRODUCTO
        [Route("OrdenTemporal/{id}/{lote}/{clave}")]
        public HttpResponseMessage GetOrdenTemporal(int id, string lote, string clave)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenTemporal where IdOrdenCarga  =" + id + " and Lote = '" + lote + "' and ClaveProducto = '" + clave + "';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("tracking/{fechaini}/{fechafinal}")]
        public HttpResponseMessage GetTracking(string fechaini, string fechafinal)
        {
            DataTable table = new DataTable();

            string query = @"select Pedidos.*,OrdenCarga.* from Pedidos left join OrdenCarga on OrdenCarga.IdPedido=Pedidos.IdPedido where FechaExpedicion between '" + fechaini + "' and '" + fechafinal + "'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("BorrarOrdenTemporal/{id}")]
        public string Delete(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" exec dtBorrarOrdenTemporal " + id;

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


        public string Post(OrdenTemporal ot)
        {
            try
            {

                DataTable table = new DataTable();

                DateTime time = ot.FechaCaducidad;
                DateTime time2 = ot.FechaMFG;
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                insert into OrdenTemporal (IdDetalleTarima, IdOrdenCarga, IdOrdenDescarga, QR, NumeroFactura, NumeroEntrada, ClaveProducto, Lote, Sacos, Producto, PesoTotal, FechaCaducidad, FechaMFG, Comentarios, CampoExtra1, CampoExtra2, CampoExtra3)
                                    VALUES ("+ot.IdDetalleTarima+", "+ot.IdOrdenCarga+", "+ot.IdOrdenDescarga+", '"+ot.QR+"', '"+ot.NumeroFactura+"','"+ot.NumeroEntrada+"','"+ot.ClaveProducto+"', '"+ot.Lote+"', '"+ot.Sacos+"', '"+ot.Producto+"', '"+ot.PesoTotal+"', '"+time.ToString(format)+"', '"+ time2.ToString(format)+"','"+ot.Comentarios + "','"+ ot.CampoExtra1 + "','"+ot.CampoExtra2+"','"+ot.CampoExtra3 + @"')";

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

        public string Put(OrdenTemporal ot)
        {
            try
            {

                DataTable table = new DataTable();
                DateTime time = ot.FechaCaducidad;
                DateTime time2 = ot.FechaMFG;
                string format = "yyyy-MM-dd HH:mm:ss";
                string query = @"
                            update OrdenTemporal SET IdOrdenCarga = "+ot.IdOrdenCarga+", IdOrdenDescarga = "+ot.IdOrdenDescarga+", QR = '"+ot.QR+"', NumeroFactura = '"+ot.NumeroFactura+"', NumeroEntrada = '"+ot.NumeroEntrada +"', ClaveProducto = '"+ot.ClaveProducto+"', Lote = '"+ot.Lote+"', Sacos = '"+ot.Sacos+"', Producto = '"+ot.Producto+
                            "', PesoTotal = '"+ot.PesoTotal+"', FechaCaducidad = '"+time.ToString(format)+"', FechaMFG = '"+time2.ToString(format)+"', Comentarios = '"+ot.Comentarios+"', CampoExtra1 ='"+ot.CampoExtra1+ "', CampoExtra2 ='"+ot.CampoExtra2+ "', CampoExtra3 = '"+ot.CampoExtra3 +"' where IdOrdenTemporal = " + ot.IdOrdenTemporal +@";";

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

        //Obtener Orden Temporal por OrdenCargaID
        [Route("OrdenTemporalID/{id}")]
        public HttpResponseMessage GetOrdenTemporalID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenTemporal where IdOrdenCarga  =" + id + ";";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Orden Temporal por OrdenDescargaID
        [Route("OrdenTemporalIDOD/{id}")]
        public HttpResponseMessage GetOrdenTemporalIDOD(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenTemporal where IdOrdenDescarga  =" + id + ";";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //Obtener Orden Temporal por IDOrdencarga y codigo QR
        [Route("OrdenTemporalIdqr/{id}/{qr}")]
        public HttpResponseMessage GetOrdenTemporalIdqr(int id, string qr)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenTemporal where IdOrdenCarga  =" + id + " and QR ='" + qr + "';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("OrdenTemporalIdqrOD/{id}/{qr}")]
        public HttpResponseMessage GetOrdenTemporalIdqrOD(int id, string qr)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenTemporal where IdOrdenDescarga  =" + id + " and QR ='" + qr + "';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //Obtener Orden Temporal por ID tarima
        [Route("OrdenTemporalIdTarima/{id}")]
        public HttpResponseMessage GetOrdenTemporalIdTarima(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenTemporal where IdTarima  =" + id + ";";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("OrdenTemporalIdTarimaOC/{id}/{oc}")]
        public HttpResponseMessage GetOrdenTemporalIdTarimaOC(int id, int oc)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenTemporal where IdTarima  =" + id + " and IdOrdenCarga ="+oc+";";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("trackingCliente/{fechaini}/{fechafinal}/{idcliente}")]
        public HttpResponseMessage GetTrackingCliente(string fechaini, string fechafinal, int idcliente)
        {
            DataTable table = new DataTable();

            string query = @"select Pedidos.*,OrdenCarga.* from Pedidos left join OrdenCarga on OrdenCarga.IdPedido=Pedidos.IdPedido where FechaExpedicion between '" + fechaini + "' and '" + fechafinal + "' and pedidos.idcliente = '" + idcliente + "';";

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
