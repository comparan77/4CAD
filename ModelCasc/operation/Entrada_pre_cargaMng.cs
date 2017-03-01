﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.operation
{
    internal class Entrada_pre_cargaMng: dbTable
    {
        #region Campos
        protected Entrada_pre_carga _oEntrada_pre_carga;
        protected List<Entrada_pre_carga> _lst;
        #endregion

        #region Propiedades
        public Entrada_pre_carga O_Entrada_pre_carga { get { return _oEntrada_pre_carga; } set { _oEntrada_pre_carga = value; } }
        public List<Entrada_pre_carga> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_pre_cargaMng()
        {
            this._oEntrada_pre_carga = new Entrada_pre_carga();
            this._lst = new List<Entrada_pre_carga>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_pre_carga.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_tipo", DbType.Int32, this._oEntrada_pre_carga.Id_transporte_tipo);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oEntrada_pre_carga.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_operador", DbType.String, this._oEntrada_pre_carga.Operador);
            GenericDataAccess.AddInParameter(this.comm, "?P_placa", DbType.String, this._oEntrada_pre_carga.Placa);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja", DbType.String, this._oEntrada_pre_carga.Caja);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja1", DbType.String, this._oEntrada_pre_carga.Caja1);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja2", DbType.String, this._oEntrada_pre_carga.Caja2);
            GenericDataAccess.AddInParameter(this.comm, "?P_sello", DbType.String, this._oEntrada_pre_carga.Sello);
            GenericDataAccess.AddInParameter(this.comm, "?P_observaciones", DbType.String, this._oEntrada_pre_carga.Observaciones);
        }

        protected void BindByDataRow(DataRow dr, Entrada_pre_carga o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_transporte_tipo"] != DBNull.Value)
                {
                    int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                    o.Id_transporte_tipo = entero;
                    entero = 0;
                }
                o.Referencia = dr["referencia"].ToString();
                o.Operador = dr["operador"].ToString();
                o.Placa = dr["placa"].ToString();
                o.Caja = dr["caja"].ToString();
                o.Caja1 = dr["caja1"].ToString();
                o.Caja2 = dr["caja2"].ToString();
                o.Sello = dr["sello"].ToString();
                o.Observaciones = dr["observaciones"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_pre_carga");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_pre_carga>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_pre_carga o = new Entrada_pre_carga();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_pre_carga");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_pre_carga);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_pre_carga");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_pre_carga.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_pre_carga");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_pre_carga");
                addParameters(4);
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
