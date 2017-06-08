using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog.personal
{
    public class Personal_registro
    {
        #region Campos
		protected int _id;
		protected int _id_personal;
		protected int _id_bodega;
		protected DateTime _fecha_hora;
		#endregion

		#region Propiedades
		public int Id { get { return _id; } set { _id = value; } } 
		public int Id_personal { get { return _id_personal; } set { _id_personal = value; } } 
		public int Id_bodega { get { return _id_bodega; } set { _id_bodega = value; } } 
		public DateTime Fecha_hora { get { return _fecha_hora; } set { _fecha_hora = value; } }
        public string Movimiento { get; set; }
		#endregion

		#region Constructores
		public Personal_registro()
		{
			this._id_personal = 0;
			this._id_bodega = 0;
		}
		#endregion
    }
}
