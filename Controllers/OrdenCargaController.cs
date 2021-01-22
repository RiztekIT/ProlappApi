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
    [RoutePrefix("api/OrdenCarga")]
    public class OrdenCargaController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from dbo.OrdenCarga where Estatus<>'Sin Validar' order by Folio desc";
            query = @"select * from dbo.OrdenCarga order by Folio desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Orden Carga por IDORDENCARGA
        [Route("OrdenCargaID/{id}")]
        public HttpResponseMessage GetOrdenCargaID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenCarga where IdOrdenCarga  =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Detalle orden Carga por ID, LOTE y CLAVE PRODUCTO 
        [Route("DetalleOrdenCarga/{id}/{lote}/{clave}")]
        public HttpResponseMessage GetDetalleOrdenCargaId(int id, string lote, string clave)
        {
            DataTable table = new DataTable();
            string query;

            if (lote == "0")
            {
                query = @"select * from DetalleOrdenCarga where IdOrdenCarga  =" + id + " and ClaveProducto = '" + clave + "';";
            }
            else
            {
                query = @"select * from DetalleOrdenCarga where IdOrdenCarga  =" + id + " and Lote = '" + lote + "' and ClaveProducto = '" + clave + "';";
            }


            //string query = @"select * from DetalleOrdenCarga where IdOrdenCarga  =" + id + " and ClaveProducto = '" + clave + "';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //PRueba RC
        //Obtener MASTER JOIN
        [Route("MasterID/{id}")]
        public HttpResponseMessage GetMasterID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleOrdenCarga where IdOrdenCarga =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("UltimoFolio")]
        public HttpResponseMessage GetUltimoFolio()
        {
            DataTable table = new DataTable();

            string query = @"select Max(folio)+1 as Folio from OrdenCarga";

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

        [Route("AddOrdenCarga")]
        public HttpResponseMessage PostOC(OrdenCarga ordencarga)
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
                                time3.ToString(format) + "' , '" + time4.ToString(format) + "' , " + ordencarga.IdUsuario + " , '" + ordencarga.Usuario + @"'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return Request.CreateResponse(HttpStatusCode.OK, table);
            }


        [Route("AddDetalleOrdenCarga")]
        public string PostDetalleOrdenCarga(DetalleOrdenCarga doc)
        {
            try
            {
               DataTable table = new DataTable();

                DateTime time = doc.FechaMFG;
                DateTime time2 = doc.FechaCaducidad;
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec itInsertNuevoDetalleOrdenCarga " + doc.IdOrdenCarga + " , '" + doc.ClaveProducto + "' , '" + doc.Producto + "' , '" + doc.Sacos +
                                "' , '" + doc.PesoxSaco + "' , '" + doc.Lote + "' , " + doc.IdProveedor + " , '" + doc.Proveedor + "' , '" + doc.PO
                                + "' , '" + time.ToString(format) + "' , '" + time2.ToString(format) +  "' , '" + doc.Shipper + "' , '" + doc.USDA + "' , '" + doc.Pedimento +
                                "' , '" + doc.Saldo + @"'";

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
                                exec etEditarOrdenCarga " + ordencarga.IdOrdenCarga + " , " + ordencarga.Folio + " , '" + time.ToString(format) + "' , " +
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

                DateTime time = doc.FechaMFG;
                DateTime time2 = doc.FechaCaducidad;
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec etEditarDetalleOrdenCarga " + doc.IdDetalleOrdenCarga + " , " + doc.IdOrdenCarga + " , " + doc.IdOrdenCarga + " , '" + doc.ClaveProducto + "' , '" + doc.Producto + "' , '" + doc.Sacos +
                                "' , '" + doc.PesoxSaco + "' , '" + doc.Lote + "' , " + doc.IdProveedor + " , '" + doc.Proveedor + "' , '" + doc.PO
                                + "' , '" + time.ToString(format) + "' , '" + time2.ToString(format) + "' , '" + doc.Shipper + "' , '" + doc.USDA + "' , '" + doc.Pedimento + "' , '" + 
                                doc.Saldo + @"'";

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





        [Route("EstatusDetalle/{Id}/{Estatus}")]
        public string PutEstatusDetalle(int Id, string Estatus)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @" exec etEditarEstatusDetalleCarga " + Id + " , '" + Estatus + "'; ";

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

        [Route("UpdateSaldo/{id}/{{saldo}}")]
        public string PutUpdateSaldo(int id, string saldo, string lote)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @" update DetalleOrdenCarga set Saldo = '" + saldo + "' where IdDetalleOrdenCarga = "+ id + ";";

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

        [Route("ChoferDetalle/{Id}/{Chofer}")]
        public string PutChoferDetalle(int Id, string Chofer)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @" exec etEditarChoferDetalleCarga " + Id + " , '" + Chofer + "'; ";

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

        [Route("ordenCargaTemporal/{id}")]
        public HttpResponseMessage getCargaTemporalOC(int id)
        {
            DataTable table = new DataTable();

            string query = @"select DetalleOrdenCarga.*, OrdenTemporal.* from DetalleOrdenCarga left join OrdenTemporal on DetalleOrdenCarga.ClaveProducto=OrdenTemporal.ClaveProducto and DetalleOrdenCarga.Lote=OrdenTemporal.Lote  where DetalleOrdenCarga.IdOrdenCarga="+id+" and OrdenTemporal.IdOrdenCarga=" + id;

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
