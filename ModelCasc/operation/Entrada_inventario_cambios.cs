using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Entrada_inventario_cambios
    {
        #region Campos
        protected int _id;
        protected int _id_entrada_inventario;
        protected int _id_usuario;
        protected string _codigo;
        protected string _orden;
        protected string _observaciones;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada_inventario { get { return _id_entrada_inventario; } set { _id_entrada_inventario = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        public string Codigo { get { return _codigo; } set { _codigo = value; } }
        public string Orden { get { return _orden; } set { _orden = value; } }
        public string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        #endregion

        #region Constructores
        public Entrada_inventario_cambios()
		{
			this._id_entrada_inventario = 0;
			this._id_usuario = 0;
			this._codigo = String.Empty;
			this._orden = String.Empty;
			this._observaciones = null;
		}
        #endregion
    }
}
