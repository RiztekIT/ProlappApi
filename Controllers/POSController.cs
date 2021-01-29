using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ProlappApi.Controllers
{
    public class Query
    {
        public string consulta { get; set; }
    }
    [RoutePrefix("api/POS")]
    public class POSController : ApiController
    {
        [Route("consulta")]
        public HttpResponseMessage PostServicios(Querys consulta)
        {
            DataTable table = new DataTable();

            string query = @"" + consulta.consulta + "";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Lacteozone"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
            //return consulta;
        }
    }
}
