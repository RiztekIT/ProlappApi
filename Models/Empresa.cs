﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Empresa
    {
        public long IdEmpresa { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public string Calle { get; set; }
        public int NumeroInterior { get; set; }
        public int NumeroExterior { get; set; }
        public int CP { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Regimen { get; set; }
        public string Foto { get; set; }
    }
}