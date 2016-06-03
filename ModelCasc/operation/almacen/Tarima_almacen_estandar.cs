using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation.almacen
{
    public class Tarima_almacen_estandar
    {
        #region Campos
        protected int _id;
        protected int _id_entrada;
        protected int _id_tarima_almacen_proveedor;
        protected string _rr;
        protected int _cajasxtarima;
        protected int _piezasxcaja;
        protected string _proveedor;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada { get { return _id_entrada; } set { _id_entrada = value; } }
        public int Id_tarima_almacen_proveedor { get { return _id_tarima_almacen_proveedor; } set { _id_tarima_almacen_proveedor = value; } }
        public string Rr { get { return _rr; } set { _rr = value; } }
        public int Cajasxtarima { get { return _cajasxtarima; } set { _cajasxtarima = value; } }
        public int Piezasxcaja { get { return _piezasxcaja; } set { _piezasxcaja = value; } }
        public string Proveedor { get { return _proveedor; } set { _proveedor = value; } }
        #endregion

        #region Constructores
        public Tarima_almacen_estandar()
        {
            this._id_entrada = 0;
            this._id_tarima_almacen_proveedor = 0;
            this._rr = String.Empty;
            this._cajasxtarima = 0;
            this._piezasxcaja = 0;
            this._proveedor = String.Empty;
        }
        #endregion
    }
}
