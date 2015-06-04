using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    [Serializable]
    public class Entrada_transporte
    {
        #region Campos
        protected int _id;
        protected int _id_entrada;
        protected string _transporte_linea;
        protected int _id_transporte_tipo;
        protected string _placa;
        protected string _caja;
        protected string _caja1;
        protected string _caja2;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada { get { return _id_entrada; } set { _id_entrada = value; } }
        public string Transporte_linea { get { return _transporte_linea; } set { _transporte_linea = value; } }
        public int Id_transporte_tipo { get { return _id_transporte_tipo; } set { _id_transporte_tipo = value; } }
        public string Placa { get { return _placa; } set { _placa = value; } }
        public string Caja { get { return _caja; } set { _caja = value; } }
        public string Caja1 { get { return _caja1; } set { _caja1 = value; } }
        public string Caja2 { get { return _caja2; } set { _caja2 = value; } }
        public string Transporte_tipo { get; set; }
        #endregion

        #region Constructores
        public Entrada_transporte()
        {
            this._id_entrada = 0;
            this._transporte_linea = String.Empty;
            this._id_transporte_tipo = 0;
            this._placa = String.Empty;
            this._caja = String.Empty;
            this._caja1 = String.Empty;
            this._caja2 = String.Empty;
        }
        #endregion
    }
}
