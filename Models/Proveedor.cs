﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Proveedor
    {
        public long IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string CP { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string NumeroInterior { get; set; }
        public string NumeroExterior { get; set; }
        public string ClaveProveedor { get; set; }
        public string Estatus { get; set; }
        public string LimiteCredito { get; set; }
        public string DiasCredito { get; set; }
        public string MetodoPago { get; set; }
        public string UsoCFDI { get; set; }

    }
}