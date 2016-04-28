using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation.almacen
{
    public class Tarima_almacen_resto
    {
        #region Campos
        protected int _id;
        protected int _id_tarima_almacen;
        protected int _cajas;
        protected int _piezasxcaja;
        protected int _piezas;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_tarima_almacen { get { return _id_tarima_almacen; } set { _id_tarima_almacen = value; } }
        public int Cajas { get { return _cajas; } set { _cajas = value; } }
        public int Piezasxcaja { get { return _piezasxcaja; } set { _piezasxcaja = value; } }
        public int Piezas { get { return _piezas; } set { _piezas = value; } }
        #endregion

        #region Constructores
        public Tarima_almacen_resto()
        {
            this._id_tarima_almacen = 0;
            this._cajas = 0;
            this._piezasxcaja = 0;
            this._piezas = 0;
        }
        #endregion
    }
}
