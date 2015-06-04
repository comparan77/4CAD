using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.report
{
    public class TransporteChartMng
    {
        #region Campos
        private string _sTransporte;
        private string _sCantidad;
        private List<string> _lstTransporte;
        private List<double> _lstCantidad;
        private int _mes;
        private int _anio;
        #endregion

        #region Propiedades
        public string STransporte { get { return _sTransporte; } private set { _sTransporte = value; } }
        public string SCantidad { get { return _sCantidad; } private set { _sCantidad = value; } }
        public int Anio { get { return _anio; } set { _anio = value; } }
        public int Mes { get { return _mes; } set { _mes = value; } }
        #endregion

        #region Metodos

        public void fillSeries()
        {
            StringBuilder sbTransporte = new StringBuilder();
            StringBuilder sbCantidad = new StringBuilder();
            try
            {
                fillData();

                foreach (string transporte in this._lstTransporte)
                {
                    sbTransporte.Append(transporte);
                    sbTransporte.Append("|");
                }
                STransporte = sbTransporte.ToString().Substring(0, sbTransporte.Length - 1);

                foreach (double cantidad in this._lstCantidad)
                {
                    sbCantidad.Append(cantidad.ToString());
                    sbCantidad.Append(",");
                }
                SCantidad = sbCantidad.ToString().Substring(0, sbCantidad.Length - 1);
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            catch
            {
                throw;
            }
        }

        private void fillData()
        {
            try
            {
                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZTransporteChart");
                GenericDataAccess.AddInParameter(comm, "?P_opcion", DbType.Int32, 1);
                GenericDataAccess.AddInParameter(comm, "?P_anio", DbType.Int32, this._anio);
                GenericDataAccess.AddInParameter(comm, "?P_mes", DbType.Int32, this._mes);
                DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

                double doble = 0;
                this._lstCantidad = new List<double>();
                this._lstTransporte = new List<string>();

                foreach (DataRow dr in dt.Rows)
                {
                    this._lstTransporte.Add(dr["nombre"].ToString().Replace("|", " "));
                    if(dr["Cantidad"]!=null)
                    {
                        double.TryParse(dr["Cantidad"].ToString(), out doble);
                    }
                    this._lstCantidad.Add(doble);
                    doble = 0;
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
