using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.report
{
    public class RptMaquila
    {
        public static DataTable getAll()
        {
            DataTable dt = new DataTable();
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZMaquila");
                GenericDataAccess.AddInParameter(comm, "?P_opcion", DbType.Int32, 0);
                dt = GenericDataAccess.ExecuteSelectCommand(comm);
            }
            catch
            {
                throw;
            }
            return dt;
        }
    }
}
