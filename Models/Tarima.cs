using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Tarima
    {
        public long IdTarima { get; set; }
        public string Sacos { get; set; }
        public string PesoTotal { get; set; }
        public string QR { get; set; }
    }
}