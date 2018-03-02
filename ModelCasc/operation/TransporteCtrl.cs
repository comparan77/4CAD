using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;

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

        public static List<Transporte_condicion> TransCondByTransporteTipo(int id_transporte_tipo, bool entrada, bool salida)
        {
            List<Transporte_condicion> lst = new List<Transporte_condicion>();
            try
            {
                Transporte_condicion_transporte_tipoMng oMng = new Transporte_condicion_transporte_tipoMng()
                {
                    O_Transporte_condicion_transporte_tipo = new Transporte_condicion_transporte_tipo()
                    {
                        Id_transporte_tipo = id_transporte_tipo,
                        Entrada = entrada,
                        Salida = salida
                    }
                };
                oMng.fillLstByTransporteTipo();
                Transporte_condicionMng oTCMng = new Transporte_condicionMng();
                Transporte_condicion_categoriaMng oTCCMng = new Transporte_condicion_categoriaMng();
                foreach (Transporte_condicion_transporte_tipo itemTCTT in oMng.Lst)
                {
                    Transporte_condicion oTC = new Transporte_condicion() { Id = itemTCTT.Id_transporte_condicion };
                    oTCMng.O_Transporte_condicion = oTC;
                    oTCMng.selById();
                    if (oTC.Id_transporte_condicion_categoria != null)
                    {
                        Transporte_condicion_categoria oTCC = new Transporte_condicion_categoria() { Id = (int)oTC.Id_transporte_condicion_categoria };
                        oTCCMng.O_Transporte_condicion_categoria = oTCC;
                        oTCCMng.selById();
                        oTC.PTransCondCat = oTCC;
                    }
                    itemTCTT.PTransCond = oTC;
                    lst.Add(oTC);
                }
                
            }
            catch 
            {
                throw;
            }
            return lst;
        }
    }
}
