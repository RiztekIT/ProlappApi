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

                string query = @" exec dtBorrarDetalleTarima" + id;

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
                                exec itInsertNuevaTarima '" + t.Sacos + "' , '" + t.PesoTotal + "' , '" + t.QR + @"'";

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

                return "Se Actualizo Correctamente";
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
                                exec etEditarTarima " + t.IdTarima + " , '" + t.Sacos + "' , '" + t.PesoTotal + "' , '" + t.QR + @"'";

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





    }
}
