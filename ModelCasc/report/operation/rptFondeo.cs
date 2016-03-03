using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.report.operation
{
    public class rptFondeo
    {
        public DateTime Fecha { get; set; }
        public string Referencia { get; set; }
        public string Factura { get; set; }
        public string Orden { get; set; }
        public string Codigo { get; set; }
        public string Vendor { get; set; }
        public int Piezas { get; set; }
        public double ValorFactura { get; set; }
    }
}
