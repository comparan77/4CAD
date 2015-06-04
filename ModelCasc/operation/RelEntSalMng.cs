using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.operation
{
    public class RelEntSalMng
    {
        #region Campos
        private int _idBodega;
        private int _idCliente;
        private int _anio_ini;
        private int _dia_ini;
        private int _anio_fin;
        private int _dia_fin;
        #endregion

        #region Propiedades
        public int IdBodega { get { return _idBodega; } set { _idBodega = value; } }
        public int IdCliente { get { return _idCliente; } set { _idCliente = value; } }
        public int Anioini { get { return _anio_ini; } set { _anio_ini = value; } }
        public int Diaini { get { return _dia_ini; } set { _dia_ini = value; } }
        public int Aniofin { get { return _anio_fin; } set { _anio_fin = value; } }
        public int Diafin { get { return _dia_fin; } set { _dia_fin = value; } }
        #endregion

        public DataTable getDataEntrada()
        {
            DataTable dt = new DataTable();
            try
            {

                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZRelEntradas");

                if (this._anio_ini == 1)
                {
                    GenericDataAccess.AddInParameter(comm, "?P_anio_ini", DbType.Int32, DBNull.Value);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_ini", DbType.Int32, DBNull.Value);
                    GenericDataAccess.AddInParameter(comm, "?P_anio_fin", DbType.Int32, DBNull.Value);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_fin", DbType.Int32, DBNull.Value);
                }
                else
                {
                    GenericDataAccess.AddInParameter(comm, "?P_anio_ini", DbType.Int32, this._anio_ini);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_ini", DbType.Int32, this._dia_ini);
                    GenericDataAccess.AddInParameter(comm, "?P_anio_fin", DbType.Int32, this._anio_fin);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_fin", DbType.Int32, this._dia_fin);
                }

                if (this._idBodega != 0)
                    GenericDataAccess.AddInParameter(comm, "?P_id_bodega", DbType.Int32, this._idBodega);
                else
                    GenericDataAccess.AddInParameter(comm, "?P_id_bodega", DbType.Int32, DBNull.Value);

                if (this._idCliente != 0)
                    GenericDataAccess.AddInParameter(comm, "?P_id_cliente", DbType.Int32, this._idCliente);
                else
                    GenericDataAccess.AddInParameter(comm, "?P_id_cliente", DbType.Int32, DBNull.Value);

                dt = GenericDataAccess.ExecuteSelectCommand(comm);
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable getDataSalida()
        {
            DataTable dt = new DataTable();
            try
            {

                IDbCommand comm = GenericDataAccess.CreateCommandSP("sp_ZRelSalidas");

                if (this._anio_ini == 1)
                {
                    GenericDataAccess.AddInParameter(comm, "?P_anio_ini", DbType.Int32, DBNull.Value);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_ini", DbType.Int32, DBNull.Value);
                    GenericDataAccess.AddInParameter(comm, "?P_anio_fin", DbType.Int32, DBNull.Value);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_fin", DbType.Int32, DBNull.Value);
                }
                else
                {
                    GenericDataAccess.AddInParameter(comm, "?P_anio_ini", DbType.Int32, this._anio_ini);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_ini", DbType.Int32, this._dia_ini);
                    GenericDataAccess.AddInParameter(comm, "?P_anio_fin", DbType.Int32, this._anio_fin);
                    GenericDataAccess.AddInParameter(comm, "?P_dia_fin", DbType.Int32, this._dia_fin);
                }

                if (this._idBodega != 0)
                    GenericDataAccess.AddInParameter(comm, "?P_id_bodega", DbType.Int32, this._idBodega);
                else
                    GenericDataAccess.AddInParameter(comm, "?P_id_bodega", DbType.Int32, DBNull.Value);

                if (this._idCliente != 0)
                    GenericDataAccess.AddInParameter(comm, "?P_id_cliente", DbType.Int32, this._idCliente);
                else
                    GenericDataAccess.AddInParameter(comm, "?P_id_cliente", DbType.Int32, DBNull.Value);

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
