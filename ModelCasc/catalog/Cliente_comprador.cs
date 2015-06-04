using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Cliente_comprador: Catalogo
    {
        #region Campos
        protected int _id_cliente_grupo;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_cliente_grupo { get { return _id_cliente_grupo; } set { _id_cliente_grupo = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        #endregion

        #region Constructores
        public Cliente_comprador()
        {
            this._id_cliente_grupo = 0;
            this._nombre = String.Empty;
            this._IsActive = false;
        }
        #endregion
    }
}
