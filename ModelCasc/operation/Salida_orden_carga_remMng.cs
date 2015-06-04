﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Salida_orden_carga_remMng: dbTable
    {
        #region Campos
        protected Salida_orden_carga_rem _oSalida_orden_carga_rem;
        protected List<Salida_orden_carga_rem> _lst;
        #endregion

        #region Propiedades
        public Salida_orden_carga_rem O_Salida_orden_carga_rem { get { return _oSalida_orden_carga_rem; } set { _oSalida_orden_carga_rem = value; } }
        public List<Salida_orden_carga_rem> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Salida_orden_carga_remMng()
        {
            this._oSalida_orden_carga_rem = new Salida_orden_carga_rem();
            this._lst = new List<Salida_orden_carga_rem>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida_orden_carga_rem.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_salida_remision", DbType.Int32, this._oSalida_orden_carga_rem.Id_salida_remision);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_salida_orden_carga", DbType.Int32, this._oSalida_orden_carga_rem.Id_salida_orden_carga);
            if(this._oSalida_orden_carga_rem.Id_salida==null)
                GenericDataAccess.AddInOutParameter(this.comm, "?P_id_salida", DbType.Int32, DBNull.Value);
            else
                GenericDataAccess.AddInOutParameter(this.comm, "?P_id_salida", DbType.Int32, this._oSalida_orden_carga_rem.Id_salida);
        }

        protected void BindByDataRowRem(DataRow dr, Salida_orden_carga_rem o)
        {
            try
            {
                o.PSalRem.Folio_remision = dr["folio_remision"].ToString();
                o.PSalRem.Referencia = dr["referencia"].ToString();
                o.PSalRem.Codigo = dr["codigo"].ToString();
                o.PSalRem.Orden = dr["orden"].ToString();
                
            }
            catch
            {
                throw;
            }
        }

        protected void BindByDataRow(DataRow dr, Salida_orden_carga_rem o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_salida_remision"] != DBNull.Value)
                {
                    int.TryParse(dr["id_salida_remision"].ToString(), out entero);
                    o.Id_salida_remision = entero;
                    entero = 0;
                }
                if (dr["id_salida_orden_carga"] != DBNull.Value)
                {
                    int.TryParse(dr["id_salida_orden_carga"].ToString(), out entero);
                    o.Id_salida_orden_carga = entero;
                    entero = 0;
                }
                if (dr["id_salida"] != DBNull.Value)
                {
                    int.TryParse(dr["id_salida"].ToString(), out entero);
                    o.Id_salida = entero;
                    entero = 0;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_rem");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_orden_carga_rem>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_orden_carga_rem o = new Salida_orden_carga_rem();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_rem");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_orden_carga_rem);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_rem");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oSalida_orden_carga_rem.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_rem");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_rem");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_rem");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oSalida_orden_carga_rem.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void selByIdSalOrdCarga()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_rem");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_orden_carga_rem>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_orden_carga_rem o = new Salida_orden_carga_rem();
                    BindByDataRow(dr, o);
                    o.PSalRem = new Salida_remision();
                    BindByDataRowRem(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void selByIdSalRemision()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_orden_carga_rem");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_orden_carga_rem);
                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad");
            }
            catch
            {
                throw;
            }
        }
    }
}
