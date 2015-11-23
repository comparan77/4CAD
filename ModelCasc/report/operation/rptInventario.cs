using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.report.operation
{
    public class rptInventario
    {
        public string Codigo_cliente { get; set; }
        public int Aduana { get; set; }
        public string Referencia { get; set; }
        public string Ubicacion { get; set; }
        public DateTime FechaEntrada { get; set; }
        public int Piezas_pedimento { get; set; }
        public int Bultos_pedimento { get; set; }
        public string Vendor { get; set; }
        public string Proveedor { get; set; }
        public string Orden { get; set; }
        public string Codigo { get; set; }
        public string Mercancia { get; set; }
        public string Factura { get; set; }
        public decimal Costo_unitario { get; set; }
        public int Piezas_maquiladas { get; set; }
        public int Piezas_en_proceso { get; set; }
        public int Saldo_bultos { get; set; }

        public int Piezas_inventario { get; set; }
        public decimal Valor_inventario { get; set; } 
    }
}
