using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class TransporteCtrl
    {
        public static Transporte_condicion_cliente TransCondCliFill(int id_cliente, bool entrada, bool salida)
        {
            Transporte_condicion_cliente o = new Transporte_condicion_cliente()
            {
                Id_cliente = id_cliente,
                Entrada = entrada,
                Salida = salida
            };
            Transporte_condicion_clienteMng oMng = new Transporte_condicion_clienteMng() { O_Transporte_condicion_cliente = o };
            try
            {
                oMng.fillForOperation();
            }
            catch
            {
                throw;
            }
            return o;
        }

        public static int TransCondCliGetNumCond(int id_cliente, bool entrada, bool salida)
        {
            int NumCond = 0;
            Transporte_condicion_cliente o = new Transporte_condicion_cliente()
            {
                Id_cliente = id_cliente,
                Entrada = entrada,
                Salida = salida
            };
            Transporte_condicion_clienteMng oMng = new Transporte_condicion_clienteMng() { O_Transporte_condicion_cliente = o };
            try
            {
                NumCond = oMng.GetNumCond();
            }
            catch
            {
                throw;
            }
            return NumCond;
        }
    }
}
