using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.catalog
{
    public class Transporte
    {
        #region Campos
		protected int _id;
		protected string _nombre;
		protected bool _IsActive;
		#endregion

		#region Propiedades
		public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } } 
		public bool IsActive { get { return _IsActive; } set { _IsActive = value; } } 
		#endregion

		#region Constructores
		public Transporte()
		{
            this._nombre = string.Empty;
			this._IsActive = false;
		}
		#endregion
    }
}
