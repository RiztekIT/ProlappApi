using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Bodegas
    {
        public long IdBodega { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Origen { get; set; }
    }
}