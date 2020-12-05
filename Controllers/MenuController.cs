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
    [RoutePrefix("api/Menu")]
    public class MenuController : ApiController
    {
        [Route("{id}")]
        public HttpResponseMessage Getusuario(int id)
        {
            DataTable table = new DataTable();

            //string query = @"select * from menu order by titulo";
            string query = @"select menu.* from menu where menu.titulo in (select procesos.Area from privilegios left join procesos on privilegios.idprocesos=procesos.idprocesos where idusuario="+id+") order by titulo";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("Submenu/{iduser}/{idmenu}/{area}")]
        public HttpResponseMessage GetSubmenu(int iduser, int idmenu, string area)
        {
            DataTable table = new DataTable();

            //string query = @"select * from submenu where idmenu="+id;
            string query = @"select * from submenu where submenu.titulo in (select procesos.Modulo from privilegios left join procesos on privilegios.idprocesos=procesos.idprocesos where idusuario="+iduser+ " and procesos.Area = '"+area+"') and idmenu=" + idmenu+ " order by orden";

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
