using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class TraspasoTarima
    {

        public long IdTraspasoTarima { get; set; }
        public string Cantidad { get; set; }
        public string QRorigen { get; set; }
        public string QRdestino { get; set; }

    }
}