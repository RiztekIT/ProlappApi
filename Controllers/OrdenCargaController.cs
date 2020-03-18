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
    [RoutePrefix("api/OrdenCarga")]
    public class OrdenCargaController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from dbo.OrdenCarga";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("DetalleOrdenCarga/{id}")]
        public HttpResponseMessage GetDetalleOrdenCargaId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleOrdenCarga where IdOrdenCarga  =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //Obtener MASTER JOIN
        [Route("MasterID/{id}")]
        public HttpResponseMessage GetMasterID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleOrdenCarga 
                                left join Tarima on DetalleOrdenCarga.IdTarima=Tarima.IdTarima 
                                    left join DetalleTarima on DetalleTarima.IdTarima=Tarima.IdTarima
                                           where DetalleOrdenCarga.IdOrdenCarga =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("BorrarOrdenCarga/{id}")]
        public string Delete(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" exec dtBorrarOrdenCarga " + id;

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
        [Route("BorrarDetalleOrdenCarga/{id}")]
        public string DeleteDetalleOrdenCarga(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" exec dtBorrarDetalleOrdenCarga " + id;

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
        public string Post(OrdenCarga ordencarga)
        {
            try
            {

                DataTable table = new DataTable();


                DateTime time = ordencarga.FechaEnvio;
                DateTime time2 = ordencarga.FechaInicioCarga;
                DateTime time3 = ordencarga.FechaFinalCarga;
                DateTime time4 = ordencarga.FechaExpedicion;

                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec itInsertarNuevaOrdenCarga " + ordencarga.Folio + " , '" + time.ToString(format) + "' , " +
                                ordencarga.IdCliente + " , '" + ordencarga.Cliente + "', " + ordencarga.IdPedido + " , '" + ordencarga.Fletera + "' , '" +
                                ordencarga.Caja + "' , '" + ordencarga.Sacos + "' , '" + ordencarga.Kg + "' , '" + ordencarga.Chofer + "' , '" + ordencarga.Origen +
                                "' , '" + ordencarga.Destino + "' , '" + ordencarga.Observaciones + "' , '" + ordencarga.Estatus + "' , '" + time2.ToString(format) + "' , '" + 
                                time3.ToString(format) + "' , '" + time4.ToString(format) + "' , " + ordencarga.IdUsuario + " , '" + ordencarga.Usuario +  @"'";

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

        [Route("AddDetalleOrdenCarga")]
        public string PostDetalleOrdenCarga(DetalleOrdenCarga doc)
        {
            try
            {
               DataTable table = new DataTable();

                string query = @"
                                exec itInsertNuevoDetalleOrdenCarga " + doc.IdOrdenCarga + " , " + doc.IdTarima + @"";

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

        public string Put(OrdenCarga ordencarga)
        {
            try
            {


                DataTable table = new DataTable();

                DateTime time = ordencarga.FechaEnvio;
                DateTime time2 = ordencarga.FechaInicioCarga;
                DateTime time3 = ordencarga.FechaFinalCarga;
                DateTime time4 = ordencarga.FechaExpedicion;

                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec etEditarOrdenCarga " + ordencarga.Folio + " , '" + time.ToString(format) + "' , " +
                                ordencarga.IdCliente + " , '" + ordencarga.Cliente + "', " + ordencarga.IdPedido + " , '" + ordencarga.Fletera + "' , '" +
                                ordencarga.Caja + "' , '" + ordencarga.Sacos + "' , '" + ordencarga.Kg + "' , '" + ordencarga.Chofer + "' , '" + ordencarga.Origen +
                                "' , '" + ordencarga.Destino + "' , '" + ordencarga.Observaciones + "' , '" + ordencarga.Estatus + "' , '" + time2.ToString(format) + "' , '" +
                                time3.ToString(format) + "' , '" + time4.ToString(format) + "' , " + ordencarga.IdUsuario + " , '" + ordencarga.Usuario + @"'";

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
        [Route("UpdateDetalleOrdenCarga")]
        public string PutDetalleOrdenCarga(DetalleOrdenCarga doc)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                                exec etEditarDetalleOrdenCarga " + doc.IdDetalleOrdenCarga + " , " + doc.IdOrdenCarga + " , " + doc.IdTarima + @"";

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



        [Route("EstatusDetalle")]
        public string PutEstatusDetalle(OrdenCarga ordencarga)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @" exec etEditarEstatusDetalleCarga" + ordencarga.IdOrdenCarga + " , '" + ordencarga.Estatus  +"'";

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



    }
}
