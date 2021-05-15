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
    public class Querys
    {
        public string consulta { get; set; }
    }
    [RoutePrefix("api/TraspasoMercancia")]
    public class TraspasoMercanciaController : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from TraspasoMercancia order by Folio desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("GetTraspasoMercanciaid/{id}")]
        public HttpResponseMessage GetTraspasoMercanciaid(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from TraspasoMercancia where IdTraspasoMercancia =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }



        [Route("PostTraspasoMercancia")]
        public string Post(TraspasoMercancia t)
        {
            try
            {
                DataTable table = new DataTable();
                //Las variables de fecha, son igualadas a un valor Datatime
                DateTime time = t.FechaExpedicion;
                //Al momento de insertar los valores de las fechas, estan seran insertadas con el formato 'Format'
                string format = "yyyy-MM-dd HH:mm:ss";
                //De esta manera no causara error al tratar de insertar fechas en la base de datos SQL
                //time.ToString(format)
                string query = @"insert into TraspasoMercancia (Folio, IdOrdenCarga, FolioOrdenCarga, IdCliente, Cliente, SacosTotales, KilogramosTotales, FechaExpedicion, Estatus, Origen, Destino, CampoExtra1, CampoExtra2) values ("+t.Folio+", "
                    +t.IdOrdenCarga+" , "+t.FolioOrdenCarga+", "+t.IdCliente+", '"+t.Cliente+"', '"+t.SacosTotales+"', '"+t.KilogramosTotales+"', '"+time.ToLocalTime().ToString(format)+"', '"+t.Estatus+"', '"+t.Origen+"','"+t.Destino+"', '"+t.CampoExtra1+"', '"+t.CampoExtra2+"')";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Agregado con Exito";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }
        [Route("PutTraspasoMercancia")]
        public string Put(TraspasoMercancia t)
        {
            try
            {
                DataTable table = new DataTable();
                //Las variables de fecha, son igualadas a un valor Datatime
                DateTime time = t.FechaExpedicion;
                //Al momento de insertar los valores de las fechas, estan seran insertadas con el formato 'Format'
                string format = "yyyy-MM-dd HH:mm:ss";
                //De esta manera no causara error al tratar de insertar fechas en la base de datos SQL
                //time.ToString(format)

                string query = @"update TraspasoMercancia set Folio = "+t.Folio+", IdOrdenCarga = "+t.IdOrdenCarga+", FolioOrdenCarga = "+t.FolioOrdenCarga+", IdCliente = "+t.IdCliente+", Cliente = '"+t.Cliente+"', SacosTotales = '"+t.SacosTotales+"', KilogramosTotales = '"+t.KilogramosTotales+"'," +
                    " FechaExpedicion = '"+time.ToLocalTime().ToString(format)+"', Estatus = '"+t.Estatus+"', Origen = '"+t.Origen+"', Destino = '"+t.Destino+"', CampoExtra1 = '"+t.CampoExtra1+"', CampoExtra2 = '"+t.CampoExtra2+"' where IdTraspasoMercancia = "+t.IdTraspasoMercancia+"";

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

        [Route("DeleteTraspasoMercancia/{id}")]
        public string Delete(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"delete TraspasoMercancia where IdTraspasoMercancia = " + id;

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
        [Route("GetDetalleTraspasoMercancia/{id}")]
        public HttpResponseMessage GetDetalleTraspasoMercancia(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleTraspasoMercancia where IdTraspasoMercancia="+id+"";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("PostDetalleTraspasoMercancia")]
        public string PostDetalleTraspasoMercancia(DetalleTraspasoMercancia t)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"insert into DetalleTraspasoMercancia (IdTraspasoMercancia, IdDetalle, Cbk, Usda, IdProveedor, Proveedor, PO, Producto, ClaveProducto, Lote, Sacos, PesoxSaco, PesoTotal, Bodega, CampoExtra3, CampoExtra4) values("+t.IdTraspasoMercancia+"," +
                    " "+t.IdDetalle+" , '"+t.Cbk+"', '"+t.Usda+"', "+t.IdProveedor+", '"+t.Proveedor+"', '"+t.PO+"', '"+t.Producto+"', '"+t.ClaveProducto+"', '"+t.Lote+"', '"+t.Sacos+"', '"+t.PesoxSaco+"','"+t.PesoTotal+"', '"+t.Bodega+"', '"+t.CampoExtra3+"', '"+t.CampoExtra4+"')";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Agregado con Exito";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }

        [Route("PutDetalleTraspasoMercancia")]
        public string PutDetalleTraspasoMercancia(DetalleTraspasoMercancia t)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"update DetalleTraspasoMercancia set IdTraspasoMercancia = "+t.IdTraspasoMercancia+", IdDetalle = "+t.IdDetalle+", Cbk = '"+t.Cbk+"', Usda = '"+t.Usda+"', IdProveedor = "+t.IdProveedor+", Proveedor = '"+t.Proveedor+"', PO = '"+t.PO+"'," +
                    " Producto = '"+t.Producto+"', ClaveProducto = '"+t.ClaveProducto+"', Lote = '"+t.Lote+"', Sacos = '"+t.Sacos+"', PesoxSaco = '"+t.PesoxSaco+"', PesoTotal = '"+t.PesoTotal+"', Bodega = '"+t.Bodega+"', CampoExtra3 = '"+t.CampoExtra3+"', CampoExtra4 = '"+t.CampoExtra4+"' where IdDetalleTraspasoMercancia = "+t.IdDetalleTraspasoMercancia+" ";

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

        [Route("DeleteDetalleTraspasoMercancia/{id}")]
        public string DeleteDetalleTraspasoMercancia(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"delete DetalleTraspasoMercancia where IdDetalleTraspasoMercancia = " + id;

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


        [Route("general")]
        public HttpResponseMessage PostGeneral(Querys consulta)
        {
            DataTable table = new DataTable();

            string query = @"" + consulta.consulta + "";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
            //return consulta;
        }

    }
}
