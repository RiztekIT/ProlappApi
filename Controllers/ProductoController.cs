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
    [RoutePrefix("api/Producto")]
    public class ProductoController : ApiController
    {




        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from dbo.Producto where Estatus='Activo'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        

        public string Post(Producto producto)
        {
            try
            {


                DataTable table = new DataTable();
                string query = @"
                                Execute itInsertNuevoProducto '" + producto.Nombre + "' , '" + producto.PrecioVenta + "' , '" + producto.PrecioCosto + "' , '" + producto.Cantidad +
                                "' , '" + producto.ClaveProducto + "' , '" + producto.Stock + "' , '" + producto.DescripcionProducto + "' , '"
                                + producto.Estatus + "' , '" + producto.UnidadMedida + "' , '"  + producto.IVA + "' , '" + producto.CodigoBarras +
                                "' , '" + producto.ClaveSAT + "' , '" + producto.Categoria + @"'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Producto Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }

        public string Delete(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                              exec dtBorrarProducto " + id;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Se Elimino Correctamente";
            }
            catch (Exception)
            {
                return "Se produjo un error";
            }
        }

        public string Put(Producto producto)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @"
                                exec etEditarProducto " + producto.IdProducto + " , '" + producto.Nombre + "' , '" + producto.PrecioVenta + "' , '" + producto.PrecioCosto + "' , '" + producto.Cantidad +
                                "' , '" + producto.ClaveProducto + "' , '" + producto.Stock + "' , '" + producto.DescripcionProducto + "' , '"
                                + producto.Estatus + "' , '" + producto.UnidadMedida + "' , " + producto.IVA + " , '" + producto.CodigoBarras +
                                "' , '" + producto.ClaveSAT + "'," + producto.Categoria + @"'
                                ";

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
        [Route("Movimiento")]
        public string PostMovimiento(MovimientoProducto movimiento)
        {
            try
            {
                DataTable table = new DataTable();


                string query = @"insert into MovimientosProductos values ('"+movimiento.ClaveProducto+ "','" + movimiento.Producto + "','" + movimiento.Marca + "','" + movimiento.Origen + "','" + movimiento.Presentacion + "','" + movimiento.Tipo + "'," + movimiento.Cantidad + ")";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Movimiento Agregado";
            }
            catch (Exception)
            {
                return "Se produjo un error";
            }
            }
        [Route("NombreProducto")]
        public HttpResponseMessage GetNombreProducto(string clave)
        {
            DataTable table = new DataTable();

            string query = @"select * from Producto where '"+clave+"' like ClaveProducto+'%';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("NombreMarca")]
        public HttpResponseMessage GetNombreMarca(string clave)
        {
            DataTable table = new DataTable();

            string query = @"select NombreMarca from MarcasProductos where '"+clave+"' like '%'+ClaveMarca+'%' group by NombreMarca";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("NombreOrigen")]
        public HttpResponseMessage GetNombreOrigen(string clave)
        {
            DataTable table = new DataTable();

            string query = @"select NombreOrigen from OrigenProductos where '"+clave+"' like '%'+ClaveOrigen";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("NombrePresentacion")]
        public HttpResponseMessage GetNombrePresentacion(string producto)
        {
            DataTable table = new DataTable();

            string query = @"select * from PresentacionProductos where '"+producto+"' like '%'+Presentacion ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        // inicio marcas productos

        [Route("GetMarcasProductos")]
        public HttpResponseMessage GetMarcasProductos()
        {
            DataTable table = new DataTable();

            string query = @"select * from MarcasProductos";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("MarcasProductos")]
        public string PostMarcasProductos(MarcasProductos marcasProductos)
        {
            try
            {

                DataTable table = new DataTable();
                string query = @"insert into MarcasProductos values(" + marcasProductos.IdMarca + ",'" + marcasProductos.NombreMarca + "','" + marcasProductos.ProductoMarca + "','" + marcasProductos.ClaveMarca + "')";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Productos Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }

        [Route("MarcasProductos")]
        public string DeleteMarcasProductos(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                              Delete from MarcasProductos where idMarca = " + id;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["TalleresZarco"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Se Elimino Correctamente";
            }
            catch (Exception)
            {
                return "Se produjo un error";
            }
        }


    }
}