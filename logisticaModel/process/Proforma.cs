using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.process
{
    public class Proforma
    {
        #region Campos
        protected Int64 _id;
        protected DateTime _fecha_recibo;
        protected string _cliente;
        protected int _id_cliente;
        protected string _referencia;
        protected string _sid;
        protected string _sku;
        protected string _mercancia;
        protected string _serielote;
        protected string _calidad;
        protected int _id_servicio;
        protected DateTime _fecha_servicio;
        protected string _nombre_servicio;
        protected int _dias_servicio;
        protected float _costo_servicio;
        protected DateTime? _fecha_expedicion;
        protected int? _entradas;
        protected int? _salidas;
        protected int? _saldo;
        protected float _valor_mercancia;
        protected int _cantidad;
        protected float _total;
        protected bool _aplicada;
        protected string _folio_aplicada;
        protected DateTime _corte_ini;
        protected DateTime _corte_fin;

        #endregion

        #region Propiedades
        public Int64 Id { get { return _id; } set { _id = value; } }
        public DateTime Fecha_recibo { get { return _fecha_recibo; } set { _fecha_recibo = value; } }
        public string Cliente { get { return _cliente; } set { _cliente = value; } }
        public int Id_cliente { get { return _id_cliente; } set { _id_cliente = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public string Sid { get { return _sid; } set { _sid = value; } }
        public string Sku { get { return _sku; } set { _sku = value; } }
        public string Mercancia { get { return _mercancia; } set { _mercancia = value; } }
        public string Serielote { get { return _serielote; } set { _serielote = value; } }
        public string Calidad { get { return _calidad; } set { _calidad = value; } }
        public int Id_servicio { get { return _id_servicio; } set { _id_servicio = value; } }
        public DateTime Fecha_servicio { get { return _fecha_servicio; } set { _fecha_servicio = value; } }
        public string Nombre_servicio { get { return _nombre_servicio; } set { _nombre_servicio = value; } }
        public int Dias_servicio { get { return _dias_servicio; } set { _dias_servicio = value; } }
        public float Costo_servicio { get { return _costo_servicio; } set { _costo_servicio = value; } }
        public DateTime? Fecha_expedicion { get { return _fecha_expedicion; } set { _fecha_expedicion = value; } }
        public int? Entradas { get { return _entradas; } set { _entradas = value; } }
        public int? Salidas { get { return _salidas; } set { _salidas = value; } }
        public int? Saldo { get { return _saldo; } set { _saldo = value; } }
        public float Valor_mercancia { get { return _valor_mercancia; } set { _valor_mercancia = value; } }
        public int Cantidad { get { return _cantidad; } set { _cantidad = value; } }
        public float Total { get { return _total; } set { _total = value; } }
        public bool Aplicada { get { return _aplicada; } set { _aplicada = value; } }
        public string Folio_aplicada { get { return _folio_aplicada; } set { _folio_aplicada = value; } }
        public DateTime Corte_ini { get { return _corte_ini; } set { _corte_ini = value; } }
        public DateTime Corte_fin { get { return _corte_fin; } set { _corte_fin = value; } }
        #endregion

        #region Constructores
        public Proforma()
        {
            this._fecha_recibo = default(DateTime);
            this._cliente = String.Empty;
            this._id_cliente = 0;
            this._referencia = String.Empty;
            this._sid = String.Empty;
            this._sku = String.Empty;
            this._mercancia = String.Empty;
            this._serielote = String.Empty;
            this._calidad = String.Empty;
            this._id_servicio = 0;
            this._fecha_servicio = default(DateTime);
            this._nombre_servicio = String.Empty;
            this._dias_servicio = 0;
            this._costo_servicio = 0;
            this._fecha_expedicion = null;
            this._entradas = null;
            this._salidas = null;
            this._saldo = null;
            this._valor_mercancia = 0;
            this._cantidad = 0;
            this._total = 0;
            this._aplicada = false;
            this._folio_aplicada = string.Empty;
            this._corte_ini = default(DateTime);
            this._corte_fin = default(DateTime);
        }
        #endregion
    }
}
