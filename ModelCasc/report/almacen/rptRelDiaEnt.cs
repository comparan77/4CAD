using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.report.almacen
{
    public class rptRelDiaEnt
    {
        public string Proveedor { get; set; }
        public string Referencia { get; set; }
        public DateTime Fecha_ingreso { get; set; }
        public string Code { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad_piezas { get; set; }
        public int Cantidad_tarimas { get; set; }
        public int Piezas_calidad { get; set; }
        public string Tipo_produccion { get; set; }
        public string Observaciones { get; set; }
    }
}
