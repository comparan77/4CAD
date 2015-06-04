using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Pivote
    {
        #region Campos
        protected int _id;
        protected string _campo;
        protected string _tipo;
        protected string _campoxls;
        protected bool _requerido;
        protected string _campotbl;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Campo { get { return _campo; } set { _campo = value; } }
        public string Tipo { get { return _tipo; } set { _tipo = value; } }
        public string Campoxls { get { return _campoxls; } set { _campoxls = value; } }
        public bool Requerido { get { return _requerido; } set { _requerido = value; } }
        public string Campotbl { get { return _campotbl; } set { _campotbl = value; } }
        public bool ExisteCampo { get; set; }
        public int NumeroCampo { get; set; }
        #endregion

        #region Constructores
        public Pivote()
        {
            this._campo = String.Empty;
            this._tipo = String.Empty;
            this._campoxls = null;
            this._requerido = false;
            this._campotbl = String.Empty;
        }
        #endregion
    }
}
