using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.process
{
    internal class Asn_transporte_selloMng: Crud
    {
        #region Campos
		protected Asn_transporte_sello _oAsn_transporte_sellos;
		protected List<Asn_transporte_sello> _lst;
		#endregion

		#region Propiedades
		public Asn_transporte_sello O_Asn_transporte_sellos { get { return _oAsn_transporte_sellos; } set { _oAsn_transporte_sellos = value; } }
		public List<Asn_transporte_sello> Lst { get { return _lst; } set { _lst = value; } }
		#endregion

		#region Constructores
		public Asn_transporte_selloMng()
		{
			this._oAsn_transporte_sellos = new Asn_transporte_sello();
			this._lst = new List<Asn_transporte_sello>();
		}
		#endregion

		#region Metodos
		protected override void addParameters(int opcion)
		{
			GenericDataAccess.AddInParameter(this.comm,"?P_opcion", DbType.Int32, opcion);
			GenericDataAccess.AddInOutParameter(this.comm,"?P_id", DbType.Int32, this._oAsn_transporte_sellos.Id);
			GenericDataAccess.AddInParameter(this.comm,"?P_id_asn", DbType.Int32, this._oAsn_transporte_sellos.Id_asn);
			GenericDataAccess.AddInParameter(this.comm,"?P_contenedor", DbType.String, this._oAsn_transporte_sellos.Contenedor);
			GenericDataAccess.AddInParameter(this.comm,"?P_sello", DbType.String, this._oAsn_transporte_sellos.Sello);
		}

		protected void BindByDataRow(DataRow dr, Asn_transporte_sello o)
		{
			try {
				int.TryParse(dr["id"].ToString(), out entero);
				o.Id = entero;
				entero = 0;
				if (dr["id_asn"] != DBNull.Value)
				{
					int.TryParse(dr["id_asn"].ToString(), out entero);
					o.Id_asn = entero;
					entero = 0;
				}
				o.Contenedor = dr["contenedor"].ToString();
				o.Sello = dr["sello"].ToString();
			} catch  {
				throw;
			}
		}

		public override void fillLst(IDbTransaction trans = null)
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Asn_transporte_sello");
				addParameters(0);
				if (trans == null)
					this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
				else
					this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
				this._lst = new List<Asn_transporte_sello>();
				foreach(DataRow dr in dt.Rows)
				{
					Asn_transporte_sello o = new Asn_transporte_sello();
					BindByDataRow(dr, o);
					this._lst.Add(o);
				}
			} catch  {
				throw;
			}
		}

		public override void selById(IDbTransaction trans = null)
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Asn_transporte_sello");
				addParameters(1);
				if (trans == null)
					this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
				else
					this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
				if(dt.Rows.Count == 1)
				{
					DataRow dr = dt.Rows[0];
					BindByDataRow(dr, this._oAsn_transporte_sellos);
				}
				else if(dt.Rows.Count > 1)
					throw new Exception("Error de integridad");
				else
					throw new Exception("No existe información para el registro solicitado");
			} catch {
				throw;
			}
		}

		public override void add(IDbTransaction trans = null)
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Asn_transporte_sello");
				addParameters(2);
				if (trans == null)
					GenericDataAccess.ExecuteNonQuery(this.comm);
				else
					GenericDataAccess.ExecuteNonQuery(this.comm, trans);
				this._oAsn_transporte_sellos.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
			} catch {
				throw;
			}
		}

		public override void udt(IDbTransaction trans = null)
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Asn_transporte_sello");
				addParameters(3);
				if (trans == null)
					GenericDataAccess.ExecuteNonQuery(this.comm);
				else
					GenericDataAccess.ExecuteNonQuery(this.comm, trans);
			} catch {
				throw;
			}
		}

		public override void dlt(IDbTransaction trans = null)
		{
			try {
				this.comm = GenericDataAccess.CreateCommandSP("sp_Asn_transporte_sello");
				addParameters(4);
				if (trans == null)
					GenericDataAccess.ExecuteNonQuery(this.comm);
				else
					GenericDataAccess.ExecuteNonQuery(this.comm, trans);
			} catch {
				throw;
			}
		}

        public void fillLstByAsn(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Asn_transporte_sello");
                addParameters(5);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Asn_transporte_sello>();
                foreach (DataRow dr in dt.Rows)
                {
                    Asn_transporte_sello o = new Asn_transporte_sello();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

		#endregion
    }
}
