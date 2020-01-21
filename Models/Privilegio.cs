using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Privilegio
    {
        public long IdPrivilegio { get; set; }
        public long IdUsuario { get; set; }
        public long IdProceso { get; set; }
    }
}