using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class ValidarOC
    {
        public long idvalidarordencompra { get; set; }
        public long idordencompra { get; set; }
        public string folioordencompra { get; set; }
        public DateTime fechaenvio { get; set; }
        public string estatus { get; set; }
        public DateTime fechavalidacion { get; set; }
        public string token { get; set; }
    }
}