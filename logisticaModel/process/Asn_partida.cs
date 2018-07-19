using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using logisticaModel.catalog;

namespace logisticaModel.process
{
    public class Asn_partida
    {
        #region Campos
        protected int _id;
        protected int _id_asn;
        protected string _sku;
        protected int _tarima;
        protected int _caja;
        protected int _pieza;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_asn { get { return _id_asn; } set { _id_asn = value; } }
        public string Sku { get { return _sku; } set { _sku = value; } }
        public int Tarima { get { return _tarima; } set { _tarima = value; } }
        public int Caja { get { return _caja; } set { _caja = value; } }
        public int Pieza { get { return _pieza; } set { _pieza = value; } }
        #endregion

        #region Constructores
        public Asn_partida()
        {
            this._id_asn = 0;
            this._sku = String.Empty;
            this._tarima = 0;
            this._caja = 0;
            this._pieza = 0;
        }
        #endregion

        public Cliente_mercancia PMercancia { get; set; }
    }
}
