using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog
{
    internal class Transporte_condicionMng: dbTable
    {
        #region Campos
		protected Transporte_condicion _oTransporte_condicion;
		protected List<Transporte_condicion> _lst;
		#endregion

		#region Propiedades
		public Transporte_condicion O_Transporte_condicion { get { return _oTransporte_condicion; } set { _oTransporte_condicion = value; } }
		public List<Transporte_condicion> Lst { get { return _lst; } set { _lst = value; } }
		#endregion

		#region Constructores
		public Transporte_condicionMng()
		{
			this._oTransporte_condicion = new Transporte_condicion();
			this._lst = new List<Transporte_condicion>();
		}
		#endregion

		#region Metodos
		protected override void addParameters(int opcion)
		{
			GenericDataAccess.AddInParameter(this.comm,"?P_opcion", DbType.Int32, opcion);
			GenericDataAccess.AddInOutParameter(this.comm,"?P_id", DbType.Int32, this._oTransporte_condicion.Id);
			GenericDataAccess.AddInParameter(this.comm,"?P_id_transporte_condicion_categoria", DbType.Int32, this._oTransporte_condicion.Id_transporte_condicion_categoria);
			GenericDataAccess.AddInParameter(this.comm,"?P_nombre", DbType.String, this._oTransporte_condicion.Nombre);
		}

		public void BindByDataRow(DataRow dr, Transporte_condicion o)
		{
			try {
				int.TryParse(dr["id"].ToString(), out entero);
				o.Id = entero;
				entero = 0;
				if (dr["id_transporte_condicion_categoria"] != DBNull.Value)
				{
					int.TryParse(dr["id_transporte_condicion_categoria"].ToString(), out entero);
					o.Id_transporte_condicion_categoria = entero;
					entero = 0;
				}
				else
				{
					o.Id_transporte_condicion_categoria = null;
				}
				o.Nombre = dr["nombre"].ToString();
				if (dr["IsActive"] != DBNull.Value)
				{
					bool.TryParse(dr["IsActive"].ToString(), out logica);
					o.IsActive = logica;
					logica = false;
				}
			} catch  {
				throw;
			}
		}

		public override void fillLst()
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion");
				addParameters(0);
				this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
				this._lst = new List<Transporte_condicion>();
				foreach(DataRow dr in dt.Rows)
				{
					Transporte_condicion o = new Transporte_condicion();
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
				this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion");
				addParameters(1);
				this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
				if(dt.Rows.Count == 1)
				{
					DataRow dr = dt.Rows[0];
					BindByDataRow(dr, this._oTransporte_condicion);
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
				this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion");
				addParameters(2);
				GenericDataAccess.ExecuteNonQuery(this.comm);
				this._oTransporte_condicion.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
			} catch {
				throw;
			}
		}

		public override void udt()
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion");
				addParameters(3);
				GenericDataAccess.ExecuteNonQuery(this.comm);
			} catch {
				throw;
			}
		}

		public override void dlt()
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_condicion");
				addParameters(4);
				GenericDataAccess.ExecuteNonQuery(this.comm);
			} catch {
				throw;
			}
		}

		#endregion
    }
}
