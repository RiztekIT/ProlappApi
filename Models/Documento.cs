using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Documento
    {
        public long IdDocumento { get; set; }
        public long Folio { get; set; }
        public string Tipo { get; set; }
        public string ClaveProducto { get; set; }
        public string NombreDocumento { get; set; }
        public string Path { get; set; }
        public string Observaciones { get; set; }
        public DateTime Vigencia { get; set; }


    }
}