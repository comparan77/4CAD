using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.catalog
{
    public class Transporte_tipoMng: dbTable
    {
        #region Campos
        protected Transporte_tipo _oTransporte_tipo;
        protected List<Transporte_tipo> _lst;
        #endregion

        #region Propiedades
        public Transporte_tipo O_Transporte_tipo { get { return _oTransporte_tipo; } set { _oTransporte_tipo = value; } }
        public List<Transporte_tipo> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Transporte_tipoMng()
        {
            this._oTransporte_tipo = new Transporte_tipo();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oTransporte_tipo.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oTransporte_tipo.Nombre);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_peso_maximo", DbType.Int32, this._oTransporte_tipo.Peso_maximo);
            GenericDataAccess.AddInParameter(this.comm, "?P_requiere_placa", DbType.Boolean, this._oTransporte_tipo.Requiere_placa);
            GenericDataAccess.AddInParameter(this.comm, "?P_requiere_caja", DbType.Boolean, this._oTransporte_tipo.Requiere_caja);
            GenericDataAccess.AddInParameter(this.comm, "?P_requiere_caja1", DbType.Boolean, this._oTransporte_tipo.Requiere_caja1);
            GenericDataAccess.AddInParameter(this.comm, "?P_requiere_caja2", DbType.Boolean, this._oTransporte_tipo.Requiere_caja2);
        }

        public void fillAllLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo");
                addParameters(-1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Transporte_tipo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Transporte_tipo o = new Transporte_tipo();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["peso_maximo"] != DBNull.Value)
                    {
                        int.TryParse(dr["peso_maximo"].ToString(), out entero);
                        o.Peso_maximo = entero;
                        entero = 0;
                    }
                    o.Nombre = dr["nombre"].ToString();
                    if (dr["requiere_placa"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_placa"].ToString(), out logica);
                        o.Requiere_placa = logica;
                        logica = false;
                    }
                    if (dr["requiere_caja"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_caja"].ToString(), out logica);
                        o.Requiere_caja = logica;
                        logica = false;
                    }
                    if (dr["requiere_caja1"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_caja1"].ToString(), out logica);
                        o.Requiere_caja1 = logica;
                        logica = false;
                    }
                    if (dr["requiere_caja2"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_caja2"].ToString(), out logica);
                        o.Requiere_caja2 = logica;
                        logica = false;
                    }
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Transporte_tipo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Transporte_tipo o = new Transporte_tipo();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["peso_maximo"] != DBNull.Value)
                    {
                        int.TryParse(dr["peso_maximo"].ToString(), out entero);
                        o.Peso_maximo = entero;
                        entero = 0;
                    }
                    o.Nombre = dr["nombre"].ToString();
                    if (dr["requiere_placa"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_placa"].ToString(), out logica);
                        o.Requiere_placa = logica;
                        logica = false;
                    }
                    if (dr["requiere_caja"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_caja"].ToString(), out logica);
                        o.Requiere_caja = logica;
                        logica = false;
                    }
                    if (dr["requiere_caja1"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_caja1"].ToString(), out logica);
                        o.Requiere_caja1 = logica;
                        logica = false;
                    }
                    if (dr["requiere_caja2"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_caja2"].ToString(), out logica);
                        o.Requiere_caja2 = logica;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    this._oTransporte_tipo.Nombre = dr["nombre"].ToString();
                    if (dr["peso_maximo"] != DBNull.Value)
                    {
                        int.TryParse(dr["peso_maximo"].ToString(), out entero);
                        this._oTransporte_tipo.Peso_maximo = entero;
                        entero = 0;
                    }
                    if (dr["requiere_placa"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_placa"].ToString(), out logica);
                        this._oTransporte_tipo.Requiere_placa = logica;
                        logica = false;
                    }
                    if (dr["requiere_caja"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_caja"].ToString(), out logica);
                        this._oTransporte_tipo.Requiere_caja = logica;
                        logica = false;
                    }
                    if (dr["requiere_caja1"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_caja1"].ToString(), out logica);
                        this._oTransporte_tipo.Requiere_caja1 = logica;
                        logica = false;
                    }
                    if (dr["requiere_caja2"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_caja2"].ToString(), out logica);
                        this._oTransporte_tipo.Requiere_caja2 = logica;
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
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oTransporte_tipo.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo");
                addParameters(-2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Devuelve los tipos de transporte asociados a la linea de transporte
        /// El Id de la clase Transporte_tipo se considera como el Id de la linea de transporte en este método
        /// </summary>
        public void getByIdTransporte()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Transporte_tipo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Transporte_tipo o = new Transporte_tipo();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    o.Nombre = dr["nombre"].ToString();
                    if (dr["peso_maximo"] != DBNull.Value)
                    {
                        int.TryParse(dr["peso_maximo"].ToString(), out entero);
                        o.Peso_maximo = entero;
                        entero = 0;
                    }
                    if (dr["requiere_placa"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_placa"].ToString(), out logica);
                        o.Requiere_placa = logica;
                        logica = false;
                    }
                    if (dr["requiere_caja"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_caja"].ToString(), out logica);
                        o.Requiere_caja = logica;
                        logica = false;
                    }
                    if (dr["requiere_caja1"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_caja1"].ToString(), out logica);
                        o.Requiere_caja1 = logica;
                        logica = false;
                    }
                    if (dr["requiere_caja2"] != DBNull.Value)
                    {
                        bool.TryParse(dr["requiere_caja2"].ToString(), out logica);
                        o.Requiere_caja2 = logica;
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
        #endregion
    }
}
