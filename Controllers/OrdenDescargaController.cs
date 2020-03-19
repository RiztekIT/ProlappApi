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
    [RoutePrefix("api/OrdenDescarga")]
    public class OrdenDescargaController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from dbo.OrdenDescarga";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetDetalleOrdenDescarga")]
        public HttpResponseMessage GetDetalleOrdenDescarga()
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleOrdenDescarga";

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

                string query = @" exec dtBorrarOrdenDescarga " + id;

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
        [Route("BorrarDetalleOrdenDescarga/{id}")]
        public string DeleteDetalleOrdenCarga(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" exec dtBorrarDetalleOrdenDescarga" + id;

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



        public string Post(OrdenDescarga ordenDescarga)
        {
            try
            {

                DataTable table = new DataTable();


                DateTime time = ordenDescarga.FechaLlegada;
                DateTime time2 = ordenDescarga.FechaInicioDescarga;
                DateTime time3 = ordenDescarga.FechaFinalDescarga;
                DateTime time4 = ordenDescarga.FechaExpedicion;

                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec itInsertNuevaOrdenDescarga " + ordenDescarga.Folio + " , '" + time.ToString(format) + "' , " +
                                ordenDescarga.IdProveedor + " , '" + ordenDescarga.Proveedor + "', " + ordenDescarga.PO + " , '" + ordenDescarga.Fletera + "' , '" +
                                ordenDescarga.Caja + "' , '" + ordenDescarga.Sacos + "' , '" + ordenDescarga.Kg + "' , '" + ordenDescarga.Chofer + "' , '" + ordenDescarga.Origen +
                                "' , '" + ordenDescarga.Destino + "' , '" + ordenDescarga.Observaciones + "' , '" + ordenDescarga.Estatus + "' , '" + time2.ToString(format) + "' , '" +
                                time3.ToString(format) + "' , '" + time4.ToString(format) + "' , " + ordenDescarga.IdUsuario + " , '" + ordenDescarga.Usuario + @"'";

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

        [Route("AddDetalleOrdenDescarga")]
        public string PostDetalleOrdenDescarga(DetalleOrdenDescarga dodc)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                                exec itInsertNuevoDetalleOrdenDescarga " + dodc.IdDetalleOrdenDescarga + " , " + dodc.IdTarima + @"";

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


        public string Put(OrdenDescarga ordenDescarga)
        {
            try
            {


                DataTable table = new DataTable();

                DateTime time = ordenDescarga.FechaLlegada;
                DateTime time2 = ordenDescarga.FechaInicioDescarga;
                DateTime time3 = ordenDescarga.FechaFinalDescarga;
                DateTime time4 = ordenDescarga.FechaExpedicion;

                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                               exec etEditarOrdenDescarga " + ordenDescarga.IdOrdenDescarga + " , " + ordenDescarga.Folio + " , '" + time.ToString(format) + "' , " +
                                ordenDescarga.IdProveedor + " , '" + ordenDescarga.Proveedor + "', " + ordenDescarga.PO + " , '" + ordenDescarga.Fletera + "' , '" +
                                ordenDescarga.Caja + "' , '" + ordenDescarga.Sacos + "' , '" + ordenDescarga.Kg + "' , '" + ordenDescarga.Chofer + "' , '" + ordenDescarga.Origen +
                                "' , '" + ordenDescarga.Destino + "' , '" + ordenDescarga.Observaciones + "' , '" + ordenDescarga.Estatus + "' , '" + time2.ToString(format) + "' , '" +
                                time3.ToString(format) + "' , '" + time4.ToString(format) + "' , " + ordenDescarga.IdUsuario + " , '" + ordenDescarga.Usuario + @"'";

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
        [Route("UpdateDetalleOrdenDescarga")]
        public string PutDetalleOrdenDescarga(DetalleOrdenDescarga doc)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                                exec etEditarDetalleOrdenDescarga " + doc.IdDetalleOrdenDescarga + " , " + doc.IdOrdenDescarga + " , " + doc.IdTarima + @"";

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