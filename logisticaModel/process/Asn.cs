using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.process
{
    public class Asn
    {
        #region Campos
        protected int _id;
        protected string _folio;
        protected int? _id_bodega;
        protected DateTime? _fecha;
        protected int? _id_transporte;
        protected string _sello;
        protected string _operador;
        protected int? _pallet;
        protected int? _caja;
        protected int? _pieza;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Folio { get { return _folio; } set { _folio = value; } }
        public int? Id_bodega { get { return _id_bodega; } set { _id_bodega = value; } }
        public DateTime? Fecha { get { return _fecha; } set { _fecha = value; } }
        public int? Id_transporte { get { return _id_transporte; } set { _id_transporte = value; } }
        public string Sello { get { return _sello; } set { _sello = value; } }
        public string Operador { get { return _operador; } set { _operador = value; } }
        public int? Pallet { get { return _pallet; } set { _pallet = value; } }
        public int? Caja { get { return _caja; } set { _caja = value; } }
        public int? Pieza { get { return _pieza; } set { _pieza = value; } }
        public List<Asn_partida> PLstPartida { get; set; }
        #endregion

        #region Constructores
        public Asn()
        {
            this._folio = String.Empty;
            this._id_bodega = null;
            this._fecha = null;
            this._id_transporte = null;
            this._sello = null;
            this._operador = null;
            this._pallet = null;
            this._caja = null;
            this._pieza = null;
        }
        #endregion
    }
}
