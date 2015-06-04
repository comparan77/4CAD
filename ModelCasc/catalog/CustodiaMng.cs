﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.catalog
{
    public class CustodiaMng: dbTable
    {
        #region Campos
        protected Custodia _oCustodia;
        protected List<Custodia> _lst;
        #endregion

        #region Propiedades
        public Custodia O_Custodia { get { return _oCustodia; } set { _oCustodia = value; } }
        public List<Custodia> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public CustodiaMng()
        {
            this._oCustodia = new Custodia();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCustodia.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oCustodia.Nombre);
        }

        public void fillAllLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Custodia");
                addParameters(-1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Custodia>();
                foreach (DataRow dr in dt.Rows)
                {
                    Custodia o = new Custodia();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Custodia");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Custodia>();
                foreach (DataRow dr in dt.Rows)
                {
                    Custodia o = new Custodia();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Custodia");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    this._oCustodia.Nombre = dr["nombre"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Custodia");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oCustodia.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Custodia");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Custodia");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Custodia");
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
