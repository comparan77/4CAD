using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.catalog
{
    public class Cuenta_tipoMng : dbTable
    {
        #region Campos
        protected Cuenta_tipo _oCuenta_tipo;
        protected List<Cuenta_tipo> _lst;
        #endregion

        #region Propiedades
        public Cuenta_tipo O_Cuenta_tipo { get { return _oCuenta_tipo; } set { _oCuenta_tipo = value; } }
        public List<Cuenta_tipo> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Cuenta_tipoMng()
        {
            this._oCuenta_tipo = new Cuenta_tipo();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCuenta_tipo.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oCuenta_tipo.Nombre);
        }

        public void fillAllLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cuenta_tipo");
                addParameters(-1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cuenta_tipo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cuenta_tipo o = new Cuenta_tipo();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    o.Nombre = dr["nombre"].ToString();
                    if (dr["IsActive"] != null)
                    {
                        bool.TryParse(dr["IsActive"].ToString(), out logica);
                        o.IsActive = logica;
                    }
                    this._lst.Add(o);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cuenta_tipo");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cuenta_tipo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cuenta_tipo o = new Cuenta_tipo();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    o.Nombre = dr["nombre"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cuenta_tipo");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    this._oCuenta_tipo.Nombre = dr["nombre"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cuenta_tipo");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oCuenta_tipo.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cuenta_tipo");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cuenta_tipo");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void reactive()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cuenta_tipo");
                addParameters(-2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }


        #endregion
    }
}
