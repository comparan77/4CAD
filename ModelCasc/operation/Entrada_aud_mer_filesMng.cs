using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Entrada_aud_mer_filesMng : dbTable, IAudImageMng
    {
        #region Campos
        protected Entrada_aud_mer_files _oEntrada_aud_mer_files;
        protected List<Entrada_aud_mer_files> _lst;
        #endregion

        #region Propiedades
        public Entrada_aud_mer_files O_Entrada_aud_mer_files { get { return _oEntrada_aud_mer_files; } set { _oEntrada_aud_mer_files = value; } }
        public List<Entrada_aud_mer_files> Lst { get { return _lst; } set { _lst = value; } }
        IAudImage IAudImageMng.O_Aud_Img
        {
            get
            {
                return this._oEntrada_aud_mer_files;
            }
            set
            {
                this._oEntrada_aud_mer_files = (Entrada_aud_mer_files)value;
            }
        }
        List<IAudImage> IAudImageMng.Lst
        {
            get
            {
                return this.Lst.Cast<IAudImage>().ToList();
            }
            set
            {
                this.Lst = value.Cast<Entrada_aud_mer_files>().ToList();
            }
        }
        #endregion

        #region Constructores
        public Entrada_aud_mer_filesMng()
        {
            this._oEntrada_aud_mer_files = new Entrada_aud_mer_files();
            this._lst = new List<Entrada_aud_mer_files>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_aud_mer_files.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_aud_mer", DbType.Int32, this._oEntrada_aud_mer_files.Id_entrada_aud_mer);
            GenericDataAccess.AddInParameter(this.comm, "?P_path", DbType.String, this._oEntrada_aud_mer_files.Path);
        }

        public void BindByDataRow(DataRow dr, Entrada_aud_mer_files o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_entrada_aud_mer"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada_aud_mer"].ToString(), out entero);
                    o.Id_entrada_aud_mer = entero;
                    entero = 0;
                }
                o.Path = dr["path"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_mer_files");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_aud_mer_files>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_aud_mer_files o = new Entrada_aud_mer_files();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_mer_files");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_aud_mer_files);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_mer_files");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_aud_mer_files.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_mer_files");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_mer_files");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_aud_mer_files");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada_aud_mer_files.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
