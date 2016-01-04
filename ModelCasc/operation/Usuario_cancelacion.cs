using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Usuario_cancelacion
    {
        #region Campos
        protected int _id;
        protected int _id_usuario;
        protected string _folio_operacion;
        protected string _motivo_cancelacion;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        public string Folio_operacion { get { return _folio_operacion; } set { _folio_operacion = value; } }
        public string Motivo_cancelacion { get { return _motivo_cancelacion; } set { _motivo_cancelacion = value; } }
        #endregion

        #region Constructores
        public Usuario_cancelacion()
		{
			this._id_usuario = 0;
			this._folio_operacion = String.Empty;
			this._motivo_cancelacion = String.Empty;
		}
        #endregion
    }
}
