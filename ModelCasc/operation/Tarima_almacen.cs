using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Tarima_almacen
    {
        #region Campos
        protected int _id;
        protected int _id_entrada;
        protected string _folio;
        protected string _mercancia_codigo;
        protected string _mercancia_nombre;
        protected string _rr;
        protected string _estandar;
        protected int _bultos;
        protected int _piezas;
        protected int? _id_salida;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada { get { return _id_entrada; } set { _id_entrada = value; } }
        public string Folio { get { return _folio; } set { _folio = value; } }
        public string Mercancia_codigo { get { return _mercancia_codigo; } set { _mercancia_codigo = value; } }
        public string Mercancia_nombre { get { return _mercancia_nombre; } set { _mercancia_nombre = value; } }
        public string Rr { get { return _rr; } set { _rr = value; } }
        public string Estandar { get { return _estandar; } set { _estandar = value; } }
        public int Bultos { get { return _bultos; } set { _bultos = value; } }
        public int Piezas { get { return _piezas; } set { _piezas = value; } }
        public int? Id_salida { get { return _id_salida; } set { _id_salida = value; } }

        /// <summary>
        /// Para autocomplete de jquery
        /// </summary>
        //public string label { get; set; }
        //public string value { get; set; }
        #endregion

        #region Constructores
        public Tarima_almacen()
        {
            this._id_entrada = 0;
            this._folio = String.Empty;
            this._mercancia_codigo = String.Empty;
            this._mercancia_nombre = String.Empty;
            this._rr = String.Empty;
            this._estandar = String.Empty;
            this._bultos = 0;
            this._piezas = 0;
            this._id_salida = null;
        }
        #endregion
    }
}
