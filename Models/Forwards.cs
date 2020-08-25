using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Forwards
    {
        public int IdForward { get; set; }
        public DateTime FechaCierre { get; set; }
        public DateTime FechaPago { get; set; }
        public string CantidadDlls { get; set; }
        public string TipoCambio { get; set; }
        public string CantidadMXN { get; set; }
        public string Garantia { get; set; }
        public string GarantiaPagada { get; set; }
        public string CantidadPendiente { get; set; }
        public string Destino { get; set; }
        public string Promedio { get; set; }
        public string Estatus { get; set; }
    }
}