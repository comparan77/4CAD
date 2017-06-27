using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog.personal
{
    public class Personal_regla_rol
    {
        #region Campos
        protected int _id;
        protected int _id_personal_regla;
        protected int _id_personal_rol;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_personal_regla { get { return _id_personal_regla; } set { _id_personal_regla = value; } }
        public int Id_personal_rol { get { return _id_personal_rol; } set { _id_personal_rol = value; } }
        #endregion

        #region Constructores
        public Personal_regla_rol()
        {
            this._id_personal_regla = 0;
            this._id_personal_rol = 0;
        }
        #endregion
    }
}
