using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.process
{
    public class Asn_transporte_sello
    {
        #region Campos
		protected int _id;
		protected int _id_asn;
		protected string _contenedor;
		protected string _sello;
		#endregion

		#region Propiedades
		public int Id { get { return _id; } set { _id = value; } } 
		public int Id_asn { get { return _id_asn; } set { _id_asn = value; } } 
		public string Contenedor { get { return _contenedor; } set { _contenedor = value; } } 
		public string Sello { get { return _sello; } set { _sello = value; } } 
		#endregion

		#region Constructores
		public Asn_transporte_sello()
		{
			this._id_asn = 0;
			this._contenedor = String.Empty;
			this._sello = String.Empty;
		}
		#endregion
    }
}
