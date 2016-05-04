using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.report.almacen
{
    public class rptInvTotDia
    {
        public DateTime Fecha { get; set; }
        public string Codigo { get; set; }
        public string Pallet { get; set; }
        public string Descripcion { get; set; }
        public int Cajas { get; set; }
        public int Piezas { get; set; }
        public int Resto { get; set; }
        public int Total_piezas { get; set; }
        public string Tarima { get; set; }
        public string Tipo { get; set; }
        public string Rr { get; set; }
        public string Ubicacion { get; set; }
    }
}
