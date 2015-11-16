using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.report.operation
{
    public class rptRemision
    {
        public DateTime Fecha_remision { get; set; }
        public string Folio_remision { get; set; }
        public string Aduana { get; set; }
        public string Referencia { get; set; }
        public string Orden { get; set; }
        public string Codigo { get; set; }
        public string Mercancia { get; set; }
        public string Vendor { get; set; }
        public string Proveedor { get; set; }
        public string Negocio { get; set; }
        public int Bultos { get; set; }
        public int Piezas { get; set; }
        public DateTime? Fecha_recibido { get; set; }
        public string Etiqueta_rr { get; set; }
        public DateTime? Fecha_salida { get; set; }
        public string Folio_salida { get; set; }
    }
}
