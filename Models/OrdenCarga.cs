using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class OrdenCarga
    {
        public long IdOrdenCarga { get; set; }
        public int NumSalida { get; set; }
        public DateTime FechaOrden { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public string Fletera { get; set; }
        public string NumCaja { get; set; }
        public string EnviarA { get; set; }
        public string Sacos { get; set; }
        public string Kilos { get; set; }
        public string Notas { get; set; }
        public string Usuario { get; set; }

    }
}