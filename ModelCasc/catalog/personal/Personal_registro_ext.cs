using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog.personal
{
    public class Personal_registro_ext
    {
        #region Campos
        protected int _id;
        protected int _id_personal_registro;
        protected string _motivo;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_personal_registro { get { return _id_personal_registro; } set { _id_personal_registro = value; } }
        public string Motivo { get { return _motivo; } set { _motivo = value; } }
        #endregion

        #region Constructores
        public Personal_registro_ext()
        {
            this._id_personal_registro = 0;
            this._motivo = String.Empty;
        }
        #endregion
    }
}
