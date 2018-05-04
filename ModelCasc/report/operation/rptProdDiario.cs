using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.report.operation
{
    public class rptProdDiario
    {
        public string Bodega { get; set; }
        public string Cuenta { get; set; }
        public string Cliente { get; set; }
        public string Ot { get; set; }
        public string Pedimento { get; set; }
        public string Trafico { get; set; }
        public string Proceso { get; set; }

        public int Pza_sol { get; set; }
        public int Maq { get; set; }
        public int Sin_proc { get; set; }
        public int Bultos { get; set; }
        public int Pallets { get; set; }
    }
}
