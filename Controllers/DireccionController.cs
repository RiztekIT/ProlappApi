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
    [RoutePrefix("api/Direccion")]
    public class DireccionController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from PrecioLeche order by FechaPrecio DESC";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("OrdenTemporal/{fecha}")]
        public string Post(PrecioLeche pl)
        {
            try
            {

                DataTable table = new DataTable();

                DateTime time = pl.FechaPrecio;
                string format = "yyyy-MM-dd";

                string query = @"
                                exec itInsertNuevaOrdenTemporal " + pl.IdPrecio + " , " + pl.PrecioLeche + " , " + time.ToString(format) +
                                " , '" + pl.FechaPrecio + @"'";

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

        //obtener PrecioLeche de dia especifico
        [Route("OrdenTemporal/{fecha}")]
        public HttpResponseMessage Get(DateTime Fecha)
        {
            DataTable table = new DataTable();

            string query = @"select PrecioLeche from PrecioLeche where FechaPrecio =" + Fecha + ";";

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