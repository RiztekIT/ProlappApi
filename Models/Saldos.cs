using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Saldos
    {
      public int IdSaldos { get; set; }
      public int IdFactura { get; set; }
      public int SaldoPendiente { get; set; }
    }
}