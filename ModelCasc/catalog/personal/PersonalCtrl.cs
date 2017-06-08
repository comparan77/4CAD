using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog.personal
{
    public class PersonalCtrl
    {
        public static void PersonalRegistro(Personal o)
        {
            try
            {
                PersonalMng oPMng = new PersonalMng() { O_Personal = o };
                oPMng.selByFolio();
                Personal_registroMng oMng = new Personal_registroMng()
                {
                    O_Personal_registro = new Personal_registro()
                    {
                        Id_personal = o.Id,
                        Id_bodega = o.Id_bodega
                    }
                };
                oMng.add();
            }
            catch
            {
                throw;
            }
        }

        public static Personal PersonalUltimoRegistroPorBodega(int id_bodega)
        {
            Personal oP = new Personal();
            try
            {
                Personal_registro oPR = new Personal_registro() { Id_bodega = id_bodega };
                Personal_registroMng oPRMng = new Personal_registroMng() { O_Personal_registro = oPR };
                oPRMng.selUltPorBodega();

                oP = new Personal() { Id = oPR.Id_personal };
                PersonalMng oPMng = new PersonalMng() { O_Personal = oP };
                oPMng.selById();
                oP.PerReg = oPR;
            }
            catch
            {
                throw;
            }
            return oP;
        }
    }
}
