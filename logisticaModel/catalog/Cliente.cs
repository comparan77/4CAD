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
        protected int _numero;
        protected bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Rfc { get { return _rfc; } set { _rfc = value; } }
        public string Razon { get { return _razon; } set { _razon = value; } }
        public int Numero { get { return _numero; } set { _numero = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        #endregion

        #region Constructores
        public Cliente()
        {
            this._nombre = String.Empty;
            this._rfc = String.Empty;
            this._razon = String.Empty;
            this._numero = 0;
            this._IsActive = false;
        }
        #endregion

        public float ProformaPorAplicarTotal { get; set; }
        public List<Cliente_regimen> PLstCteReg { get; set; }
    }
}
