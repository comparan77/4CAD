using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Entrada_aud_merMng : dbTable, IAuditoriaCAECppMng
    {
        #region Campos
        protected Entrada_aud_mer _oEntrada_aud_mer;
        protected List<Entrada_aud_mer> _lst;
        #endregion

        #region Propiedades
        public Entrada_aud_mer O_Entrada_aud_mer { get { return _oEntrada_aud_mer; } set { _oEntrada_aud_mer = value; } }
        public List<Entrada_aud_mer> Lst { get { return _lst; } set { _lst = value; } }
        public IAuditoriaCAEApp O_aud { get { return this._oEntrada_aud_mer; } set { this._oEntrada_aud_mer = (Entrada_aud_mer)value; } }
        #endregion

        #region Constructores
        public Entrada_aud_merMng()
        {
            this._oEntrada_aud_mer = new Entrada_aud_mer();
            this._lst = new List<Entrada_aud_mer>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_aud_mer.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_pre_carga", DbType.Int32, this._oEntrada_aud_mer.Id_entrada_pre_carga);
            GenericDataAccess.AddInParameter(this.comm, "?P_informa", DbType.String, this._oEntrada_aud_mer.Informa);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oEntrada_aud_mer.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_notificado", DbType.String, this._oEntrada_aud_mer.Notificado);
            GenericDataAccess.AddInParameter(this.comm, "?P_entrada_unica", DbType.Boolean, this._oEntrada_aud_mer.Entrada_unica);
            GenericDataAccess.AddInParameter(this.comm, "?P_no_entrada", DbType.Int32, this._oEntrada_aud_mer.No_entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_bulto_declarado", DbType.Int32, this._oEntrada_aud_mer.Bulto_declarado);
            GenericDataAccess.AddInParameter(this.comm, "?P_bulto_recibido", DbType.Int32, this._oEntrada_aud_mer.Bulto_recibido);
            GenericDataAccess.AddInParameter(this.comm, "?P_bulto_abierto", DbType.Int32, this._oEntrada_aud_mer.Bulto_abierto);
            GenericDataAccess.AddInParameter(this.comm, "?P_bulto_danado", DbType.Int32, this._oEntrada_aud_mer.Bulto_danado);
            GenericDataAccess.AddInParameter(this.comm, "?P_pallet", DbType.Int32, this._oEntrada_aud_mer.Pallet);
            GenericDataAccess.AddInParameter(this.comm, "?P_acta_informativa", DbType.String, this._oEntrada_aud_mer.Acta_informativa);
            GenericDataAccess.AddInParameter(this.comm, "?P_vigilante", DbType.String, this._oEntrada_aud_mer.Vigilante);
        }

        protected void BindByDataRow(DataRow dr, Entrada_aud_mer o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_entrada_pre_carga"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada_pre_carga"].ToString(), out entero);
                    o.Id_entrada_pre_carga = entero;
                    entero = 0;
                }
                o.Informa = dr["informa"].ToString();
                o.Referencia = dr["referencia"].ToString();
                o.Notificado = dr["notificado"].ToString();
                if (dr["entrada_unica"] != DBNull.Value)
                {
                    bool.TryParse(dr["entrada_unica"].ToString(), out logica);
                    o.Entrada_unica = logica;
                    logica = false;
                }
                if (dr["no_entrada"] != DBNull.Value)
                {
                    int.TryParse(dr["no_entrada"].ToString(), out entero);
                    o.No_entrada = entero;
                    entero = 0;
                }
                if (dr["bulto_declarado"] != DBNull.Value)
                {
                    int.TryParse(dr["bulto_declarado"].ToString(), out entero);
                    o.Bulto_declarado = entero;
                    entero = 0;
                }
                if (dr["bulto_recibido"] != DBNull.Value)
                {
                    int.TryParse(dr["bulto_recibido"].ToString(), out entero);
                    o.Bulto_recibido = entero;
                    entero = 0;
                }
                if (dr["bulto_abierto"] != DBNull.Value)
                {
                    int.TryParse(dr["bulto_abierto"].ToString(), out entero);
                    o.Bulto_abierto = entero;
                    entero = 0;
                }
                if (dr["bulto_danado"] != DBNull.Value)
                {
                    int.TryParse(dr["bulto_danado"].ToString(), out entero);
                    o.Bulto_danado = entero;
                    entero = 0;
                }
                if (dr["pallet"] != DBNull.Value)
                {
                    int.TryParse(dr["pallet"].ToString(), out entero);
                    o.Pallet = entero;
                    entero = 0;
                }
                o.Acta_informativa = dr["acta_informativa"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_mer");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_aud_mer>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_aud_mer o = new Entrada_aud_mer();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_mer");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_aud_mer);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_mer");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_aud_mer.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_mer");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_mer");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_mer");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada_aud_mer.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        public void selByIdWithImg()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_mer");
                addParameters(5);
                DataSet ds = GenericDataAccess.ExecuteMultSelectCommand(comm);
                this.dt = ds.Tables[0];
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this.O_Entrada_aud_mer);

                    Entrada_aud_mer_filesMng oMngFiles = new Entrada_aud_mer_filesMng();
                    this.O_Entrada_aud_mer.PLstEntAudMerFiles = new List<Entrada_aud_mer_files>();
                    foreach (DataRow drFile in ds.Tables[1].Rows)
                    {
                        Entrada_aud_mer_files oFile = new Entrada_aud_mer_files();
                        oMngFiles.BindByDataRow(drFile, oFile);
                        this.O_Entrada_aud_mer.PLstEntAudMerFiles.Add(oFile);
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
        #endregion
    }
}
