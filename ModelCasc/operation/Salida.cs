using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;

namespace ModelCasc.operation
{
    public class Salida
    {
        #region Campos
        protected int _id;
        protected int _id_bodega;
        protected string _folio;
        protected string _folio_indice;
        protected DateTime _fecha;
        protected string _hora_salida;
        protected int _id_cortina;
        protected int _id_cliente;
        protected string _referencia;
        protected string _destino;
        protected string _mercancia;
        protected int _id_transporte;
        protected int _id_transporte_tipo;
        protected string _placa;
        protected string _caja1;
        protected string _caja2;
        protected string _sello;
        protected string _talon;
        protected int _id_custodia;
        protected string _operador;
        protected int _no_pallet;
        protected int _no_bulto;
        protected int _no_pieza;
        protected double _peso_unitario;
        protected double _total_carga;
        protected bool _es_unica;
        protected string _hora_carga;
        protected string _vigilante;
        protected string _observaciones;
        protected string _motivo_cancelacion;
        protected bool _isActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_bodega { get { return _id_bodega; } set { _id_bodega = value; } }
        public string Folio { get { return _folio; } set { _folio = value; } }
        public string Folio_indice { get { return _folio_indice; } set { _folio_indice = value; } }
        public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public string Hora_salida { get { return _hora_salida; } set { _hora_salida = value; } }
        public int Id_cortina { get { return _id_cortina; } set { _id_cortina = value; } }
        public int Id_cliente { get { return _id_cliente; } set { _id_cliente = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public string Destino { get { return _destino; } set { _destino = value; } }
        public string Mercancia { get { return _mercancia; } set { _mercancia = value; } }
        public int Id_transporte { get { return _id_transporte; } set { _id_transporte = value; } }
        public int Id_transporte_tipo { get { return _id_transporte_tipo; } set { _id_transporte_tipo = value; } }
        public string Placa { get { return _placa; } set { _placa = value; } }
        public string Caja1 { get { return _caja1; } set { _caja1 = value; } }
        public string Caja2 { get { return _caja2; } set { _caja2 = value; } }
        public string Sello { get { return _sello; } set { _sello = value; } }
        public string Talon { get { return _talon; } set { _talon = value; } }
        public int Id_custodia { get { return _id_custodia; } set { _id_custodia = value; } }
        public string Operador { get { return _operador; } set { _operador = value; } }
        public int No_pallet { get { return _no_pallet; } set { _no_pallet = value; } }
        public int No_bulto { get { return _no_bulto; } set { _no_bulto = value; } }
        public int No_pieza { get { return _no_pieza; } set { _no_pieza = value; } }
        public double Peso_unitario { get { return _peso_unitario; } set { _peso_unitario = value; } }
        public double Total_carga { get { return _total_carga; } set { _total_carga = value; } }
        public bool Es_unica { get { return _es_unica; } set { _es_unica = value; } }
        public string Hora_carga { get { return _hora_carga; } set { _hora_carga = value; } }
        public string Vigilante { get { return _vigilante; } set { _vigilante = value; } }
        public string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public string Motivo_cancelacion { get { return _motivo_cancelacion; } set { _motivo_cancelacion = value; } }
        public bool IsActive { get { return _isActive; } set { _isActive = value; } }

        public string FolioSalida { get; set; }

        public Usuario PUsuario { get; set; }
        public Bodega PBodega { get; set; }
        public Cortina PCortina { get; set; }
        public Cliente PCliente { get; set; }
        public bool EsConsolidada { get; set; }
        public Salida_parcial PSalPar { get; set; }
        public List<Salida_documento> PLstSalDoc { get; set; }
        public List<Salida_compartida> PLstSalComp { get; set; }
        public Transporte PTransporte { get; set; }
        public Transporte_tipo PTransporteTipo { get; set; }
        public Custodia PCustodia { get; set; }
        public int Id_salida_orden_carga { get; set; }
        #endregion

        #region Constructores
        public Salida()
        {
            this._id_bodega = 0;
            this._folio = String.Empty;
            this._fecha = new DateTime(1, 1, 1);
            this._hora_salida = String.Empty;
            this._id_cortina = 0;
            this._id_cliente = 0;
            this._referencia = string.Empty;
            this._destino = string.Empty;
            this._mercancia = string.Empty;
            this._id_transporte = 0;
            this._id_transporte_tipo = 0;
            this._id_custodia = 0;
            this._operador = string.Empty;
            this._no_pallet = 0;
            this._no_bulto = 0;
            this._no_pieza = 0;
            this._peso_unitario = 0;
            this._total_carga = 0;
            this._es_unica = false;
            this._hora_carga = string.Empty;
            this._vigilante = string.Empty;
            this._observaciones = string.Empty;
            this._motivo_cancelacion = string.Empty;
            this._isActive = false;
        }
        #endregion
    }
}
