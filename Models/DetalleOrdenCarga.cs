using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class DetalleOrdenCarga
    {
       public long IdDetalleOrdenCarga { get; set; }
       public int IdOrdenCarga { get; set; }
       public int IdTarima { get; set; }
    }
}