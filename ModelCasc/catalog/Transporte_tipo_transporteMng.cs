using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.catalog
{
    public class Transporte_tipo_transporteMng : dbTable
    {
        #region Campos
        protected Transporte_tipo_transporte _oTransporte_tipo_transporte;
        protected List<Transporte_tipo_transporte> _lst;
        #endregion

        #region Propiedades
        public Transporte_tipo_transporte O_Transporte_tipo_transporte { get { return _oTransporte_tipo_transporte; } set { _oTransporte_tipo_transporte = value; } }
        public List<Transporte_tipo_transporte> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Transporte_tipo_transporteMng()
        {
            this._oTransporte_tipo_transporte = new Transporte_tipo_transporte();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oTransporte_tipo_transporte.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte", DbType.Int32, this._oTransporte_tipo_transporte.Id_transporte);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_tipo", DbType.Int32, this._oTransporte_tipo_transporte.Id_transporte_tipo);
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo_transporte");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Transporte_tipo_transporte>();
                foreach (DataRow dr in dt.Rows)
                {
                    Transporte_tipo_transporte o = new Transporte_tipo_transporte();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_transporte"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte"].ToString(), out entero);
                        o.Id_transporte = entero;
                        entero = 0;
                    }
                    if (dr["id_transporte_tipo"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                        o.Id_transporte_tipo = entero;
                        entero = 0;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo_transporte");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_transporte"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte"].ToString(), out entero);
                        this._oTransporte_tipo_transporte.Id_transporte = entero;
                        entero = 0;
                    }
                    if (dr["id_transporte_tipo"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                        this._oTransporte_tipo_transporte.Id_transporte_tipo = entero;
                        entero = 0;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo_transporte");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oTransporte_tipo_transporte.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo_transporte");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo_transporte");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void selByIdTransporte()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo_transporte");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    int.TryParse(dr["id"].ToString(), out entero);
                    this._oTransporte_tipo_transporte.Id = entero;
                    entero = 0;
                    if (dr["id_transporte"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte"].ToString(), out entero);
                        this._oTransporte_tipo_transporte.Id_transporte = entero;
                        entero = 0;
                    }
                    if (dr["id_transporte_tipo"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                        this._oTransporte_tipo_transporte.Id_transporte_tipo = entero;
                        entero = 0;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void selByIdTransporteTipo()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo_transporte");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    int.TryParse(dr["id"].ToString(), out entero);
                    this._oTransporte_tipo_transporte.Id = entero;
                    entero = 0;
                    if (dr["id_transporte"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte"].ToString(), out entero);
                        this._oTransporte_tipo_transporte.Id_transporte = entero;
                        entero = 0;
                    }
                    if (dr["id_transporte_tipo"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                        this._oTransporte_tipo_transporte.Id_transporte_tipo = entero;
                        entero = 0;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void dltByIdTransporteTipo()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo_transporte");
                addParameters(7);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void fillLstByTransporteTipo()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo_transporte");
                addParameters(8);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Transporte_tipo_transporte>();
                foreach (DataRow dr in dt.Rows)
                {
                    Transporte_tipo_transporte o = new Transporte_tipo_transporte();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_transporte"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte"].ToString(), out entero);
                        o.Id_transporte = entero;
                        entero = 0;
                    }
                    if (dr["id_transporte_tipo"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                        o.Id_transporte_tipo = entero;
                        entero = 0;
                    }
                    o.Transporte = dr["transporte"].ToString();
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
