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
                                + producto.Estatus + "' , '" + producto.UnidadMedida + "' , "  + producto.IVA + " , '" + producto.CodigoBarras +
                                "' , '" + producto.ClaveSAT + "' , " + producto.Categoria + @"'
                                ";

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

            }
}