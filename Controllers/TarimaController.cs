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
    [RoutePrefix("api/Tarima")]
    public class TarimaController : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from dbo.Tarima";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetDetalleTarima")]
        public HttpResponseMessage GetDetalleTarima()
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleTarima";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

            //Obtener cierta tarima por IdTarima
        [Route("GetTarimaID/{id}")]
        public HttpResponseMessage GetTarimaID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Tarima where IdTarima =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetTarimaBodega")]
        public HttpResponseMessage GetTarimaBodega()
        {
            DataTable table = new DataTable();

            string query = @"select tarima.*,DetalleTarima.* from DetalleTarima left join Tarima on Tarima.IdTarima=DetalleTarima.IdTarima";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetTarimaProducto")]
        public HttpResponseMessage GetTarimaProducto(string producto, string bodega)
        {
            DataTable table = new DataTable();

            string query = @"select tarima.*,DetalleTarima.* from DetalleTarima left join Tarima on Tarima.IdTarima=DetalleTarima.IdTarima where DetalleTarima.Producto like '"+producto+ "%' and Tarima.Bodega='"+bodega+"' ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetTarimaProductoAllBodegas/{producto}")]
        public HttpResponseMessage GetTarimaProductoAllBodegas(string producto)
        {
            DataTable table = new DataTable();

            string query = @"select tarima.*,DetalleTarima.* from DetalleTarima left join Tarima on Tarima.IdTarima=DetalleTarima.IdTarima where DetalleTarima.Producto like '" + producto + "' ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetTarimaProductoD")]
        public HttpResponseMessage GetTarimaProductoD(string producto, string lote)
        {
            DataTable table = new DataTable();

            string query = @"select DetalleOrdenCarga.Sacos from OrdenCarga left join DetalleOrdenCarga on OrdenCarga.IdOrdenCarga=DetalleOrdenCarga.IdOrdenCarga where DetalleOrdenCarga.Producto = '"+producto+"' and lote='"+lote+"' and (OrdenCarga.Estatus='Creada' or OrdenCarga.Estatus='Preparada' )";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener cierto detalle tarima por IdTarima
        [Route("GetDetalleTarimaID/{id}")]
        public HttpResponseMessage GetDetalleTarimaID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleTarima where IdTarima =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //Obtener cierto detalle tarima por IdDetalleTarima
        [Route("GetDetalleTarimaIDdetalle/{id}")]
        public HttpResponseMessage GetDetalleTarimaIDdetalle(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleTarima where IdDetalleTarima =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener ultima tarima insertada
        [Route("GetUltimaTarima")]
        public HttpResponseMessage GetUltimaTarima()
        {
            DataTable table = new DataTable();

            string query = @"select TOP 1 * from Tarima order by Tarima.IdTarima desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Tarima por codigo QR
        [Route("GetTarimaQR/{qr}")]
        public HttpResponseMessage GetTarimaQR(string qr)
        {
            DataTable table = new DataTable();

            string query = @"select * from Tarima where QR = '" + qr + "'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener detalle tarima por IdTarima, ClaveProducto y Lote
        [Route("GetDetalleTarimaIdClaveLote/{id}/{clave}/{lote}")]
        public HttpResponseMessage GetDetalleTarimaIdClaveLote(int id, string clave, string lote)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleTarima where IdTarima = " + id + " and ClaveProducto = '" + clave + "' and Lote = '" + lote + "';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [Route("BorrarTarima/{id}")]
        public string Delete(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" exec dtBorrarTarima " + id;

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
        [Route("BorrarDetalleTarima/{id}")]
        public string DeleteDetalleTarima(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" exec dtBorrarDetalleTarima " + id;

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



        public string Post(Tarima t)
        {
            try
            {

                DataTable table = new DataTable();       

                string query = @"
                                exec itInsertNuevaTarima '" + t.Sacos + "' , '" + t.PesoTotal + "' , '" + t.QR + "' , '" + t.Bodega + @"'";

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

        [Route("AddDetalleTarima")]
        public string PostDetalleTarima(DetalleTarima dt)
        {
            try
            {
                DataTable table = new DataTable();

                DateTime time = dt.FechaMFG;
                DateTime time2 = dt.FechaCaducidad;
                string format = "yyyy-MM-dd HH:mm:ss";

                string query = @"
                                exec itInsertNuevoDetalleTarima " + dt.IdTarima + " , '" + dt.ClaveProducto + "' , '" + dt.Producto + "' , '" + dt.Sacos +
                                "' , '" + dt.PesoxSaco + "' , '" + dt.Lote + "' , " + dt.IdProveedor + " , '" + dt.Proveedor + "' , '" + dt.PO
                                + "' , '" + time.ToString(format) + "' , '" + time2.ToString(format) + "' , '" + dt.Shipper + "' , '" + dt.USDA + "' , '" + dt.Pedimento + @"'";

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


        public string Put(Tarima t)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @"
                                exec etEditarTarima " + t.IdTarima + " , '" + t.Sacos + "' , '" + t.PesoTotal + "' , '" + t.QR + "' , '" + t.Bodega + @"'";

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

        [Route("UpdateDetalleTarima")]
        public string PutDetalleTarima(DetalleTarima dt)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                                exec etEditarDetalleTarima " + dt.IdDetalleTarima + " , " + dt.IdTarima + " , '" + dt.ClaveProducto + "' , '" + dt.Producto + "' , '" + dt.Sacos +
                                "' , '" + dt.PesoxSaco + "' , '" + dt.Lote + "' , " + dt.IdProveedor + " , '" + dt.Proveedor + "' , '" + dt.PO
                                + "' , '" + dt.FechaMFG + "' , '" + dt.FechaCaducidad + "' , '" + dt.Shipper + "' , '" + dt.USDA + "' , '" + dt.Pedimento + @"";

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

        //Update TARIMA Sacos y peso Total
        [Route("UpdateTarimaSacosPeso/{id}/{sacos}/{peso}")]
        public string PutTarimasSacosPeso(int id, string sacos, string peso)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"update Tarima set Sacos = '" + sacos + "', PesoTotal = '" + peso + "' where IdTarima = " + id + ";";

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

        //Update DETALLE TARIMA IDTarima y Sacos
        [Route("UpdateDetalleTarimaIdSacos/{idt}/{iddt}/{sacos}")]
        public string PutDetalleTarimaIdSacos(int idt, int iddt, string sacos)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"update DetalleTarima set IdTarima = " + idt + ", Sacos = '" + sacos + "' where IdDetalleTarima = " + iddt + ";";

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

        [Route("GetTarimaBodega/{bodega}")]
        public HttpResponseMessage GetTarimaBodega(string bodega)
        {
            DataTable table = new DataTable();

            string query = @"select * from Tarima where Bodega =" + bodega;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            { 
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
               
        [Route("GetTarimaDttqr/{qr}")]
        public HttpResponseMessage GetTarimaDttqr(string qr)
        {
            DataTable table = new DataTable();

            string query = @" exec jnTarimaDtt " + " '" + qr + "' ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetTarimaBodegaQR/{qr}/{bodega}")]
        public HttpResponseMessage GetTarimaBodegaQR(string qr, string bodega)
        {
            DataTable table = new DataTable();

            string query = @" select * from Tarima where tarima.QR = '"+qr+"' and tarima.Bodega = '"+bodega+"';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("UpdateBodega/{bodega}/{qr}")]
        public string PutBodegaTarima(string bodega, string qr)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"update Tarima set Bodega ='"+bodega+"' where QR='"+qr+"'";

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

        [Route("TarimaOC/{id}")]
        public HttpResponseMessage GetTarimaOC(int id)
        {
            DataTable table = new DataTable();

            string query = @"select Tarima.* from Tarima left join OrdenTemporal on Tarima.IdTarima=OrdenTemporal.IdTarima where OrdenTemporal.IdOrdenCarga="+id+"";

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
