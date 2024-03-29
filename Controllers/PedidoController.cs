﻿using System;
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

            string query = @"select * from pedidos where Estatus<>'Borrado' and idPedido =" + id;

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

            string query = @"Select Pedidos.*, Cliente.* from Pedidos LEFT JOIN Cliente ON Pedidos.IdCliente = Cliente.IdClientes where Pedidos.Estatus<>'Borrado' order by FechaDeExpedicion desc ;";

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
                DateTime time4 = pedido.FechaDeExpedicion;
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
                                + pedido.SubtotalDlls + "' , '" + pedido.DescuentoDlls + "' , '" + pedido.TotalDlls + "' , '" + pedido.Flete + "' , " + pedido.IdDireccion + " ,'" + time4.ToString(format) + "';";

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
                DateTime time4 = pedido.FechaDeExpedicion;
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
                                + pedido.SubtotalDlls + "' , '" + pedido.DescuentoDlls + "' , '" + pedido.TotalDlls + "' , '" + pedido.Flete + "' , " + pedido.IdDireccion + " ,'" + time4.ToString(format) + @"'
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
                string query = @"insert into DetallePedidos (IdPedido, ClaveProducto, Producto, Unidad, PrecioUnitario, Cantidad, Importe, Observaciones, TextoExtra, PrecioUnitarioDlls, ImporteDlls)
values (" + dp.IdPedido + " , '" + dp.ClaveProducto + "' , '"
                                + dp.Producto + "' , '" + dp.Unidad + "' , '"
                                + dp.PrecioUnitario + "' , '" + dp.Cantidad + "' , '"
                                + dp.Importe + "' , '" + dp.Observaciones + "' , '" + dp.TextoExtra + "'  ,  '"
                                + dp.PrecioUnitarioDlls + "' , '" + dp.ImporteDlls + "')";
              

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

        [Route("ValidarOC/{token}")]
        public HttpResponseMessage GetValidarOC(string token)
        {
            DataTable table = new DataTable();

            string query = @"select pedidos.*, validarordencompra.* from pedidos left join validarordencompra on pedidos.IdPedido = validarordencompra.idordencompra where validarordencompra.token = '" + token + "'";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("OrdenCarga/{id}")]
        public HttpResponseMessage GetOC(string id)
        {
            DataTable table = new DataTable();

            string query = @"update OrdenCarga set estatus='Creada' where idPedido= " + id + "";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ValidarOC")]
        public string PostValidarOC(ValidarOC validaroc)
        {
            try
            {


                DataTable table = new DataTable();
                //Las variables de fecha, son igualadas a un valor Datatime
                DateTime time2 = validaroc.fechaenvio;
                DateTime time3 = validaroc.fechavalidacion;
                
                //Al momento de insertar los valores de las fechas, estan seran insertadas con el formato 'Format'
                string format = "yyyy-MM-dd HH:mm:ss";
                //De esta manera no causara error al tratar de insertar fechas en la base de datos SQL
                //time.ToString(format)
                string query = @"insert into validarordencompra values("+validaroc.idordencompra+",'"+ validaroc .folioordencompra+ "','"+ time2.ToString(format) + "','"+ validaroc.estatus+ "','"+ time3.ToString(format) + "','"+ validaroc.token+ "')";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Validador Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }

        [Route("ValidarOC")]
        public string PutValidarOC(ValidarOC validaroc)
        {
            try
            {


                DataTable table = new DataTable();
                //Las variables de fecha, son igualadas a un valor Datatime
                DateTime time2 = validaroc.fechaenvio;
                DateTime time3 = validaroc.fechavalidacion;

                //Al momento de insertar los valores de las fechas, estan seran insertadas con el formato 'Format'
                string format = "yyyy-MM-dd HH:mm:ss";
                //De esta manera no causara error al tratar de insertar fechas en la base de datos SQL
                //time.ToString(format)
                string query = @"update validarordencompra set idordencompra = " + validaroc.idordencompra + ", folioordencompra='" + validaroc.folioordencompra + "', fechaenvio='" + time2.ToString(format) + "', estatus='" + validaroc.estatus + "',fechavalidacion='" + time3.ToString(format) + "',token='" + validaroc.token + "')";

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


        // ******* PEDIDO INFO ***** //

        //Informacion Adicional a Pedido info.

        //GET PEDIDO INFO
        [Route("GetPedidoInfoId/{id}")]
        public HttpResponseMessage GetPedidoInfoId(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from PedidosInfo where IdPedidoInfo = " + id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("GetPedidoInfoIdPedido/{id}")]
        public HttpResponseMessage GetPedidoInfoIdPedido(int id)
        {
            DataTable table = new DataTable();

            string query = @"select * from PedidosInfo where IdPedido = " + id ;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //POST PEDIDO INFO
        [Route("AddPedidoInfo")]
        public string PostPedidoInfo(PedidoInfo pi)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @"insert into PedidosInfo (IdPedido, SeleccionManual, Campo1, Campo2, Campo3) values("+pi.IdPedido+",'"+pi.SeleccionManual+"','"+pi.Campo1+"','"+pi.Campo2+"','"+pi.Campo3+"')" + @"";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Agregado Correctamente";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }

        //PUT PEDIDO INFO

        [Route("EditPedidoInfo")]
        public string PutPedidoInfo(PedidoInfo pi)
        {
            try
            {


                DataTable table = new DataTable();
               
                string query = @"update PedidosInfo set IdPedido = "+pi.IdPedido+", SeleccionManual = '"+pi.SeleccionManual+"', Campo1 = '"+pi.Campo1+"', Campo2 = '"+pi.Campo2+"', Campo3 = '"+pi.Campo3+"' where IdPedido ="+pi.IdPedido + @"";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Actualizado Correctamente";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }
        //DELETE PEDIDO INFO

        // ******* PEDIDO INFO ***** //
        [Route("DeletePedidoInfo/{id}")]
        public string DeletePedidoInfo(int id)
        {
            try
            {


                DataTable table = new DataTable();


                string query = @"delete PedidosInfo where IdPedido = " + id;

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



    }






}
