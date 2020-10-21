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

        //Obtener detalle tarima por Bodega ClaveProducto y Lote
        [Route("GetDetalleTarimaBodegaClaveLote/{bodega}/{clave}/{lote}")]
        public HttpResponseMessage GetDetalleTarimaBodegaClaveLote(string bodega, string clave, string lote)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleTarima where Bodega = '" + bodega + "' and ClaveProducto = '" + clave + "' and Lote = '" + lote + "';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //Obtener detalle tarima por ClaveProducto y Lote
        [Route("GetDetalleTarimaClaveLote/{clave}/{lote}")]
        public HttpResponseMessage GetDetalleTarimaClaveLote(string clave, string lote)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleTarima where ClaveProducto = '" + clave + "' and Lote = '" + lote + "';";

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

                string query = @"insert into DetalleTarima (ClaveProducto, Producto, SacosTotales, PesoxSaco, Lote, PesoTotal, SacosxTarima, TarimasTotales, Bodega, IdProveedor, Proveedor, PO, FechaMFG, FechaCaducidad, Shipper, USDA, Pedimento, Estatus) VALUES ('" +
                                     dt.ClaveProducto + "', '" + dt.Producto + "', '" + dt.SacosTotales + "', '" + dt.PesoxSaco + "', '" + dt.Lote + "', '" + dt.PesoTotal + "', '" + dt.SacosxTarima + "', '" + dt.TarimasTotales + "', '" + dt.Bodega +
                                        "', " + dt.IdProveedor + ", '" + dt.Proveedor + "', '" + dt.PO + "', '" + time.ToString(format) + "', '" + time2.ToString(format) + "', '" + dt.Shipper + "', '" + dt.USDA + "', '" + dt.Pedimento + "', '" + dt.Estatus + @"')";

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
                DateTime time = dt.FechaMFG;
                DateTime time2 = dt.FechaCaducidad;
                string format = "yyyy-MM-dd HH:mm:ss";

                DataTable table = new DataTable();

                string query = @"
                             update DetalleTarima set ClaveProducto = '" + dt.ClaveProducto + "', Producto = '" + dt.Producto + "', SacosTotales = '" + dt.SacosTotales + "', PesoxSaco = '" + dt.PesoxSaco + "', Lote = '" + dt.Lote + "', PesoTotal = '" + dt.PesoTotal + "', SacosxTarima = '" + dt.SacosxTarima +
                                "', TarimasTotales = '" + dt.TarimasTotales + "', Bodega = '" + dt.Bodega + "', IdProveedor = " + dt.IdProveedor + ", Proveedor = '" + dt.Proveedor + "', PO = '" + dt.PO + "', FechaMFG = '" + time.ToString(format) + "', FechaCaducidad = '" + time2.ToString(format) +
                                    "', Shipper = '" + dt.Shipper + "', USDA = '" + dt.USDA + "', Pedimento = '" + dt.Pedimento + "', Estatus = '" + dt.Estatus + "' where IdDetalleTarima = " + dt.IdDetalleTarima + @"";

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
        [Route("UpdateDetalleTarimaIdSacos/{iddt}/{sacos}")]
        public string PutDetalleTarimaIdSacos(int iddt, string sacos)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"update DetalleTarima set Sacos = '" + sacos + "' where IdDetalleTarima = " + iddt + ";";

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

            string query = @" select * from Tarima where tarima.QR = '" + qr + "' and tarima.Bodega = '" + bodega + "';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [Route("GetProductoClaveProducto/{clave}")]
        public HttpResponseMessage GetProductoClaveProducto(string clave)
        {
            DataTable table = new DataTable();

            string query = @"select * from dbo.Producto where Estatus='Activo' and ClaveProducto ='" + clave + "'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("GetSumatoriaAllBodegas")]
        public HttpResponseMessage GetSumatoriaAllBodegas()
        {
            DataTable table = new DataTable();

            string query = @"select SUM(PARSE(Sacos as INT)) as Sacos from Tarima";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("GetSumatoriaBodega/{bodega}")]
        public HttpResponseMessage GetSumatoriaBodega(string bodega)
        {
            DataTable table = new DataTable();

            string query = @"select SUM(PARSE(Sacos as INT)) as Sacos from Tarima where Bodega = '" + bodega + "'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        // ACTUALIZACION DE ALMACEN 
        [Route("GetProductoInformacionBodega/{ClaveProducto}/{Lote}/{bodega}")]
        public HttpResponseMessage GetProductoInformacionBodega(string ClaveProducto, string Lote, string bodega)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleTarima where ClaveProducto = '" + ClaveProducto + "' and lote  = '" + Lote + "' and Bodega = '" + bodega + "'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        //Update DETALLE TARIMA Sacos, Peso total, Tarimas totales y Bodega
        [Route("UpdateDetalleTarimaSacosPesoTarimasBodega")]
        public string PutDetalleTarimaSacosPesoTarimasBodega(DetalleTarima dt)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"update DetalleTarima set SacosTotales = '" + dt.SacosTotales + "', PesoTotal = '"+dt.PesoTotal+"', TarimasTotales = '"+dt.TarimasTotales+"', Bodega = '"+dt.Bodega+"' where IdDetalleTarima = " + dt.IdDetalleTarima + ";";

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


        //Obtener ultim detalle tarima insertada
        [Route("GetUltimoDetalleTarima")]
        public HttpResponseMessage GetUltimoDetalleTarima()
        {
            DataTable table = new DataTable();

            string query = @"select TOP 1 * from DetalleTarima order by DetalleTarima.IdDetalleTarima desc";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener detalle Tarima por Bodega
        [Route("GetDetalleTarimaBodega/{bodega}")]
        public HttpResponseMessage GetDetalleTarimaBodega(string bodega)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleTarima where Bodega = '"+bodega+"'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener detalle Tarima por Bodega
        [Route("GetDetalleTarimaClaveLoteBodega/{Clave}/{Lote}/{bodega}")]
        public HttpResponseMessage GetDetalleTarimaClaveLoteBodega(string Clave, string Lote, string bodega)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleTarima where ClaveProducto = '"+Clave+"' and Lote = '"+Lote+"' and Bodega = '" + bodega + "'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener detalle Tarima por Bodega
        [Route("UpdateDetalleTarimaBodega/{id}/{bodega}")]
        public HttpResponseMessage GetUpdateDetalleTarimaBodega(int id, string bodega)
        {
            DataTable table = new DataTable();

            string query = @"update detalleTarima set bodega = '"+bodega+"' where IdDetalleTarima = " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }




        //FIN 
    }
}
