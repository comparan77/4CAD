using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.operation
{
    internal class Entrada_parcialMng : dbTable
    {
        #region Campos
        protected Entrada_parcial _oEntrada_parcial;
        protected List<Entrada_parcial> _lst;
        #endregion

        #region Propiedades
        public Entrada_parcial O_Entrada_parcial { get { return _oEntrada_parcial; } set { _oEntrada_parcial = value; } }
        public List<Entrada_parcial> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_parcialMng()
        {
            this._oEntrada_parcial = new Entrada_parcial();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_parcial.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada", DbType.Int32, this._oEntrada_parcial.Id_entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oEntrada_parcial.Id_usuario);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_no_entrada", DbType.Int32, this._oEntrada_parcial.No_entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oEntrada_parcial.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_es_ultima", DbType.Boolean, this._oEntrada_parcial.Es_ultima);
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_parcial");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_parcial>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_parcial o = new Entrada_parcial();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        o.Id_entrada = entero;
                        entero = 0;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        o.Id_usuario = entero;
                        entero = 0;
                    }
                    if (dr["no_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_entrada"].ToString(), out entero);
                        o.No_entrada = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_parcial");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        this._oEntrada_parcial.Id_entrada = entero;
                        entero = 0;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        this._oEntrada_parcial.Id_usuario = entero;
                        entero = 0;
                    }
                    if (dr["no_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_entrada"].ToString(), out entero);
                        this._oEntrada_parcial.No_entrada = entero;
                        entero = 0;
                    }
                    this._oEntrada_parcial.Referencia = dr["referencia"].ToString();
                    if (dr["es_ultima"] != DBNull.Value)
                    {
                        bool.TryParse(dr["es_ultima"].ToString(), out logica);
                        this._oEntrada_parcial.Es_ultima = logica;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_parcial");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada_parcial.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
                this._oEntrada_parcial.No_entrada = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_no_entrada"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_parcial");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_parcial");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_parcial");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_parcial>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_parcial o = new Entrada_parcial();
                    //int.TryParse(dr["id"].ToString(), out entero);
                    //o.Id = entero;
                    //entero = 0;
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        o.Id_entrada = entero;
                        entero = 0;
                    }
                    //if (dr["id_usuario"] != DBNull.Value)
                    //{
                    //    int.TryParse(dr["id_usuario"].ToString(), out entero);
                    //    o.Id_usuario = entero;
                    //    entero = 0;
                    //}
                    if (dr["no_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_entrada"].ToString(), out entero);
                        o.No_entrada = entero;
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

        public void selByIdEntrada()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_parcial");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    int.TryParse(dr["id"].ToString(), out entero);
                    this._oEntrada_parcial.Id = entero;
                    entero = 0;
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        this._oEntrada_parcial.Id_entrada = entero;
                        entero = 0;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        this._oEntrada_parcial.Id_usuario = entero;
                        entero = 0;
                    }
                    if (dr["no_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_entrada"].ToString(), out entero);
                        this._oEntrada_parcial.No_entrada = entero;
                        entero = 0;
                    }
                    this._oEntrada_parcial.Referencia = dr["referencia"].ToString();
                    if (dr["es_ultima"] != DBNull.Value)
                    {
                        logica = Convert.ToBoolean(dr["es_ultima"]);
                        //bool.TryParse(dr["es_ultima"], out logica);
                        this._oEntrada_parcial.Es_ultima = logica;
                        logica = false;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void getAllByReferencia()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_parcial");
                addParameters(7);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_parcial>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_parcial o = new Entrada_parcial();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        o.Id_entrada = entero;
                        entero = 0;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        o.Id_usuario = entero;
                        entero = 0;
                    }
                    if (dr["no_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_entrada"].ToString(), out entero);
                        o.No_entrada = entero;
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

        internal void getByReferencia(bool EvenLast = false)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_parcial");
                addParameters(EvenLast ? 9 : 8);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        this._oEntrada_parcial.Id_entrada = entero;
                        entero = 0;
                    }
                    if (dr["no_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_entrada"].ToString(), out entero);
                        this._oEntrada_parcial.No_entrada = entero;
                        entero = 0;
                    }
                    this._oEntrada_parcial.Referencia = dr["referencia"].ToString();
                    if (dr["pieza_recibidas"] != DBNull.Value)
                    {
                        int.TryParse(dr["pieza_recibidas"].ToString(), out entero);
                        this._oEntrada_parcial.No_pieza_recibidas = entero;
                        entero = 0;
                    }
                    if (dr["bulto_recibido"] != DBNull.Value)
                    {
                        int.TryParse(dr["bulto_recibido"].ToString(), out entero);
                        this._oEntrada_parcial.No_bulto_recibido = entero;
                        entero = 0;
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
