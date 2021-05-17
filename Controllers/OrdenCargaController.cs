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
        public HttpResponseMessage Post(OrdenCarga ordencarga)
        {
           
            {

                DataTable table = new DataTable();


                DateTime time = ordencarga.FechaEnvio;
                DateTime time2 = ordencarga.FechaInicioCarga;
                DateTime time3 = ordencarga.FechaFinalCarga;
                DateTime time4 = ordencarga.FechaExpedicion;

                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec itInsertarNuevaOrdenCarga " + ordencarga.Folio + " , '" + time.ToLocalTime().ToString(format) + "' , " +
                                ordencarga.IdCliente + " , '" + ordencarga.Cliente + "', " + ordencarga.IdPedido + " , '" + ordencarga.Fletera + "' , '" +
                                ordencarga.Caja + "' , '" + ordencarga.Sacos + "' , '" + ordencarga.Kg + "' , '" + ordencarga.Chofer + "' , '" + ordencarga.Origen +
                                "' , '" + ordencarga.Destino + "' , '" + ordencarga.Observaciones + "' , '" + ordencarga.Estatus + "' , '" + time2.ToLocalTime().ToString(format) + "' , '" +
                                time3.ToLocalTime().ToString(format) + "' , '" + time4.ToLocalTime().ToString(format) + "' , " + ordencarga.IdUsuario + " , '" + ordencarga.Usuario + @"'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

              //  return "Se Actualizo Correctamente";
                return Request.CreateResponse(HttpStatusCode.OK, table);
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
                                exec itInsertarNuevaOrdenCarga " + ordencarga.Folio + " , '" + time.ToLocalTime().ToString(format) + "' , " +
                                ordencarga.IdCliente + " , '" + ordencarga.Cliente + "', " + ordencarga.IdPedido + " , '" + ordencarga.Fletera + "' , '" +
                                ordencarga.Caja + "' , '" + ordencarga.Sacos + "' , '" + ordencarga.Kg + "' , '" + ordencarga.Chofer + "' , '" + ordencarga.Origen +
                                "' , '" + ordencarga.Destino + "' , '" + ordencarga.Observaciones + "' , '" + ordencarga.Estatus + "' , '" + time2.ToLocalTime().ToString(format) + "' , '" +
                                time3.ToLocalTime().ToString(format) + "' , '" + time4.ToLocalTime().ToString(format) + "' , " + ordencarga.IdUsuario + " , '" + ordencarga.Usuario + @"'";

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

                ///   string query = @"
                //                 exec itInsertNuevoDetalleOrdenCarga " + doc.IdOrdenCarga + " , '" + doc.ClaveProducto + "' , '" + doc.Producto + "' , '" + doc.Sacos +
                //               "' , '" + doc.PesoxSaco + "' , '" + doc.Lote + "' , " + doc.IdProveedor + " , '" + doc.Proveedor + "' , '" + doc.PO
                //             + "' , '" + time.ToString(format) + "' , '" + time2.ToString(format) +  "' , '" + doc.Shipper + "' , '" + doc.USDA + "' , '" + doc.Pedimento +
                //           "' , '" + doc.Saldo + @"'";
                string query = @"insert into DetalleOrdenCarga (IdOrdenCarga, ClaveProducto, Producto, Sacos, PesoxSaco, Lote, IdProveedor, Proveedor, PO, FechaMFG, FechaCaducidad, Shipper, USDA, Pedimento, Saldo)
                                    values (" + doc.IdOrdenCarga + " , '" + doc.ClaveProducto + "' , '" + doc.Producto + "' , '" + doc.Sacos +
                              "' , '" + doc.PesoxSaco + "' , '" + doc.Lote + "' , " + doc.IdProveedor + " , '" + doc.Proveedor + "' , '" + doc.PO
                            + "' , '" + time.ToLocalTime().ToString(format) + "' , '" + time2.ToLocalTime().ToString(format) +  "' , '" + doc.Shipper + "' , '" + doc.USDA + "' , '" + doc.Pedimento +
                          "' , '" + doc.Saldo + @"')";

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
                                exec etEditarOrdenCarga " + ordencarga.IdOrdenCarga + " , " + ordencarga.Folio + " , '" + time.ToLocalTime().ToString(format) + "' , " +
                                ordencarga.IdCliente + " , '" + ordencarga.Cliente + "', " + ordencarga.IdPedido + " , '" + ordencarga.Fletera + "' , '" +
                                ordencarga.Caja + "' , '" + ordencarga.Sacos + "' , '" + ordencarga.Kg + "' , '" + ordencarga.Chofer + "' , '" + ordencarga.Origen +
                                "' , '" + ordencarga.Destino + "' , '" + ordencarga.Observaciones + "' , '" + ordencarga.Estatus + "' , '" + time2.ToLocalTime().ToString(format) + "' , '" +
                                time3.ToLocalTime().ToString(format) + "' , '" + time4.ToLocalTime().ToString(format) + "' , " + ordencarga.IdUsuario + " , '" + ordencarga.Usuario + @"'";

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
                                update DetalleOrdenCarga set IdOrdenCarga =" + doc.IdOrdenCarga + " , ClaveProducto = '" + doc.ClaveProducto + "' , Producto ='" + doc.Producto + "' , Sacos ='" + doc.Sacos +
                                "' , PesoxSaco ='" + doc.PesoxSaco + "' , Lote='" + doc.Lote + "' , IdProveedor=" + doc.IdProveedor + " , Proveedor='" + doc.Proveedor + "' , PO='" + doc.PO
                                + "' , FechaMFG='" + time.ToLocalTime().ToString(format) + "' , FechaCaducidad='" + time2.ToLocalTime().ToString(format) + "' , Shipper='" + doc.Shipper + "' , USDA='" + doc.USDA + "' , Pedimento='" + doc.Pedimento + "' , Saldo='" + 
                                doc.Saldo + "' where IdDetalleOrdenCarga ="+ doc.IdDetalleOrdenCarga +@"";

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


        [Route("UpdateSaldo/{id}/{saldo}")]
        public string PutUpdateSaldo(int id, string saldo)

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






        // Orden Carga Info- son campos adjuntos a Orden Carga que contienen informacion relevante
        //Obtener Orden Carga Info por IdOrdenCargaInfo
        [Route("OrdenCargaInfoId/{id}")]
        public HttpResponseMessage GetOrdenCargaInfoId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenCargaInfo where IdOrdenCargaInfo  =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Orden Carga Info por IdOrdenCarga
        [Route("OrdenCargaInfoIdOC/{id}")]
    public HttpResponseMessage GetOrdenCargaInfoIdOC(int id)
    {
        DataTable table = new DataTable();

        string query = @"select * from OrdenCargaInfo where IdOrdenCarga  =" + id;

        using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
        using (var cmd = new SqlCommand(query, con))
        using (var da = new SqlDataAdapter(cmd))
        {
            cmd.CommandType = CommandType.Text;
            da.Fill(table);
        }

        return Request.CreateResponse(HttpStatusCode.OK, table);
    }
        //Obtener Orden Carga Info por IdOrdenDescarga
        [Route("OrdenCargaInfoIdOD/{id}")]
        public HttpResponseMessage GetOrdenCargaInfoIdOD(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenCargaInfo where Campo1  ='" + id + "'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("AddOrdenCargaInfo")]
    public string PostOrdenCargaInfo(OrdenCargaInfo o)
    {
        try
        {
            DataTable table = new DataTable();



            string query = @"
                            insert into OrdenCargaInfo (IdOrdenCarga, SelloCaja, Campo1, Campo2, Campo3) values ("+o.IdOrdenCarga+", '"+o.SelloCaja+"', '"+o.Campo1+"', '"+o.Campo2+"', '"+o.Campo3+"')" +  @"";

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
        [Route("UpdateOrdenCargaInfo")]
        public string PutOrdenCargaInfo(OrdenCargaInfo o)
    {
        try
        {


            DataTable table = new DataTable();

  

  

            string query = @"
                             update OrdenCargaInfo set IdOrdenCarga = "+o.IdOrdenCarga+", SelloCaja = '"+o.SelloCaja+"', Campo1 = '"+o.Campo1+"', Campo2 = '"+o.Campo2+"', Campo3 = '"+o.Campo3+"' where IdOrdenCargaInfo = "+o.IdOrdenCargaInfo+"  " + @"";

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
    [Route("DeleteOrdenCargaInfo/{id}")]
    public string DeleteOrdenCargaInfo(int id)
    {
        try
        {

            DataTable table = new DataTable();

            string query = @" delete OrdenCargaInfo where IdOrdenCargaInfo =" + id;

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
}
}
