using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Privilegio
    {
        public long IdPrivilegio { get; set; }
        public long IdUsuairo { get; set; }
        public long IdProceso { get; set; }
    }
}