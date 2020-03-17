using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class DetalleOrdenDescarga
    {

        public long IdDetalleOrdenDescarga { get; set; }
        public int IdOrdenDescarga { get; set; }
        public int IdTarima { get; set; }
    }
}