using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog.personal
{
    public class Personal_qr_pivote
    {
        #region Campos
        protected int _id;
        protected string _idf;
        protected int? _id_usuario;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Idf { get { return _idf; } set { _idf = value; } }
        public int? Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        #endregion

        #region Constructores
        public Personal_qr_pivote()
        {
            this._idf = String.Empty;
            this._id_usuario = null;
        }
        #endregion

    }
}
