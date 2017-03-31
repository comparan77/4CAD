using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Salida_aud_uniMng : dbTable, IAuditoriaCAECppMng
    {
        #region Campos
        protected Salida_aud_uni _oSalida_aud_uni;
        protected List<Salida_aud_uni> _lst;
        #endregion

        #region Propiedades
        public Salida_aud_uni O_Salida_aud_uni { get { return _oSalida_aud_uni; } set { _oSalida_aud_uni = value; } }
        public List<Salida_aud_uni> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Salida_aud_uniMng()
        {
            this._oSalida_aud_uni = new Salida_aud_uni();
            this._lst = new List<Salida_aud_uni>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida_aud_uni.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_salida_orden_carga", DbType.Int32, this._oSalida_aud_uni.Id_salida_orden_carga);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_tipo", DbType.Int32, this._oSalida_aud_uni.Id_transporte_tipo);
            GenericDataAccess.AddInParameter(this.comm, "?P_informa", DbType.String, this._oSalida_aud_uni.Informa);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oSalida_aud_uni.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_operador", DbType.String, this._oSalida_aud_uni.Operador);
            GenericDataAccess.AddInParameter(this.comm, "?P_placa", DbType.String, this._oSalida_aud_uni.Placa);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja", DbType.String, this._oSalida_aud_uni.Caja);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja1", DbType.String, this._oSalida_aud_uni.Caja1);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja2", DbType.String, this._oSalida_aud_uni.Caja2);
            GenericDataAccess.AddInParameter(this.comm, "?P_sello", DbType.String, this._oSalida_aud_uni.Sello);
            GenericDataAccess.AddInParameter(this.comm, "?P_acta_informativa", DbType.String, this._oSalida_aud_uni.Acta_informativa);
            GenericDataAccess.AddInParameter(this.comm, "?P_vigilante", DbType.String, this._oSalida_aud_uni.Vigilante);
        }

        protected void BindByDataRow(DataRow dr, Salida_aud_uni o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_salida_orden_carga"] != DBNull.Value)
                {
                    int.TryParse(dr["id_salida_orden_carga"].ToString(), out entero);
                    o.Id_salida_orden_carga = entero;
                    entero = 0;
                }
                if (dr["id_transporte_tipo"] != DBNull.Value)
                {
                    int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                    o.Id_transporte_tipo = entero;
                    entero = 0;
                }
                o.Informa = dr["informa"].ToString();
                o.Referencia = dr["referencia"].ToString();
                o.Operador = dr["operador"].ToString();
                o.Placa = dr["placa"].ToString();
                o.Caja = dr["caja"].ToString();
                o.Caja1 = dr["caja1"].ToString();
                o.Caja2 = dr["caja2"].ToString();
                o.Sello = dr["sello"].ToString();
                o.Acta_informativa = dr["acta_informativa"].ToString();
                if (dr["Fecha"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["Fecha"].ToString(), out fecha);
                    o.Fecha = fecha;
                    fecha = default(DateTime);
                }
                o.Vigilante = dr["vigilante"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_aud_uni");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_aud_uni>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_aud_uni o = new Salida_aud_uni();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_aud_uni");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_aud_uni);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_aud_uni");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oSalida_aud_uni.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_aud_uni");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_aud_uni");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        public IAuditoriaCAEApp O_aud
        {
            get { return this.O_Salida_aud_uni; }
            set { this.O_Salida_aud_uni = (Salida_aud_uni)value; }
        }

        public void selByIdWithImg()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_aud_uni");
                addParameters(5);
                DataSet ds = GenericDataAccess.ExecuteMultSelectCommand(comm);
                this.dt = ds.Tables[0];
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this.O_Salida_aud_uni);

                    Salida_aud_uni_filesMng oMngFiles = new Salida_aud_uni_filesMng();
                    this.O_Salida_aud_uni.PLstSalAudUniFiles = new List<Salida_aud_uni_files>();
                    foreach (DataRow drFile in ds.Tables[1].Rows)
                    {
                        Salida_aud_uni_files oFile = new Salida_aud_uni_files();
                        oMngFiles.BindByDataRow(drFile, oFile);
                        this.O_Salida_aud_uni.PLstSalAudUniFiles.Add(oFile);
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

        public void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_aud_uni");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oSalida_aud_uni.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void selByOrdenCarga()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_aud_uni");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_aud_uni);
                    this.O_Salida_aud_uni.Cliente = dr["cliente"].ToString();
                    this.O_Salida_aud_uni.Lugar = dr["bodega"].ToString();
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
    }
}
