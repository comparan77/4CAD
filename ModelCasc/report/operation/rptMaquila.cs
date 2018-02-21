using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.report.operation
{
    public class rptMaquila
    {
        public DateTime FechaEntrada { get; set; }
        public string Folio_entrada { get; set; }
        public string Referencia { get; set; }
        public string Folio_ot { get; set; }
        public DateTime Fecha_ot { get; set; }
        public string Ref_cte { get; set; }
        public string Supervisor { get; set; }
        public string Servcio { get; set; }
        public string Etiqueta { get; set; }
        public string Ref_serv { get; set; }
        public int Pzas_sol { get; set; }
        public string Fecha_maq { get; set; }
        public int Piezas_maq { get; set; }
        public int Bultos_maq { get; set; }
        public int Pallets_maq { get; set; }
    }
}
