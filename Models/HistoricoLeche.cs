using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class HistoricoLeche
    {
        public long IdPrecio { get; set; }
        public string PrecioLeche { get; set; }
        public string VarianteDiaAnterior { get; set; }
        public DateTime FechaPrecio { get; set; }
    }
}