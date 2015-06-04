using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.report
{
    public class GeneralChartMng
    {
        #region Campos

        private double _sBultoEntrada;
        private double _sBultoSalida;
        private double _sPiezaEntrada;
        private double _sPiezaSalida;

        #endregion

        #region Propiedades

        public double SBultoEntrada { get { return _sBultoEntrada; } private set { _sBultoEntrada = value; } }
        public double SBultoSalida { get { return _sBultoSalida; } private set { _sBultoSalida = value; } }
        public double SPiezaEntrada { get { return _sPiezaEntrada; } private set { _sPiezaEntrada = value; } }
        public double SPiezaSalida { get { return _sPiezaSalida; } private set { _sPiezaSalida = value; } }

        #endregion

        #region Metodos

        public void fillSeries(int AnioOperacion, int MesOpearcion, int IdCliente)
        {
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZGeneralChart");
                GenericDataAccess.AddInParameter(comm, "?P_opcion", DbType.Int32, 1);
                GenericDataAccess.AddInParameter(comm, "?P_anioOperacion", DbType.Int32, AnioOperacion);
                GenericDataAccess.AddInParameter(comm, "?P_mesOperacion", DbType.Int32, MesOpearcion);
                GenericDataAccess.AddInParameter(comm, "?P_id_cliente", DbType.Int32, IdCliente);
                DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

                double doble = 0;

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["sBultoEntrada"] != DBNull.Value)
                    {
                        double.TryParse(dr["sBultoEntrada"].ToString(), out doble);
                        this.SBultoEntrada = doble;
                        doble = 0;
                    }
                    if (dr["sBultoSalida"] != DBNull.Value)
                    {
                        double.TryParse(dr["sBultoSalida"].ToString(), out doble);
                        this.SBultoSalida = doble;
                        doble = 0;
                    }
                    if (dr["SPiezaEntrada"] != DBNull.Value)
                    {
                        double.TryParse(dr["SPiezaEntrada"].ToString(), out doble);
                        this.SPiezaEntrada = doble;
                        doble = 0;
                    }
                    if (dr["SPiezaSalida"] != DBNull.Value)
                    {
                        double.TryParse(dr["SPiezaSalida"].ToString(), out doble);
                        this.SPiezaSalida = doble;
                        doble = 0;
                    }
                }
            }
            catch 
            {
                throw;
            }
        }

        #endregion
    }
}
