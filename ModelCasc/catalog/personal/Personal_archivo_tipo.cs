using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog.personal
{
    public class Personal_archivo_tipo
    {
        #region Campos
        protected int _id;
        protected string _tipo;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Tipo { get { return _tipo; } set { _tipo = value; } }
        #endregion

        #region Constructores
        public Personal_archivo_tipo()
        {
            this._tipo = String.Empty;
        }
        #endregion

    }
}
