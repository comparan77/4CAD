using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Salida_aud_uni_filesMng: dbTable, IAudImageMng
    {
        #region Campos
        protected Salida_aud_uni_files _oSalida_aud_uni_files;
        protected List<Salida_aud_uni_files> _lst;
        #endregion

        #region Propiedades
        public Salida_aud_uni_files O_Salida_aud_uni_files { get { return _oSalida_aud_uni_files; } set { _oSalida_aud_uni_files = value; } }
        public List<Salida_aud_uni_files> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Salida_aud_uni_filesMng()
        {
            this._oSalida_aud_uni_files = new Salida_aud_uni_files();
            this._lst = new List<Salida_aud_uni_files>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida_aud_uni_files.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_salida_aud_uni", DbType.Int32, this._oSalida_aud_uni_files.Id_salida_aud_uni);
            GenericDataAccess.AddInParameter(this.comm, "?P_path", DbType.String, this._oSalida_aud_uni_files.Path);
        }

        public void BindByDataRow(DataRow dr, Salida_aud_uni_files o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_salida_aud_uni"] != DBNull.Value)
                {
                    int.TryParse(dr["id_salida_aud_uni"].ToString(), out entero);
                    o.Id_salida_aud_uni = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_aud_uni_files");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_aud_uni_files>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_aud_uni_files o = new Salida_aud_uni_files();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_aud_uni_files");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_aud_uni_files);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_aud_uni_files");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oSalida_aud_uni_files.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_aud_uni_files");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_aud_uni_files");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        public IAudImage O_Aud_Img
        {
            get
            {
                return this.O_Salida_aud_uni_files;
            }
            set
            {
                this.O_Salida_aud_uni_files = (Salida_aud_uni_files)value;
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
                this.Lst = value.Cast<Salida_aud_uni_files>().ToList();
            }
        }

        public void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_aud_uni_files");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this.O_Salida_aud_uni_files.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }
    }
}
