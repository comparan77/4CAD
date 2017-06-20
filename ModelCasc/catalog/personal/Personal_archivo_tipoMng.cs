using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog.personal
{
    internal class Personal_archivo_tipoMng : dbTable
    {
        #region Campos
		protected Personal_archivo_tipo _oPersonal_archivo_tipo;
		protected List<Personal_archivo_tipo> _lst;
		#endregion

		#region Propiedades
		public Personal_archivo_tipo O_Personal_archivo_tipo { get { return _oPersonal_archivo_tipo; } set { _oPersonal_archivo_tipo = value; } }
		public List<Personal_archivo_tipo> Lst { get { return _lst; } set { _lst = value; } }
		#endregion

		#region Constructores
		public Personal_archivo_tipoMng()
		{
			this._oPersonal_archivo_tipo = new Personal_archivo_tipo();
			this._lst = new List<Personal_archivo_tipo>();
		}
		#endregion

		#region Metodos
		protected override void addParameters(int opcion)
		{
			GenericDataAccess.AddInParameter(this.comm,"?P_opcion", DbType.Int32, opcion);
			GenericDataAccess.AddInOutParameter(this.comm,"?P_id", DbType.Int32, this._oPersonal_archivo_tipo.Id);
			GenericDataAccess.AddInParameter(this.comm,"?P_tipo", DbType.String, this._oPersonal_archivo_tipo.Tipo);
		}

		protected void BindByDataRow(DataRow dr, Personal_archivo_tipo o)
		{
			try {
				int.TryParse(dr["id"].ToString(), out entero);
				o.Id = entero;
				entero = 0;
				o.Tipo = dr["tipo"].ToString();
			} catch  {
				throw;
			}
		}

		public override void fillLst()
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_archivo_tipo");
				addParameters(0);
				this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
				this._lst = new List<Personal_archivo_tipo>();
				foreach(DataRow dr in dt.Rows)
				{
					Personal_archivo_tipo o = new Personal_archivo_tipo();
					BindByDataRow(dr, o);
					this._lst.Add(o);
				}
			} catch  {
				throw;
			}
		}

		public override void selById()
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_archivo_tipo");
				addParameters(1);
				this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
				if(dt.Rows.Count == 1)
				{
					DataRow dr = dt.Rows[0];
					BindByDataRow(dr, this._oPersonal_archivo_tipo);
				}
				else if(dt.Rows.Count > 1)
					throw new Exception("Error de integridad");
				else
					throw new Exception("No existe información para el registro solicitado");
			} catch {
				throw;
			}
		}

		public override void add()
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_archivo_tipo");
				addParameters(2);
				GenericDataAccess.ExecuteNonQuery(this.comm);
				this._oPersonal_archivo_tipo.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
			} catch {
				throw;
			}
		}

		public override void udt()
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_archivo_tipo");
				addParameters(3);
				GenericDataAccess.ExecuteNonQuery(this.comm);
			} catch {
				throw;
			}
		}

		public override void dlt()
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_archivo_tipo");
				addParameters(4);
				GenericDataAccess.ExecuteNonQuery(this.comm);
			} catch {
				throw;
			}
		}

		#endregion
    }
}
