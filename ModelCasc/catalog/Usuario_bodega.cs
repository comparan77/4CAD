using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Usuario_bodega
    {
        #region Campos
        protected int _id;
        protected int _id_usuario;
        protected int _id_bodega;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        public int Id_bodega { get { return _id_bodega; } set { _id_bodega = value; } }
        #endregion

        #region Constructores
        public Usuario_bodega()
        {
            this._id_usuario = 0;
            this._id_bodega = 0;
        }
        #endregion
    }
}
