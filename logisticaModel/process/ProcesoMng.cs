using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using logisticaModel.catalog;

namespace logisticaModel.process
{
    internal class ProcesoMng
    {
        public static void procesarProforma(Cliente o)
        {
            IDbCommand comm = null;
            try
            {
                foreach (DateTime day in CommonFunctions.EachDay(new DateTime(2017, 12, 1), DateTime.Now))
                {

                    comm = GenericDataAccess.CreateCommandSP("sp_Calc_serv_unico_entrada");
                    GenericDataAccess.AddInParameter(comm, "?P_fecha", DbType.Date, day);
                    GenericDataAccess.ExecuteSelectCommand(comm);

                    comm = GenericDataAccess.CreateCommandSP("sp_Calc_serv_personalizado");
                    GenericDataAccess.AddInParameter(comm, "?P_fecha", DbType.Date, day);
                    GenericDataAccess.ExecuteSelectCommand(comm);

                }
            }
            catch
            {
                throw;
            }
        }
    }
}
