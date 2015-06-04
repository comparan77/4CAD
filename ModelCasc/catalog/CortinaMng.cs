using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.catalog
{
    public class CortinaMng: dbTable
    {
        #region Campos
        protected Cortina _oCortina;
        protected List<Cortina> _lst;
        #endregion

        #region Propiedades
        public Cortina O_Cortina { get { return _oCortina; } set { _oCortina = value; } }
        public List<Cortina> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public CortinaMng()
        {
            this._oCortina = new Cortina();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCortina.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_bodega", DbType.Int32, this._oCortina.Id_bodega);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oCortina.Nombre);
        }

        public void fillAllLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cortina");
                addParameters(-1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cortina>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cortina o = new Cortina();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        o.Id_bodega = entero;
                        entero = 0;
                    }
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cortina");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cortina>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cortina o = new Cortina();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        o.Id_bodega = entero;
                        entero = 0;
                    }
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cortina");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        this._oCortina.Id_bodega = entero;
                        entero = 0;
                    }
                    this._oCortina.Nombre = dr["nombre"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cortina");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oCortina.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cortina");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cortina");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cortina");
                addParameters(-2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void selByIdBodegaAll()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cortina");
                addParameters(-3);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cortina>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cortina o = new Cortina();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        o.Id_bodega = entero;
                        entero = 0;
                    }
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

        public void selByIdBodega()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cortina");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cortina>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cortina o = new Cortina();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        o.Id_bodega = entero;
                        entero = 0;
                    }
                    o.Nombre = dr["nombre"].ToString();
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
