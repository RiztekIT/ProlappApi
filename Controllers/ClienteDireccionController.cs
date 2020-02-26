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
    [RoutePrefix("api/ClienteDireccion")]
    public class ClienteDireccionController : ApiController
    {


        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"Select * from DireccionesCliente";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(ClienteDireccion Cd)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"
                                INSERT INTO DireccionesCliente (IdCliente, Calle, Colonia, CP, Ciudad, Estado, NumeroInterior, NumeroExterior)
                                VALUES (" + Cd.IdCliente + " , '" + Cd.Calle + "', '" + Cd.Colonia + "', '" + Cd.CP + "', '" + Cd.Ciudad + "', '" + Cd.Estado + "', '" + Cd.NumeroInterior + "', '" + Cd.NumeroExterior + "')" +
                                ";";
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Direccion Cliente Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }

        public string Put(ClienteDireccion dc)
        {
            try
            {

                DataTable table = new DataTable();

                string query = @"
                                update DireccionesCliente set IdCliente = " + dc.IdCliente + ", Calle = '" + dc.Calle + "' , Colonia = '" + dc.Colonia + "' , CP = '" + dc.CP + 
                                "' , Ciudad = '" + dc.Ciudad + "' , Estado = '" + dc.Estado + "' , NumeroInterior = '" + dc.NumeroInterior + "' , NumeroExterior = '" + dc.NumeroExterior + 
                                "' where IdDireccion = " + dc.IdDireccion + ";";

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

        public string Delete(int id)
        {
            try
            {

                DataTable table = new DataTable();


                string query = @"
                              Delete DireccionesCliente where IdDireccion = " + id + ";";

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

        //Obtener Direcciones por IdCliente
        [Route("DireccionIdCliente/{id}")]
        public HttpResponseMessage GetDireccionIdCliente(int id)
        {
            DataTable table = new DataTable();

            string query = @"Select * from DireccionesCliente where IdCliente = " + id + ";";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        //JOIN DIRECCION CLIENTE CON CLIENTE
        [Route("JoinDireccionCliente")]
        public HttpResponseMessage GetJoinDireccionCliente()
        {
            DataTable table = new DataTable();

            string query = @"select * from DireccionesCliente left join Cliente on Cliente.IdClientes = DireccionesCliente.IdCliente";

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
