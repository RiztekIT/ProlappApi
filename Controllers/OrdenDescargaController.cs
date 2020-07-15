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

        [Route("GetOrdenDescargaID/{id}")]
        public HttpResponseMessage GetOrdenDescargaID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenDescarga where IdOrdenDescarga = "+ id;

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

        //Obtener detalles de OD por ID
        [Route("MasterID/{id}")]
        public HttpResponseMessage GetMasterID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleOrdenDescarga where IdOrdenDescarga =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //Obtener ID ultima OD
        [Route("GetUltimoIdOrdenDescarga")]
        public string GetUtimoIdCompra()
        {
            string IdOrdenDescarga;
            DataRow row;
            DataTable table = new DataTable();

            string query = @"select TOP 1 OrdenDescarga.IdOrdenDescarga from OrdenDescarga order by OrdenDescarga.IdOrdenDescarga desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
                row = table.Rows[0];
                IdOrdenDescarga = row["IdOrdenDescarga"].ToString();
            }

            return IdOrdenDescarga;
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
                                ordenDescarga.IdProveedor + " , '" + ordenDescarga.Proveedor + "', '" + ordenDescarga.PO + "' , '" + ordenDescarga.Fletera + "' , '" +
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

                return "Se Agrego Correctamente";
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

                DateTime time = dodc.FechaMFG;
                DateTime time2 = dodc.FechaCaducidad; 
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec itInsertNuevoDetalleOrdenDescarga " + dodc.IdDetalleOrdenDescarga + " , '" + dodc.ClaveProducto + "' , '" + dodc.Producto + "' , '" + dodc.Sacos +
                                "' , '" + dodc.PesoxSaco + "' , '" + dodc.Lote + "' , " + dodc.IdProveedor + " , '" + dodc.Proveedor + "' , '" + dodc.PO
                                + "' , '" + time.ToString(format) + "' , '" + time2.ToString(format) + "' , '" + dodc.Shipper + "' , '" + dodc.USDA + "' , '" + dodc.Pedimento +
                                "' , '" + dodc.Saldo + @"'";

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
        public string PutDetalleOrdenDescarga(DetalleOrdenDescarga dodc)
        {
            try
            {
                DataTable table = new DataTable();
                DateTime time = dodc.FechaMFG;
                DateTime time2 = dodc.FechaCaducidad;
                string format = "yyyy-MM-dd HH:mm:ss";


                string query = @"
                                exec etEditarDetalleOrdenDescarga " + dodc.IdDetalleOrdenDescarga + " , " + dodc.IdOrdenDescarga + " , '" + dodc.ClaveProducto + "' , '" + dodc.Producto + "' , '" + dodc.Sacos +
                                "' , '" + dodc.PesoxSaco + "' , '" + dodc.Lote + "' , " + dodc.IdProveedor + " , '" + dodc.Proveedor + "' , '" + dodc.PO
                                + "' , '" + time.ToString(format) + "' , '" + time2.ToString(format) + "' , '" + dodc.Shipper + "' , '" + dodc.USDA + "' , '" + dodc.Pedimento +
                                "' , '" + dodc.Saldo + @"'";

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
        //Obtener Detalle orden Carga por ID, LOTE y CLAVE PRODUCTO 
        [Route("DetalleOrdenDescarga/{id}/{lote}/{clave}")]
        public HttpResponseMessage GetDetalleOrdenDescargaId(int id, string lote, string clave)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleOrdenDescarga where IdOrdenDescarga  =" + id + " and Lote = '" + lote + "' and ClaveProducto = '" + clave + "';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("UpdateSaldo/{id}/{saldo}")]
        public string PutUpdateSaldo(int id, string saldo)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @" update DetalleOrdenDescarga set Saldo = '" + saldo + "' where IdDetalleOrdenDescarga = " + id + ";";

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

        [Route("GetODOT/{id}")]
        public HttpResponseMessage GetODOT(int id)
        {
            DataTable table = new DataTable();

            string query = @" exec jnODOT " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetODOTQR/{id}")]
        public HttpResponseMessage GetODOTQR(int id)
        {
            DataTable table = new DataTable();

            string query = @" exec jnODOTQR " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Get JOIN ODOTTB
        [Route("GetODOTTB/{id}/{bodega}")]
        public HttpResponseMessage GetODOTTB(int id, string bodega)
        {
            DataTable table = new DataTable();

            string query = @" select ordentemporal.QR, tarima.* from OrdenDescarga left join ordentemporal on ordentemporal.idOrdenDescarga = OrdenDescarga.idOrdenDescarga 
                                left join tarima on ordentemporal.QR = tarima.QR  where  OrdenDescarga.IdOrdenDescarga = " + id + " and tarima.Bodega = '" + bodega +
                                "'group by OrdenTemporal.QR, tarima.IdTarima, tarima.Sacos, tarima.PesoTotal, tarima.QR, tarima.Bodega";

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
