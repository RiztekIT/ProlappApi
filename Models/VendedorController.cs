using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProlappApi.Models
{
    public class VendedorController : ApiController
    {
        public long IdVendedor { get; set; }
        public string Nombre { get; set; }
    }
}
