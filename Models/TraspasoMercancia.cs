using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class TraspasoMercancia
    {

        public long IdTraspasoMercancia { get; set; }
        public long Folio { get; set; }
        public long IdOrdenCarga { get; set; }
        public long FolioOrdenCarga { get; set; }
        public long IdCliente { get; set; }
        public string Cliente { get; set; }
        public string SacosTotales { get; set; }
        public string KilogramosTotales { get; set; }
        public DateTime FechaExpedicion { get; set; }
        public string Estatus { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string CampoExtra1 { get; set; }
        public string CampoExtra2 { get; set; }



    }
}