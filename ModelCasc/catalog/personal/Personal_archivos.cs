using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog.personal
{
    public class Personal_archivos
    {
        #region Campos
        protected int _id;
        protected int _id_personal;
        protected string _ruta_foto;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_personal { get { return _id_personal; } set { _id_personal = value; } }
        public string Ruta_foto { get { return _ruta_foto; } set { _ruta_foto = value; } }
        #endregion

        #region Constructores
        public Personal_archivos()
        {
            this._id_personal = 0;
            this._ruta_foto = String.Empty;
        }
        #endregion
    }
}
