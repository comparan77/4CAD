using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.operation
{
    internal class Entrada_compartidaMng: dbTable
    {
        #region Campos
        protected Entrada_compartida _oEntrada_compartida;
        protected List<Entrada_compartida> _lst;
        #endregion

        #region Propiedades
        public Entrada_compartida O_Entrada_compartida { get { return _oEntrada_compartida; } set { _oEntrada_compartida = value; } }
        public List<Entrada_compartida> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_compartidaMng()
        {
            this._oEntrada_compartida = new Entrada_compartida();
            this._lst = new List<Entrada_compartida>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_compartida.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada", DbType.Int32, this._oEntrada_compartida.Id_entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oEntrada_compartida.Id_usuario);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio", DbType.String, this._oEntrada_compartida.Folio);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oEntrada_compartida.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_capturada", DbType.Boolean, this._oEntrada_compartida.Capturada);
        }

        protected void BindByDataRow(DataRow dr, Entrada_compartida o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_entrada"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada"].ToString(), out entero);
                    o.Id_entrada = entero;
                    entero = 0;
                }
                else
                {
                    o.Id_entrada = null;
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
                if (dr["IsActive"] != DBNull.Value)
                {
                    bool.TryParse(dr["IsActive"].ToString(), out logica);
                    o.IsActive = logica;
                    logica = false;
                }
            }
            catch
            {
                throw;
            }
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_compartida>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_compartida o = new Entrada_compartida();
                    BindByDataRow(dr, o);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_compartida);
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
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_compartida.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada_compartida.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_compartida>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_compartida o = new Entrada_compartida();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
                addParameters(6);
                indice = GenericDataAccess.ExecuteScalar(this.comm, trans);
            }
            catch
            {
                throw;
            }

            return indice;
        }

        public void udtEntradaCompartida(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
                addParameters(7);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        public void fillLstEntradaCompartida(bool ConFondeo = false)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
                addParameters(ConFondeo ? 10 : 8);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_compartida>();
                
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_compartida o = new Entrada_compartida();
                    BindByDataRow(dr, o);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
                addParameters(9);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count >= 1)
                {
                    Exist = true;
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_compartida);
                    this._oEntrada_compartida.NombreUsuario = dr["nombre"].ToString();
                }
            }
            catch
            {
                throw;
            }
            return Exist;
        }

        internal void selByIdEntrada(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
                addParameters(11);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_compartida);
                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad");
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void addPendienteEntrada(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
                addParameters(12);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        internal void deactive(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
                addParameters(13);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        internal int countByFolio(IDbTransaction trans)
        {
            int cantidad = -1;
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
                addParameters(14);
                cantidad = Convert.ToInt32(GenericDataAccess.ExecuteScalar(this.comm, trans));
            }
            catch
            {
                throw;
            }
            return cantidad;
        }

        internal void dltByFolio(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_compartida");
                addParameters(15);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }
    }
}
