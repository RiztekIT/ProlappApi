
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
    [RoutePrefix("api/Empresa")]
    public class EmpresaController : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"select * from dbo.Empresa";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("EmpresaFoto")]
        public HttpResponseMessage GetEmpresaFoto()
        {
            DataTable table = new DataTable();

            string query = @"select Foto from Empresa";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        public string Put(Empresa empresa)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @"
                                exec etEditarEmpresa " + empresa.IdEmpresa + " , '" + empresa.RFC + "' , '" + empresa.RazonSocial + "' , '" + empresa.Calle + "' , " + empresa.NumeroInterior + " , "
                                    + empresa.NumeroExterior + " , " + empresa.CP + " , '" + empresa.Colonia + "' , '" + empresa.Ciudad + "' , '" + empresa.Estado + "' , '" + empresa.Pais + "' , '"
                                    + empresa.Regimen + @"'";

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
        [Route("EditarEmpresaFoto")]
        public string Putfoto(Empresa empresa)
        {
            try
            {


                DataTable table = new DataTable();

                string query = @"
                                  UPDATE EMPRESA SET Foto = '" + empresa.Foto + "';";


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
                return "Se produjo un error" + exe + empresa.Foto;

            }
        }
        public string Post(Empresa empresa)
        {
            try
            {


                DataTable table = new DataTable();
                string query = @"
                                Execute itInsertNuevaEmpresa '" + empresa.RFC + "' , '" + empresa.RazonSocial + "' , '" + empresa.Calle + "' , " + empresa.NumeroInterior + " , "  + empresa.NumeroExterior +  " , " + empresa.CP + " , '"
                                 + empresa.Colonia + "' , '" + empresa.Ciudad + "' , '" + empresa.Estado + "' , '" + empresa.Pais + "' , '"  + empresa.Regimen +  @" ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Prolapp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }



                return "Cliente Agregado";
            }
            catch (Exception exe)
            {
                return "Se produjo un error" + exe;
            }
        }
    }

   

     

}
