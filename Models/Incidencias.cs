using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Incidencias
    {
        public long IdIncidencia { get; set; }
        public long Folio { get; set; }
        public string TipoIncidencia { get; set; }
        public string Procedencia { get; set; }
        public long IdDetalle { get; set; }
        public string Cantidad { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaElaboracion { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public string Observaciones { get; set; }
    }
}