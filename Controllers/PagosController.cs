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
    [RoutePrefix("api/Pagos")]
    public class PagosController : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from Pagos";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetPagoId/{id}")]
        public HttpResponseMessage GetPagoId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Pagos where IdPago = " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("GetPagoFolio/{folio}")]
        public HttpResponseMessage GetPagoFolio(int folio)
        {
            DataTable table = new DataTable();

            string query = @"select * from Pagos where Folio = " + folio;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetPagoTipo/{tipo}")]
        public HttpResponseMessage GetPagoTipo(string tipo)
        {
            DataTable table = new DataTable();

            string query = @"select * from Pagos where TipoDocumento = '" + tipo + "';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetPagoFolioTipo/{folio}/{tipo}")]
        public HttpResponseMessage GetPagoFolioTipo(int folio, string tipo)
        {
            DataTable table = new DataTable();

            string query = @"select * from Pagos where FolioDocumento = " + folio + " and TipoDocumento = '" +tipo+"';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //Obtener Folio y sumarle 1
        [Route("GetNewFolio")]
        public string GetNewFolio()
        {
            string folio;
            DataRow row;
            DataTable table = new DataTable();

            string query = @"select MAX ( Pagos.Folio) + 1 as Folio from Pagos";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
                row = table.Rows[0];
                folio = row["folio"].ToString();
            }

            return folio;
        }

        [Route("BorrarPago/{id}")]
        public string Delete(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" delete Pagos where IdPago =" + id;

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

        public string Post(Pagos p)
        {
            try
            {
                DateTime time = p.FechaPago;

                string format = "yyyy-MM-dd HH:mm:ss";

                DataTable table = new DataTable();

                string query = @"
                               insert into Pagos (Folio, FolioDocumento, TipoDocumento, Cantidad, CuentaOrigen, CuentaDestino, FechaPago, Observaciones)
                                    values ("+p.Folio+", "+p.FolioDocumento+", '"+p.TipoDocumento+"', '"+p.Cantidad+"', '"+p.CuentaOrigen+"', '"+p.CuentaDestino+"', '"+time.ToString(format)+"', '"+p.Observaciones+ @"')";

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

        public string Put(Pagos p)
        {
            try
            {
                DateTime time = p.FechaPago;

                string format = "yyyy-MM-dd HH:mm:ss";

                DataTable table = new DataTable();

                string query = @"
                             update Pagos set Folio = " + p.Folio + ", FolioDocumento = " + p.FolioDocumento + ", TipoDocumento = '" + p.TipoDocumento + "', Cantidad = '" + p.Cantidad +
                             "', CuentaOrigen = '" + p.CuentaOrigen + "', CuentaDestino = '" + p.CuentaDestino + "', FechaPago = '" + time.ToString(format) +
                             "', Observaciones = '" + p.Observaciones + "' where IdPago = "+p.IdPago+@"; ";

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

        [Route("GetComprasAdministrativas")]
        public HttpResponseMessage GetcomprasAdministrativas()
        {
            DataTable table = new DataTable();

            string query = @"select * from Compras where Estatus = 'Administrativa'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("GetComprasMateriaPrima")]
        public HttpResponseMessage GetComprasMateriaPrima()
        {
            DataTable table = new DataTable();

            string query = @"select * from Compras where Estatus != 'Administrativa'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("GetComprasMateriaPrimaEstatus/{estatus}")]
        public HttpResponseMessage GetComprasMateriaPrimaEstatus(string estatus)
        {
            DataTable table = new DataTable();

            string query = @"select * from Compras where Estatus = '"+estatus+"'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetFletes")]
        public HttpResponseMessage GetFletes()
        {
            DataTable table = new DataTable();

            string query = @"select * from FacturaFlete";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetFletesEstado/{estado}")]
        public HttpResponseMessage GetFletesEstado(string estado)
        {
            DataTable table = new DataTable();

            string query = @"select * from FacturaFlete where Estatus = '"+estado+"'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("GetComisiones")]
        public HttpResponseMessage GetComisiones()
        {
            DataTable table = new DataTable();

            string query = @"select * from Pedidos";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("GetComisionesEstado/{estado}")]
        public HttpResponseMessage GetComisionesEstado(string estado)
        {
            DataTable table = new DataTable();

            string query = @"select * from Pedidos where Estatus = '"+estado+"'";

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
