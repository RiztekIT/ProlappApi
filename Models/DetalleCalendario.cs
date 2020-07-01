using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class DetalleCalendario
    {
        public int IdDetalleCalendario { set; get; }
        public int IdCalendario { set; get; }
        public int Folio { set; get; }
        public string Documento { set; get; }
        public string Descripcion { set; get; }
        public DateTime Start { set; get; }
        public DateTime Endd { set; get; }
        public string Title { set; get; }
        public string Color { set; get; }
        public int AllDay { set; get; }
        public int ResizableBeforeStart { set; get; }
        public int ResizableBeforeEnd { set; get; }
        public int Draggable { set; get; }
    }
}