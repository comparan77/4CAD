using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.operation
{
    internal class Salida_compartidaMng : dbTable
    {
        #region Campos
        protected Salida_compartida _oSalida_compartida;
        protected List<Salida_compartida> _lst;
        #endregion

        #region Propiedades
        public Salida_compartida O_Salida_compartida { get { return _oSalida_compartida; } set { _oSalida_compartida = value; } }
        public List<Salida_compartida> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Salida_compartidaMng()
        {
            this._oSalida_compartida = new Salida_compartida();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida_compartida.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_salida", DbType.Int32, this._oSalida_compartida.Id_salida);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oSalida_compartida.Id_usuario);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio", DbType.String, this._oSalida_compartida.Folio);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oSalida_compartida.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_capturada", DbType.Boolean, this._oSalida_compartida.Capturada);
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_compartida");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_compartida>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_compartida o = new Salida_compartida();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_salida"].ToString(), out entero);
                        o.Id_salida = entero;
                        entero = 0;
                    }
                    else
                    {
                        o.Id_salida = null;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        o.Id_usuario = entero;
                        entero = 0;
                    }
                    o.Folio = dr["folio"].ToString();
                    o.Referencia = dr["referencia"].ToString();
                    if (dr["capturada"] != DBNull.Value)
                    {
                        bool.TryParse(dr["capturada"].ToString(), out logica);
                        o.Capturada = logica;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_compartida");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_salida"].ToString(), out entero);
                        this._oSalida_compartida.Id_salida = entero;
                        entero = 0;
                    }
                    else
                    {
                        this._oSalida_compartida.Id_salida = null;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        this._oSalida_compartida.Id_usuario = entero;
                        entero = 0;
                    }
                    this._oSalida_compartida.Folio = dr["folio"].ToString();
                    this._oSalida_compartida.Referencia = dr["referencia"].ToString();
                    if (dr["capturada"] != DBNull.Value)
                    {
                        bool.TryParse(dr["capturada"].ToString(), out logica);
                        this._oSalida_compartida.Capturada = logica;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_compartida");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oSalida_compartida.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_compartida");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_compartida");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public string GetIndice(IDbTransaction trans)
        {
            string indice = string.Empty;

            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_compartida");
                addParameters(6);
                indice = GenericDataAccess.ExecuteScalar(this.comm, trans);
            }
            catch
            {
                throw;
            }

            return indice;
        }

        public void udtSalidaCompartida(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_compartida");
                addParameters(7);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        public void SelByFolio()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_compartida");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_compartida>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_compartida o = new Salida_compartida();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_salida"].ToString(), out entero);
                        o.Id_salida = entero;
                        entero = 0;
                    }
                    else
                    {
                        o.Id_salida = null;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        o.Id_usuario = entero;
                        entero = 0;
                    }
                    o.Folio = dr["folio"].ToString();
                    o.Referencia = dr["referencia"].ToString();
                    if (dr["capturada"] != DBNull.Value)
                    {
                        bool.TryParse(dr["capturada"].ToString(), out logica);
                        o.Capturada = logica;
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

        public void fillLstSalidaCompartida()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_compartida");
                addParameters(8);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_compartida>();

                foreach (DataRow dr in dt.Rows)
                {
                    Salida_compartida o = new Salida_compartida();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_salida"].ToString(), out entero);
                        o.Id_salida = entero;
                        entero = 0;
                    }
                    else
                    {
                        o.Id_salida = null;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        o.Id_usuario = entero;
                        entero = 0;
                    }
                    o.Folio = dr["folio"].ToString();
                    o.Referencia = dr["referencia"].ToString();
                    if (dr["capturada"] != DBNull.Value)
                    {
                        bool.TryParse(dr["capturada"].ToString(), out logica);
                        o.Capturada = logica;
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

        internal bool Exists()
        {
            bool Exist = false;
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_compartida");
                addParameters(9);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count >= 1)
                {
                    Exist = true;
                    DataRow dr = dt.Rows[0];
                    if (dr["id_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_salida"].ToString(), out entero);
                        this._oSalida_compartida.Id_salida = entero;
                        entero = 0;
                    }
                    else
                    {
                        this._oSalida_compartida.Id_salida = null;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        this._oSalida_compartida.Id_usuario = entero;
                        entero = 0;
                    }
                    this._oSalida_compartida.Folio = dr["folio"].ToString();
                    this._oSalida_compartida.Referencia = dr["referencia"].ToString();
                    if (dr["capturada"] != DBNull.Value)
                    {
                        bool.TryParse(dr["capturada"].ToString(), out logica);
                        this._oSalida_compartida.Capturada = logica;
                        logica = false;
                    }
                }
            }
            catch
            {
                throw;
            }
            return Exist;
        }

        #endregion
               
    }
}
