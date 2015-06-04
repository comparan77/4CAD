using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Transporte_tipo
    {
        #region Campos
        protected int _id;
        protected string _nombre;
        protected int _peso_maximo;
        protected bool _requiere_placa;
        protected bool _requiere_caja;
        protected bool _requiere_caja1;
        protected bool _requiere_caja2;
        private bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public int Peso_maximo { get { return _peso_maximo; } set { _peso_maximo = value; } }
        public bool Requiere_placa { get { return _requiere_placa; } set { _requiere_placa = value; } }
        public bool Requiere_caja { get { return _requiere_caja; } set { _requiere_caja = value; } }
        public bool Requiere_caja1 { get { return _requiere_caja1; } set { _requiere_caja1 = value; } }
        public bool Requiere_caja2 { get { return _requiere_caja2; } set { _requiere_caja2 = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        #endregion

        #region Constructores
        public Transporte_tipo()
        {
            this._nombre = String.Empty;
            this._peso_maximo = 0;
            this._requiere_placa = false;
            this._requiere_caja = false;
            this._requiere_caja1 = false;
            this._requiere_caja2 = false;
            this._IsActive = false;
        }
        #endregion
    }
}
