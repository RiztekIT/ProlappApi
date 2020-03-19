using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class OrdenCarga
    {
        public long IdOrdenCarga { get; set; }
        public int Folio { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public int IdPedido { get; set; }
        public string Fletera { get; set; }
        public string Caja { get; set; }
        public string Sacos { get; set; }
        public string Kg { get; set; }
        public string Chofer { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Observaciones { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaInicioCarga { get; set; }
        public DateTime FechaFinalCarga { get; set; }
        public DateTime FechaExpedicion { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }


    }
}