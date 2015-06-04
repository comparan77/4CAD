using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    [Serializable]
    public class Documento
    {
        #region Campos
        protected int _id;
        protected string _nombre;
        protected string _mascara;
        private bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Mascara { get { return _mascara; } set { _mascara = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        #endregion

        #region Constructores
        public Documento()
        {
            this._nombre = string.Empty;
            this._mascara = string.Empty;
            this._IsActive = false;
        }
        #endregion

    }
}
