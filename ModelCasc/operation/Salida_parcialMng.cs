using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.operation
{
    internal class Salida_parcialMng : dbTable
    {
        #region Campos
        protected Salida_parcial _oSalida_parcial;
        protected List<Salida_parcial> _lst;
        #endregion

        #region Propiedades
        public Salida_parcial O_Salida_parcial { get { return _oSalida_parcial; } set { _oSalida_parcial = value; } }
        public List<Salida_parcial> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Salida_parcialMng()
        {
            this._oSalida_parcial = new Salida_parcial();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida_parcial.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_salida", DbType.Int32, this._oSalida_parcial.Id_salida);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oSalida_parcial.Id_usuario);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_no_salida", DbType.Int32, this._oSalida_parcial.No_salida);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oSalida_parcial.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_es_ultima", DbType.Boolean, this._oSalida_parcial.Es_ultima);
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_parcial");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_parcial>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_parcial o = new Salida_parcial();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_salida"].ToString(), out entero);
                        o.Id_salida = entero;
                        entero = 0;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        o.Id_usuario = entero;
                        entero = 0;
                    }
                    if (dr["no_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_salida"].ToString(), out entero);
                        o.No_salida = entero;
                        entero = 0;
                    }
                    o.Referencia = dr["referencia"].ToString();
                    if (dr["es_ultima"] != DBNull.Value)
                    {
                        bool.TryParse(dr["es_ultima"].ToString(), out logica);
                        o.Es_ultima = logica;
                        logica = false;
                    }
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public override void selById()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_parcial");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_salida"].ToString(), out entero);
                        this._oSalida_parcial.Id_salida = entero;
                        entero = 0;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        this._oSalida_parcial.Id_usuario = entero;
                        entero = 0;
                    }
                    if (dr["no_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_salida"].ToString(), out entero);
                        this._oSalida_parcial.No_salida = entero;
                        entero = 0;
                    }
                    this._oSalida_parcial.Referencia = dr["referencia"].ToString();
                    if (dr["es_ultima"] != DBNull.Value)
                    {
                        bool.TryParse(dr["es_ultima"].ToString(), out logica);
                        this._oSalida_parcial.Es_ultima = logica;
                        logica = false;
                    }
                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad");
                else
                    throw new Exception("No existe información para el registro solicitado");
            }
            catch
            {
                throw;
            }
        }

        public override void add()
        {
            throw new NotImplementedException();
        }

        public void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_parcial");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oSalida_parcial.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
                this._oSalida_parcial.No_salida = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_no_salida"));
            }
            catch
            {
                throw;
            }
        }

        public override void udt()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_parcial");
                addParameters(3);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public override void dlt()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_parcial");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void fillLstByUsuario()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_parcial");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_parcial>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_parcial o = new Salida_parcial();
                    //int.TryParse(dr["id"].ToString(), out entero);
                    //o.Id = entero;
                    //entero = 0;
                    if (dr["id_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_salida"].ToString(), out entero);
                        o.Id_salida = entero;
                        entero = 0;
                    }
                    //if (dr["id_usuario"] != DBNull.Value)
                    //{
                    //    int.TryParse(dr["id_usuario"].ToString(), out entero);
                    //    o.Id_usuario = entero;
                    //    entero = 0;
                    //}
                    if (dr["no_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_salida"].ToString(), out entero);
                        o.No_salida = entero;
                        entero = 0;
                    }
                    o.Referencia = dr["referencia"].ToString();
                    //if (dr["es_ultima"] != DBNull.Value)
                    //{
                    //    bool.TryParse(dr["es_ultima"].ToString(), out logica);
                    //    o.Es_ultima = logica;
                    //    logica = false;
                    //}
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public void getNumSalidaByReferencia()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_parcial");
                addParameters(7);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["no_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_salida"].ToString(), out entero);
                        this._oSalida_parcial.No_salida = entero;
                        entero = 0;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void selByIdSalida()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_parcial");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    int.TryParse(dr["id"].ToString(), out entero);
                    this._oSalida_parcial.Id = entero;
                    entero = 0;
                    if (dr["id_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_salida"].ToString(), out entero);
                        this._oSalida_parcial.Id_salida = entero;
                        entero = 0;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        this._oSalida_parcial.Id_usuario = entero;
                        entero = 0;
                    }
                    if (dr["no_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_salida"].ToString(), out entero);
                        this._oSalida_parcial.No_salida = entero;
                        entero = 0;
                    }
                    this._oSalida_parcial.Referencia = dr["referencia"].ToString();
                    if (dr["es_ultima"] != DBNull.Value)
                    {
                        logica = Convert.ToBoolean(dr["es_ultima"]);
                        //bool.TryParse(dr["es_ultima"].ToString(), out logica);
                        this._oSalida_parcial.Es_ultima = logica;
                        logica = false;
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
