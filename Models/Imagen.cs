using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Imagenes
    {

        public long IdImagen {get; set;}
        public long Folio { get; set; }
        public string Tipo { get; set; }
        public string Imagen { get; set; }
        public string Path { get; set; }
    }
}