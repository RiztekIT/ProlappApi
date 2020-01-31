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
                                + pedido.Factura + "' , '" + pedido.LugarDeEntrega + "' , '" + pedido.Moneda + @"'
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Pedido Agregad";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }


    }
}
