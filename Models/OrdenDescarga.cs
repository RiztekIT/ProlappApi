using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class OrdenDescarga
    {

        public long IdOrdenDescarga { get; set; }
        public int Folio { get; set; }
        public DateTime FechaLlegada { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string PO { get; set; }
        public string Fletera { get; set; }
        public string Caja { get; set; }
        public string Sacos { get; set; }
        public string Kg { get; set; }
        public string Chofer { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Observaciones { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaInicioDescarga { get; set; }
        public DateTime FechaFinalDescarga { get; set; }
        public DateTime FechaExpedicion { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }

    }
}