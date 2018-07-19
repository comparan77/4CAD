using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using logisticaModel.operation.warehouse;

namespace logisticaModel.process
{
    public class Asn
    {
        #region Campos
        protected int _id;
        protected int _id_cliente;
        protected string _folio;
        protected string _referencia;
        protected int? _id_bodega;
        protected DateTime? _fecha_hora;
        protected int? _id_transporte;
        protected string _operador;
        protected int? _pallet;
        protected int? _caja;
        protected int? _pieza;
        protected bool _descargada;
        protected DateTime? _fecha_hora_descarga;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_cliente { get { return _id_cliente; } set { _id_cliente = value; } }
        public string Folio { get { return _folio; } set { _folio = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public int? Id_bodega { get { return _id_bodega; } set { _id_bodega = value; } }
        public DateTime? Fecha_hora { get { return _fecha_hora; } set { _fecha_hora = value; } }
        public int? Id_transporte { get { return _id_transporte; } set { _id_transporte = value; } }
        public string Operador { get { return _operador; } set { _operador = value; } }
        public int? Pallet { get { return _pallet; } set { _pallet = value; } }
        public int? Caja { get { return _caja; } set { _caja = value; } }
        public int? Pieza { get { return _pieza; } set { _pieza = value; } }
        public bool Descargada { get { return _descargada; } set { _descargada = value; } }
        public DateTime? Fecha_hora_descarga { get { return _fecha_hora_descarga; } set { _fecha_hora_descarga = value; } }
        #endregion

        #region Constructores
        public Asn()
        {
            this._id_cliente = 0;
            this._folio = String.Empty;
            this._referencia = null;
            this._id_bodega = null;
            this._fecha_hora = null;
            this._id_transporte = null;
            this._operador = null;
            this._pallet = null;
            this._caja = null;
            this._pieza = null;
            this._descargada = false;
            this._fecha_hora_descarga = null;
        }
        #endregion

        public List<Asn_partida> PLstPartida { get; set; }
        public string ClienteNombre { get; set; }
        public string BodegaNombre { get; set; }
        public string TransporteNombre { get; set; }
        public Cortina_disponible PCortinaAsignada { get; set; }
        public string CortinaNombre { get; set; }
        public List<Asn_transporte_sello> PLstTranSello { get; set; }
        public List<Entrada> PLstEntrada { get; set; }
    }
}
