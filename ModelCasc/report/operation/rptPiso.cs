using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.report.operation
{
    public class rptPiso
    {
        public string Folio_entrada { get; set; }

        public string Bodega { get; set; }
        public string Referencia { get; set; }
        public string Cuenta { get; set; }
        public DateTime FechaEntrada { get; set; }
        public int Pza_e { get; set; }
        public int Bto_e { get; set; }
        public int Tar_e { get; set; }
        public int Pza_s { get; set; }
        public int Bto_s { get; set; }
        public int Tar_s { get; set; }
        public int Pza_i { get; set; }
        public int Bto_i { get; set; }
        public int Tar_i { get; set; }
        
        public string Orden { get; set; }
        public string Codigo { get; set; }
        public string Mercancia { get; set; }
        public string Vendor { get; set; }
        public string Proveedor { get; set; }
        public int Bultos_recibidos { get; set; }
        public int Bultos_desglosados { get; set; }
        public int Piezas_recibidas { get; set; }
        public int Piezas_desglosadas { get; set; }
        public DateTime? Ultima_fecha_trabajo { get; set; }
        public int Pallets { get; set; }
        public int Piezas_maquiladas { get; set; }
        public int Bultos_maquilados { get; set; }
        public int Piezas_sobrante { get; set; }
        public int Piezas_no_maquiladas { get; set; }
    }
}
