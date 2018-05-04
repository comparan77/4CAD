using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.report.operation
{
    public class rptPartNom
    {
        public DateTime Fecha { get; set; }
        public string Bodega { get; set; }
        public string Cuenta { get; set; }
        public string Cliente { get; set; }
        public string Ref_entrada { get; set; }

        public int Pza_tot { get; set; }
        public int Nom { get; set; }
    }
}
