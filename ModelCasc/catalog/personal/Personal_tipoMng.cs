using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog.personal
{
    internal class Personal_tipoMng: dbTable
    {
        #region Campos
		protected Personal_tipo _oPersonal_tipo;
		protected List<Personal_tipo> _lst;
		#endregion

		#region Propiedades
		public Personal_tipo O_Personal_tipo { get { return _oPersonal_tipo; } set { _oPersonal_tipo = value; } }
		public List<Personal_tipo> Lst { get { return _lst; } set { _lst = value; } }
		#endregion

		#region Constructores
		public Personal_tipoMng()
		{
			this._oPersonal_tipo = new Personal_tipo();
			this._lst = new List<Personal_tipo>();
		}
		#endregion

		#region Metodos
		protected override void addParameters(int opcion)
		{
			GenericDataAccess.AddInParameter(this.comm,"?P_opcion", DbType.Int32, opcion);
			GenericDataAccess.AddInOutParameter(this.comm,"?P_id", DbType.Int32, this._oPersonal_tipo.Id);
			GenericDataAccess.AddInParameter(this.comm,"?P_nombre", DbType.String, this._oPersonal_tipo.Nombre);
		}

		protected void BindByDataRow(DataRow dr, Personal_tipo o)
		{
			try {
				int.TryParse(dr["id"].ToString(), out entero);
				o.Id = entero;
				entero = 0;
				o.Nombre = dr["nombre"].ToString();
			} catch  {
				throw;
			}
		}

		public override void fillLst()
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_tipo");
				addParameters(0);
				this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
				this._lst = new List<Personal_tipo>();
				foreach(DataRow dr in dt.Rows)
				{
					Personal_tipo o = new Personal_tipo();
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
				this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_tipo");
				addParameters(1);
				this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
				if(dt.Rows.Count == 1)
				{
					DataRow dr = dt.Rows[0];
					BindByDataRow(dr, this._oPersonal_tipo);
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
				this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_tipo");
				addParameters(2);
				GenericDataAccess.ExecuteNonQuery(this.comm);
				this._oPersonal_tipo.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
			} catch {
				throw;
			}
		}

		public override void udt()
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_tipo");
				addParameters(3);
				GenericDataAccess.ExecuteNonQuery(this.comm);
			} catch {
				throw;
			}
		}

		public override void dlt()
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_tipo");
				addParameters(4);
				GenericDataAccess.ExecuteNonQuery(this.comm);
			} catch {
				throw;
			}
		}

		#endregion
    }
}
