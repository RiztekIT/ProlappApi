﻿
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
    [RoutePrefix("api/Proceso")]
    public class ProcesoController : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select Area from Procesos";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ProcesoPrivilegio")]
        public HttpResponseMessage GetProcesoPrivilegio()
        {
            DataTable table = new DataTable();

            string query = @"	select distinct Area from Procesos";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ProcesoArea")]
        public HttpResponseMessage GetProcesoArea()
        {
            DataTable table = new DataTable();

            //string query = @" exec jnAreasUsuario " + id;
            string query = @"	select distinct Area from Procesos";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [Route("PermisosUsuario")]
        public HttpResponseMessage GetPermisosUsuario()
        {
            DataTable table = new DataTable();

            string query = @" select IdUsuario, Nombre, NombreUsuario, Correo, Telefono from Usuario ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ProcesoNombre/{areas}/{id}")]
        public HttpResponseMessage GetProcesoNombre(string areas, int id)
        {
            DataTable table = new DataTable();

            //string query = @" select NombreProceso from procesos where Area = '" + areas + "';";
            string query = @" select * from procesos left join privilegios on Privilegios.IdProcesos=Procesos.idprocesos and Privilegios.IdUsuario="+ id +" where area= '" + areas + "';";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("ProcesoIdProceso/{id}")]
        public HttpResponseMessage GetProcesoIdProceso(int id)
        {
            DataTable table = new DataTable();  

            string query = @"select * from Procesos where IdProcesos ="+ id;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("PermisoDelete/{id}/{id1}")]
        public string Delete(int id, int id1)
        {
            DataTable table = new DataTable();

            string query = @" delete from privilegios where IdUsuario=" + id + " and idprocesos= " + id1 + ";";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return "Se elimino correctamente";
        }

        [Route("PermisoPost/{id}/{id1}")]
        public string Post(int id, int id1)
        {
            try
            {

                DataTable table = new DataTable();
                string query = @"
                                Execute itInsertNuevoPermiso '" + id + "' , '" + id1 + @"'
                                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Permiso Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }


    }
}
