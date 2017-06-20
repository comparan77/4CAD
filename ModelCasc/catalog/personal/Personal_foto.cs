using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog.personal
{
    public class Personal_foto
    {
        #region Campos
        protected int _id;
        protected string _foto;
        protected int _id_usuario;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Foto { get { return _foto; } set { _foto = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        #endregion

        #region Constructores
        public Personal_foto()
        {
            this._foto = String.Empty;
            this._id_usuario = 0;
        }
        #endregion
    }
}
