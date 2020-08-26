using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Pagos
    {

        public long IdPago { get; set; }
        public long Folio { get; set; }
        public long FolioDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string Cantidad { get; set; }
        public string CuentaOrigen { get; set; }
        public string CuentaDestino { get; set; }
        public DateTime FechaPago { get; set; }
        public string Observaciones { get; set; }

    }
}