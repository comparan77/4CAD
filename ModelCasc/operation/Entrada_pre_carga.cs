using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Entrada_pre_carga
    {
        #region Campos
        protected int _id;
        protected int _id_transporte_tipo;
        protected string _referencia;
        protected string _operador;
        protected string _placa;
        protected string _caja;
        protected string _caja1;
        protected string _caja2;
        protected string _sello;
        protected string _observaciones;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_transporte_tipo { get { return _id_transporte_tipo; } set { _id_transporte_tipo = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public string Operador { get { return _operador; } set { _operador = value; } }
        public string Placa { get { return _placa; } set { _placa = value; } }
        public string Caja { get { return _caja; } set { _caja = value; } }
        public string Caja1 { get { return _caja1; } set { _caja1 = value; } }
        public string Caja2 { get { return _caja2; } set { _caja2 = value; } }
        public string Sello { get { return _sello; } set { _sello = value; } }
        public string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        #endregion

        #region Constructores
        public Entrada_pre_carga()
        {
            this._id_transporte_tipo = 0;
            this._referencia = String.Empty;
            this._operador = String.Empty;
            this._placa = String.Empty;
            this._caja = String.Empty;
            this._caja1 = String.Empty;
            this._caja2 = String.Empty;
            this._sello = String.Empty;
            this._observaciones = String.Empty;
        }
        #endregion
    }
}
