using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.report
{
    public class ChartJs
    {
        public string Title { get; set; }
        public ChartJsData Data { get; set; }
        public int Opcion { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; } 
        public int Id_cliente { get; set; }
        public int Id_bodega { get; set; }
    }
}
