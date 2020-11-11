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
    [RoutePrefix("api/Documentos")]
    public class DocumentosController : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from Documentos";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetDocumentoId/{id}")]
        public HttpResponseMessage GetDocumnetoId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from Documentos where IdDocumento = " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetDocumnetoPath/{path}")]
        public HttpResponseMessage GetDocumentoPath(string path)
        {
            DataTable table = new DataTable();

            string query = @"select * from Documentos where Path = '" + path + "';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Get Documento por Tipo, Folio y Nombre Imagen
        [Route("GetDocumentoFTN")]
        public HttpResponseMessage PostDocumentosFTN(Documento i)
        {
            DataTable table = new DataTable();

            string query = @"select * from Documentos where Folio =" + i.Folio + " and Tipo = '" + i.Tipo + "' and NombreDocumento = '" + i.NombreDocumento + "'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //get OrdenesDescarga Descaradas
        [Route("GetOrdenesDescargadas")]
        public HttpResponseMessage GetOrdenesDescargadas()
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenDescarga where Estatus = 'Descargada'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //get GetOrdenDescargaFolio
        [Route("GetOrdenDescargaFolio/{folio}")]
        public HttpResponseMessage GetOrdenDescargaFolio(int folio)
        {
            DataTable table = new DataTable();

            string query = @"select * from OrdenDescarga where Folio =" + folio +" and Estatus ='Descargada'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //get GetCompraFolio
        [Route("GetCompraFolio/{folio}")]
        public HttpResponseMessage GetCompraFolio(int folio)
        {
            DataTable table = new DataTable();

            string query = @"select * from Compras where Folio =" + folio + " and Estatus ='Terminada'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        //get Documento tipo y folio
        [Route("GetDocumentoFolioTipoModulo/{folio}/{tipo}/{modulo}")]
        public HttpResponseMessage GetDocumentoFolioTipoModulo(int folio, string tipo, string modulo)
        {
            DataTable table = new DataTable();

            string query = @"select * from Documentos where Folio = "+folio+" and Tipo = '"+tipo+"' and Modulo = '"+modulo+"'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //get Join DetalleOrdenDescarga con Documento
        [Route("GetJoinDodD/{id}/{clave}")]
        public HttpResponseMessage GetJoinDodD(int id, string clave)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleOrdenDescarga left join Documentos on DetalleOrdenDescarga.IdDetalleOrdenDescarga = Documentos.IdDetalle where DetalleOrdenDescarga.IdOrdenDescarga = "+id
                +" and Documentos.Modulo = 'Importacion' and Documentos.Tipo = 'OrdenDescarga' and Documentos.ClaveProducto = '"+clave+"'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //get Compras Terminadas
        [Route("GetComprasTerminadas")]
        public HttpResponseMessage GetComprasTerminadas()
        {
            DataTable table = new DataTable();

            string query = @"select * from Compras where Estatus = 'Terminada'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //get DetalleOrdenDescargaId
        [Route("GetDetalleODId/{id}")]
        public HttpResponseMessage GetDetalleODId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleOrdenDescarga where IdOrdenDescarga ="+id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //get DetalleCompraId
        [Route("GetDetalleCompraId/{id}")]
        public HttpResponseMessage GetDetalleCompraId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleCompra where IdCompra =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //get Join DetalleCompras con Documento
        [Route("GetJoinDcD/{id}/{clave}")]
        public HttpResponseMessage GetJoinDcD(int id, string clave)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetalleCompra left join Documentos on DetalleCompra.IdDetalleCompra = Documentos.IdDetalle where DetalleCompra.IdCompra ="+ id 
                +" and Documentos.Modulo = 'Importacion' and Documentos.Tipo = 'Compras' and Documentos.ClaveProducto = '"+clave+"'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //get documento por tipo y modulo
        [Route("GetDocumentoTipoModulo/{tipo}/{modulo}")]
        public HttpResponseMessage GetDocumentoTipoModulo(int folio, string tipo, string modulo)
        {
            DataTable table = new DataTable();

            string query = @"select * from Documentos where Tipo = '" + tipo + "' and Modulo = '" + modulo + "'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }



        [Route("BorrarDocumento/{id}")]
        public string Delete(int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" Delete Documentos where IdDocumento = " + id;

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
        //Borrar Documento por Folio, Modulo, Tipo, Nombre Documento y IdDetalle
        [Route("BorrarDocumentoFMTDID")]
        public string PostDocumentoFMTDID(Documento doc)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @"delete Documentos where Folio = "+doc.Folio+" and Modulo = '"+doc.Modulo+"' and Tipo='"+doc.Tipo+"' and NombreDocumento = '"+doc.NombreDocumento+"' and IdDetalle = "+doc.IdDetalle;

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

        //Get Documento por Folio, Modulo, Tipo, Nombre Documento y IdDetalle
        [Route("GetDocumentoFMTDID")]
        public HttpResponseMessage PostDocumentoFMTDNID(Documento doc)
        {
           

                DataTable table = new DataTable();

                string query = @"select * from Documentos where Folio = " + doc.Folio + " and Modulo = '" + doc.Modulo + "' and Tipo='" + doc.Tipo + "' and NombreDocumento = '" + doc.NombreDocumento + "' and IdDetalle = " + doc.IdDetalle;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return Request.CreateResponse(HttpStatusCode.OK, table);
            

            
        }

        //Get updateUsda
        [Route("updateUsda/{usda}/{id}")]
        public string PutUsda(string usda, int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @"update DetalleOrdenDescarga set USDA = '"+usda+"' where IdDetalleOrdenDescarga = "+id;

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

        //Get pedimento
        [Route("updatePedimento/{pedimento}/{id}")]
        public string PutPedimento(string pedimento, int id)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @"update DetalleOrdenDescarga set Pedimento = '" + pedimento + "' where IdDetalleOrdenDescarga = " + id;

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

        //Borrar documento por tipo, folio, nombreDocumento
        [Route("BorrarDocumentoTFN")]
        public string PostBorrarDocumentoTFN(Documento d)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @" delete from Documentos where Tipo = '" + d.Tipo + "' and Folio = " + d.Folio + " and NombreDocumento = '" + d.NombreDocumento + "'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Se elimino Correctamente";
            }
            catch (Exception exe)
            {
                return "Error al Eliminar " + exe;
            }
        }

        public string Post(Documento d)
        {
            try
            {

                DateTime time = d.Vigencia;

                string format = "yyyy-MM-dd HH:mm:ss";

                DataTable table = new DataTable();

                string query = @"
                                INSERT INTO Documentos (Folio, IdDetalle, Modulo, Tipo, ClaveProducto, NombreDocumento, Path, Observaciones, Vigencia) VALUES 
                                ("+d.Folio+", "+d.IdDetalle+", '"+d.Modulo+"', '"+d.Tipo+"', '"+d.ClaveProducto+"', '"+d.NombreDocumento+"', '"+d.Path+"', '"+d.Observaciones+"', '"+time.ToString(format)+ @"');";

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

        public string Put(Documento d)
        {
            try
            {
                DateTime time = d.Vigencia;

                string format = "yyyy-MM-dd HH:mm:ss";

                DataTable table = new DataTable();

                string query = @"
                                UPDATE Documentos SET Folio = "+d.Folio+", IdDetalle ="+d.IdDetalle+", Modulo = '"+d.Modulo+"', Tipo = '"+d.Tipo+"', ClaveProducto ='" + d.ClaveProducto + "', NombreDocumento = '"+d.NombreDocumento+"'," +
                                " Path = '"+d.Path+"', Observaciones = '"+d.Observaciones+"', Vigencia = '"+time.ToString(format)+"' where IdDocumento = "+d.IdDocumento+ @";";

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
