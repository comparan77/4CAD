using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog.personal
{
    public class Personal_rolMng:dbTable
    {
        #region Campos
		protected Personal_rol _oPersonal_rol;
		protected List<Personal_rol> _lst;
		#endregion

		#region Propiedades
		public Personal_rol O_Personal_rol { get { return _oPersonal_rol; } set { _oPersonal_rol = value; } }
		public List<Personal_rol> Lst { get { return _lst; } set { _lst = value; } }
		#endregion

		#region Constructores
		public Personal_rolMng()
		{
			this._oPersonal_rol = new Personal_rol();
			this._lst = new List<Personal_rol>();
		}
		#endregion

		#region Metodos
		protected override void addParameters(int opcion)
		{
			GenericDataAccess.AddInParameter(this.comm,"?P_opcion", DbType.Int32, opcion);
			GenericDataAccess.AddInOutParameter(this.comm,"?P_id", DbType.Int32, this._oPersonal_rol.Id);
			GenericDataAccess.AddInParameter(this.comm,"?P_nombre", DbType.String, this._oPersonal_rol.Nombre);
		}

		protected void BindByDataRow(DataRow dr, Personal_rol o)
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
				this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_rol");
				addParameters(0);
				this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
				this._lst = new List<Personal_rol>();
				foreach(DataRow dr in dt.Rows)
				{
					Personal_rol o = new Personal_rol();
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
				this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_rol");
				addParameters(1);
				this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
				if(dt.Rows.Count == 1)
				{
					DataRow dr = dt.Rows[0];
					BindByDataRow(dr, this._oPersonal_rol);
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
				this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_rol");
				addParameters(2);
				GenericDataAccess.ExecuteNonQuery(this.comm);
				this._oPersonal_rol.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
			} catch {
				throw;
			}
		}

		public override void udt()
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_rol");
				addParameters(3);
				GenericDataAccess.ExecuteNonQuery(this.comm);
			} catch {
				throw;
			}
		}

		public override void dlt()
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Personal_rol");
				addParameters(4);
				GenericDataAccess.ExecuteNonQuery(this.comm);
			} catch {
				throw;
			}
		}

		#endregion
    }
}
