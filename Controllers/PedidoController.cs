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
    [RoutePrefix("api/Pedido")]
    public class PedidoController : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"exec stSelectTablaPedidos";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("PedidoId/{id}")]
        public HttpResponseMessage GetPedidoId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from pedidos where idPedido =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener Join de Pedido con cliente
        [Route("PedidoCliente")]
        public HttpResponseMessage GetPedidoCliente()
        {
            DataTable table = new DataTable();

            string query = @"Select Pedidos.*, Cliente.* from Pedidos LEFT JOIN Cliente ON Pedidos.IdCliente = Cliente.IdClientes order by IdPedido;";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener detalles de pedido dependiendo el id del pedido
        [Route("DetallePedidoId/{id}")]
        public HttpResponseMessage GetDetallePedidoId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DetallePedidos where IdPedido =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener direcciones de cierto cliente por ID
        [Route("DireccionCliente/{id}")]
        public HttpResponseMessage GetDireccionCliente(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DireccionesCliente where IdCliente =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //Obtener direcciones por ID Direccion
        [Route("DireccionID/{id}")]
        public HttpResponseMessage GetDireccionID(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from DireccionesCliente where IdDireccion =" + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("UltimoPedido")]
        public HttpResponseMessage GetUtimoPedido()
        {
            DataTable table = new DataTable();

            string query = @"select MAX(IdPedido) as IdPedido from pedidos";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Pedido pedido)
        {
            try
            {


                DataTable table = new DataTable();
                //Las variables de fecha, son igualadas a un valor Datatime
                DateTime time2 = pedido.FechaVencimiento;
                DateTime time3 = pedido.FechaDeEntrega;
                //Al momento de insertar los valores de las fechas, estan seran insertadas con el formato 'Format'
                string format = "yyyy-MM-dd HH:mm:ss";
                //De esta manera no causara error al tratar de insertar fechas en la base de datos SQL
                //time.ToString(format)
                string query = @"
                                Execute itInsertNuevoPedido " + pedido.IdCliente + " , '" + pedido.Folio + "' , '"
                                + pedido.Subtotal + "' , '" + pedido.Descuento + "' , '"
                                + pedido.Total + "' , '" + pedido.Observaciones + "' , '"
                                + time2.ToString(format) + "' , '" + pedido.OrdenDeCompra + "' , '"
                                + time3.ToString(format) + "' , '" + pedido.CondicionesDePago + "' , '" + pedido.Vendedor + "' , '"
                                + pedido.Estatus + "' , '" + pedido.Usuario + "' , '"
                                + pedido.Factura + "' , '" + pedido.LugarDeEntrega + "' , '" + pedido.Moneda + "' , '" + pedido.Prioridad + "', '" 
                                + pedido.SubtotalDlls + "' , '" + pedido.DescuentoDlls + "' , '" + pedido.TotalDlls + "' , '" + pedido.Flete + " , " + pedido.IdDireccion +  @"'
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Pedido Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }

        public string Put(Pedido pedido)
        {
            try
            {


                DataTable table = new DataTable();
                //Las variables de fecha, son igualadas a un valor Datatime
                DateTime time2 = pedido.FechaVencimiento;
                DateTime time3 = pedido.FechaDeEntrega;
                //Al momento de insertar los valores de las fechas, estan seran insertadas con el formato 'Format'
                string format = "yyyy-MM-dd HH:mm:ss";
                //De esta manera no causara error al tratar de insertar fechas en la base de datos SQL
                //time.ToString(format)
                string query = @"
                                Execute etEditarPedido " + pedido.IdPedido + " , " + pedido.IdCliente + " , '" + pedido.Folio + "' , '"
                                + pedido.Subtotal + "' , '" + pedido.Descuento + "' , '"
                                + pedido.Total + "' , '" + pedido.Observaciones + "' , '"
                                + time2.ToString(format) + "' , '" + pedido.OrdenDeCompra + "' , '"
                                + time3.ToString(format) + "' , '" + pedido.CondicionesDePago + "' , '" + pedido.Vendedor + "' , '"
                                + pedido.Estatus + "' , '" + pedido.Usuario + "' , '"
                                + pedido.Factura + "' , '" + pedido.LugarDeEntrega + "' , '" + pedido.Moneda + "' , '" + pedido.Prioridad + "' , '" 
                                + pedido.SubtotalDlls + "' , '" + pedido.DescuentoDlls + "' , '" + pedido.TotalDlls + "' , '" + pedido.Flete + " , " + pedido.IdDireccion + @"'
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Pedido Actualizado";
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
                              exec dtBorrarPedido " + id;

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
        //Agregar Detalle Pedido
        [Route("InsertDetallePedido")]
        public string PostDetallePedido(DetallePedido dp)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"
                                Execute itInsertNuevoDetallePedido " + dp.IdPedido + " , '" + dp.ClaveProducto + "' , '"
                                + dp.Producto + "' , '" + dp.Unidad + "' , '"
                                + dp.PrecioUnitario + "' , '" + dp.Cantidad + "' , '"
                                + dp.Importe + "' , '" + dp.Observaciones + "' , '" + dp.TextoExtra + "'  ,  '"
                                + dp.PrecioUnitarioDlls + "' , '" + dp.ImporteDlls + "'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Detalle Pedido Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }
        //Editar Detalle Pedido
        [Route("EditDetallePedido")]
        public string PutDetallePedido(DetallePedido dp)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"
                                Execute etEditarDetallePedido " + dp.IdDetallePedido + " , " + dp.IdPedido + " , '" + dp.ClaveProducto + "' , '"
                                + dp.Producto + "' , '" + dp.Unidad + "' , '"
                                + dp.PrecioUnitario + "' , '" + dp.Cantidad + "' , '"
                                + dp.Importe + "' , '" + dp.Observaciones + "' , '" + dp.TextoExtra + "'  ,  '" + dp.PrecioUnitarioDlls + "' , '"
                                + dp.ImporteDlls + "'";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Detalle Pedido Actualizado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error " + exe;
            }
        }
        //Eliminar Detalle Pedido
        [Route("DeleteDetallePedido/{id}")]
        public string DeleteDetallePedido(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                              exec dtBorrarDetallePedido " + id;

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

        //Eliminar Detalle Pedido
        [Route("DeleteAllDetallePedido/{id}")]
        public string DeleteAllDetallePedido(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"
                              Delete DetallePedidos where IdPedido = " + id;

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

        [Route("SumaImporte/{id}")]
        public HttpResponseMessage GetSumaImporte(int id)
        {
            DataTable table = new DataTable();

            string query = @"SELECT sum(CAST(Importe AS float)) as importe, sum(CAST(ImporteDlls AS float)) as importeDlls FROM DetallePedidos where IdPedido=" +id;


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("Folio")]
        public HttpResponseMessage GetFolio()
        {
            DataTable table = new DataTable();

            string query = @"select MAX(Folio) as Folio from Pedidos";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("Vendedor")]
        public HttpResponseMessage GetVendedor()
        {
            DataTable table = new DataTable();

            string query = @"select * from Vendedor";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [Route("ProductoDetalleProducto/{ClaveProducto}/{Id}")]
        public HttpResponseMessage GetProductoDetalleProducto(String ClaveProducto, int Id)
        {
            DataTable table = new DataTable();

            string query = @"
                             exec jnProductoDetalleProducto '" + ClaveProducto + "',"+Id+";";


            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        //Editar Stock Product
        [Route("EditStockProducto/{id}/{stock}")]
        public string PutStockProducto(string id, string stock)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @" update Producto set Stock = '" + stock + "' where ClaveProducto = '" + id + "';";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Producto Stock Actualizado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error " + exe;
            }
        }



    }

}
