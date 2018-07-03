using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.catalog
{
    public class Servicio_periodo
    {
    #region Campos
		protected int _id;
		protected string _nombre;
		#endregion

		#region Propiedades
		public int Id { get { return _id; } set { _id = value; } } 
		public string Nombre { get { return _nombre; } set { _nombre = value; } } 
		#endregion

		#region Constructores
		public Servicio_periodo()
		{
			this._nombre = String.Empty;
		}
		#endregion
    }
}
