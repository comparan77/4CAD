using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.catalog
{
    public class Cliente
    {
        #region Campos
        protected int _id;
        protected string _nombre;
        protected string _rfc;
        protected string _razon;
        protected bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Rfc { get { return _rfc; } set { _rfc = value; } }
        public string Razon { get { return _razon; } set { _razon = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        public float ProformaPorAplicarTotal { get; set; }
        #endregion

        #region Constructores
        public Cliente()
        {
            this._nombre = String.Empty;
            this._rfc = String.Empty;
            this._razon = String.Empty;
            this._IsActive = false;
        }
        #endregion
    }
}
