using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;
using ModelCasc.operation.almacen;

namespace ModelCasc.operation
{
    [Serializable]
    public class Entrada: IDocumentoEncontrado
    {
        #region Campos
        protected int _id;
        protected int _id_bodega;
        protected string _bodega;
        protected string _folio;
        protected string _folio_indice;
        protected DateTime _fecha;
        protected string _hora;
        protected int _id_cortina;
        protected string _cortina;
        protected int _id_cliente;
        protected string _cliente;
        protected string _referencia;
        protected string _origen;
        protected string _mercancia;
        protected string _sello;
        protected string _talon;
        protected int _id_custodia;
        protected string _custodia;
        protected string _operador;
        protected string _transporte_linea;
        protected string _transporte_tipo;
        protected string _placa;
        protected string _caja;
        protected string _caja1;
        protected string _caja2;
        protected int _no_caja_cinta_aduanal;
        protected int _no_pallet;
        protected int _no_bulto_danado;
        protected int _no_bulto_abierto;
        protected int _no_bulto_declarado;
        protected int _no_pieza_declarada;
        protected int _no_bulto_recibido;
        protected int _no_pieza_recibida;
        protected bool _es_unica;
        protected string _hora_descarga;
        protected string _vigilante;
        protected string _observaciones;
        protected string _motivo_cancelacion;
        protected string _codigo;
        protected int _id_tipo_carga;
        protected string _tipo_carga;
        protected bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_bodega { get { return _id_bodega; } set { _id_bodega = value; } }
        public string Bodega { get { return _bodega; } set { _bodega = value; } }
        public string Folio { get { return _folio; } set { _folio = value; } }
        public string Folio_indice { get { return _folio_indice; } set { _folio_indice = value; } }
        public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public string Hora { get { return _hora; } set { _hora = value; } }
        public int Id_cortina { get { return _id_cortina; } set { _id_cortina = value; } }
        public string Cortina { get { return _cortina; } set { _cortina = value; } }
        public int Id_cliente { get { return _id_cliente; } set { _id_cliente = value; } }
        public string Cliente { get { return _cliente; } set { _cliente = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public string Origen { get { return _origen; } set { _origen = value; } }
        public string Mercancia { get { return _mercancia; } set { _mercancia = value; } }
        public string Sello { get { return _sello; } set { _sello = value; } }
        public string Talon { get { return _talon; } set { _talon = value; } }
        public int Id_custodia { get { return _id_custodia; } set { _id_custodia = value; } }
        public string Custodia { get { return _custodia; } set { _custodia = value; } }
        public string Operador { get { return _operador; } set { _operador = value; } }
        public string Transporte_linea { get { return _transporte_linea; } set { _transporte_linea = value; } }
        public string Transporte_tipo { get { return _transporte_tipo; } set { _transporte_tipo = value; } }
        public string Placa { get { return _placa; } set { _placa = value; } }
        public string Caja { get { return _caja; } set { _caja = value; } }
        public string Caja1 { get { return _caja1; } set { _caja1 = value; } }
        public string Caja2 { get { return _caja2; } set { _caja2 = value; } }
        public int No_caja_cinta_aduanal { get { return _no_caja_cinta_aduanal; } set { _no_caja_cinta_aduanal = value; } }
        public int No_pallet { get { return _no_pallet; } set { _no_pallet = value; } }
        public int No_bulto_danado { get { return _no_bulto_danado; } set { _no_bulto_danado = value; } }
        public int No_bulto_abierto { get { return _no_bulto_abierto; } set { _no_bulto_abierto = value; } }
        public int No_bulto_declarado { get { return _no_bulto_declarado; } set { _no_bulto_declarado = value; } }
        public int No_pieza_declarada { get { return _no_pieza_declarada; } set { _no_pieza_declarada = value; } }
        public int No_bulto_recibido { get { return _no_bulto_recibido; } set { _no_bulto_recibido = value; } }
        public int No_pieza_recibida { get { return _no_pieza_recibida; } set { _no_pieza_recibida = value; } }
        public bool Es_unica { get { return _es_unica; } set { _es_unica = value; } }
        public string Hora_descarga { get { return _hora_descarga; } set { _hora_descarga = value; } }
        public string Vigilante { get { return _vigilante; } set { _vigilante = value; } }
        public string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public string Motivo_cancelacion { get { return _motivo_cancelacion; } set { _motivo_cancelacion = value; } }
        public string Codigo { get { return _codigo; } set { _codigo = value; } }
        public int Id_tipo_carga { get { return _id_tipo_carga; } set { _id_tipo_carga = value; } }
        public string Tipo_carga { get { return _tipo_carga; } set { _tipo_carga = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }

        public int IdEntrada { get { return this._id; } set { this._id = value; } }
        public string FolioEntrada { get; set ; }
        public string Ubicacion { get; set; }
        public string ClienteNombre { get; set; }

        public Usuario PUsuario { get; set; }
        public Bodega PBodega { get; set; }
        public Cortina PCortina { get; set; }
        public Cliente PCliente { get; set; }
        public List<Entrada_compartida> PLstEntComp { get; set; }
        public List<Entrada_documento> PLstEntDoc { get; set; }
        public List<Entrada_parcial> PLstEntPar { get; set; }
        public List<Entrada_transporte> PLstEntTrans { get; set; }
        public List<Entrada_transporte_condicion> PLstEntTransCond { get; set; }
        public Entrada_parcial PEntPar { get; set; }
        public Custodia PCustodia { get; set; }
        public Tipo_carga PTipoCarga { get; set; }
        public bool EsConsolidada { get; set; }
        public Entrada_inventario PEntInv { get; set; }
        public bool ConFondeo { get; set; }
        public List<Tarima_almacen> PLstTarAlm { get; set; }
        public Tarima_almacen_estandar PTarAlmEstd { get; set; }
        public List<Cliente_copia> PLstCCopia { get; set; }
        #endregion

        #region Constructores
        public Entrada()
        {
            this._id_bodega = 0;
            this._bodega = String.Empty;
            this._folio = String.Empty;
            this._folio_indice = null;
            this._fecha = default(DateTime);
            this._hora = String.Empty;
            this._id_cortina = 0;
            this._cortina = String.Empty;
            this._id_cliente = 0;
            this._cliente = String.Empty;
            this._referencia = String.Empty;
            this._origen = String.Empty;
            this._mercancia = String.Empty;
            this._sello = null;
            this._talon = null;
            this._id_custodia = 0;
            this._custodia = String.Empty;
            this._operador = String.Empty;
            this._transporte_linea = String.Empty;
            this._transporte_tipo = String.Empty;
            this._placa = String.Empty;
            this._caja = String.Empty;
            this._caja1 = String.Empty;
            this._caja2 = String.Empty;
            this._no_caja_cinta_aduanal = 0;
            this._no_pallet = 0;
            this._no_bulto_danado = 0;
            this._no_bulto_abierto = 0;
            this._no_bulto_declarado = 0;
            this._no_pieza_declarada = 0;
            this._no_bulto_recibido = 0;
            this._no_pieza_recibida = 0;
            this._es_unica = false;
            this._hora_descarga = String.Empty;
            this._vigilante = String.Empty;
            this._observaciones = null;
            this._motivo_cancelacion = null;
            this._codigo = null;
            this._id_tipo_carga = 0;
            this._tipo_carga = String.Empty;
            this._IsActive = false;
        }
        #endregion


        //#region Campos
        //protected int _id;
        //protected int _id_bodega;
        //protected string _folio;
        //protected string _folio_indice;
        //protected DateTime _fecha;
        //protected string _hora;
        //protected int _id_cortina;
        //protected int _id_cliente;
        //protected string _referencia;
        //protected string _origen;
        //protected string _mercancia;
        //protected string _sello;
        //protected string _talon;
        //protected int _id_custodia;
        //protected string _operador;
        //protected int _no_caja_cinta_aduanal;
        //protected int _no_pallet;
        //protected int _no_bulto_danado;
        //protected int _no_bulto_abierto;
        //protected int _no_bulto_declarado;
        //protected int _no_pieza_declarada;
        //protected int _no_bulto_recibido;
        //protected int _no_pieza_recibida;
        //protected bool _es_unica;
        //protected string _hora_descarga;
        //protected string _vigilante;
        //protected string _observaciones;
        //protected string _motivo_cancelacion;
        //protected string _codigo;
        //protected int _id_tipo_carga;
        //protected bool _isActive;
        //#endregion

        //#region Propiedades
        //public int Id { get { return _id; } set { _id = value; } }
        //public int Id_bodega { get { return _id_bodega; } set { _id_bodega = value; } }
        //public string Folio { get { return _folio; } set { _folio = value; } }
        //public string Folio_indice { get { return _folio_indice; } set { _folio_indice = value; } }
        //public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        //public string Hora { get { return _hora; } set { _hora = value; } }
        //public int Id_cortina { get { return _id_cortina; } set { _id_cortina = value; } }
        //public int Id_cliente { get { return _id_cliente; } set { _id_cliente = value; } }
        //public string Referencia { get { return _referencia; } set { _referencia = value; } }
        //public string Origen { get { return _origen; } set { _origen = value; } }
        //public string Mercancia { get { return _mercancia; } set { _mercancia = value; } }
        //public string Sello { get { return _sello; } set { _sello = value; } }
        //public string Talon { get { return _talon; } set { _talon = value; } }
        //public int Id_custodia { get { return _id_custodia; } set { _id_custodia = value; } }
        //public string Operador { get { return _operador; } set { _operador = value; } }
        //public int No_caja_cinta_aduanal { get { return _no_caja_cinta_aduanal; } set { _no_caja_cinta_aduanal = value; } }
        //public int No_pallet { get { return _no_pallet; } set { _no_pallet = value; } }
        //public int No_bulto_danado { get { return _no_bulto_danado; } set { _no_bulto_danado = value; } }
        //public int No_bulto_abierto { get { return _no_bulto_abierto; } set { _no_bulto_abierto = value; } }
        //public int No_bulto_declarado { get { return _no_bulto_declarado; } set { _no_bulto_declarado = value; } }
        //public int No_pieza_declarada { get { return _no_pieza_declarada; } set { _no_pieza_declarada = value; } }
        //public int No_bulto_recibido { get { return _no_bulto_recibido; } set { _no_bulto_recibido = value; } }
        //public int No_pieza_recibida { get { return _no_pieza_recibida; } set { _no_pieza_recibida = value; } }
        //public bool Es_unica { get { return _es_unica; } set { _es_unica = value; } }
        //public string Hora_descarga { get { return _hora_descarga; } set { _hora_descarga = value; } }
        //public string Vigilante { get { return _vigilante; } set { _vigilante = value; } }
        //public string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        //public string Motivo_cancelacion { get { return _motivo_cancelacion; } set { _motivo_cancelacion = value; } }
        ///// <summary>
        ///// Código del cliente
        ///// </summary>
        //public string Codigo { get { return _codigo; } set { _codigo = value; } }
        //public int Id_tipo_carga { get { return _id_tipo_carga; } set { _id_tipo_carga = value; } } 
        //public bool IsActive { get { return _isActive; } set { _isActive = value; } }

        //public int IdEntrada { get { return this._id; } set { this._id = value; } }
        //public string FolioEntrada { get; set ; }
        //public string Ubicacion { get; set; }
        //public string ClienteNombre { get; set; }

        //public Usuario PUsuario { get; set; }
        //public Bodega PBodega { get; set; }
        //public Cortina PCortina { get; set; }
        //public Cliente PCliente { get; set; }
        //public List<Entrada_compartida> PLstEntComp { get; set; }
        //public List<Entrada_documento> PLstEntDoc { get; set; }
        //public List<Entrada_parcial> PLstEntPar { get; set; }
        //public List<Entrada_transporte> PLstEntTrans { get; set; }
        //public List<Entrada_transporte_condicion> PLstEntTransCond { get; set; }
        //public Entrada_parcial PEntPar { get; set; }
        //public Custodia PCustodia { get; set; }
        //public Tipo_carga PTipoCarga { get; set; }
        //public bool EsConsolidada { get; set; }
        //public Entrada_inventario PEntInv { get; set; }
        //public bool ConFondeo { get; set; }
        //public List<Tarima_almacen> PLstTarAlm { get; set; }
        //public Tarima_almacen_estandar PTarAlmEstd { get; set; }

        //public List<Cliente_copia> PLstCCopia { get; set; }
        //#endregion

        //#region Constructores
        //public Entrada()
        //{
        //    this._id_bodega = 0;
        //    this._folio = String.Empty;
        //    this._folio_indice = String.Empty;
        //    this._fecha = new DateTime(1, 1, 1);
        //    this._hora = string.Empty;
        //    this._id_cortina = 0;
        //    this._id_cliente = 0;
        //    this._referencia = string.Empty;
        //    this._origen = string.Empty;
        //    this._mercancia = string.Empty;
        //    this._id_custodia = 0;
        //    this._operador = string.Empty;
        //    this._no_caja_cinta_aduanal = 0;
        //    this._no_pallet = 0;
        //    this._no_bulto_danado = 0;
        //    this._no_bulto_abierto = 0;
        //    this._no_bulto_declarado = 0;
        //    this._no_pieza_declarada = 0;
        //    this._no_bulto_recibido = 0;
        //    this._no_pieza_recibida = 0;
        //    this._es_unica = false;
        //    this._hora_descarga = string.Empty;
        //    this._vigilante = string.Empty;
        //    this._observaciones = string.Empty;
        //    this._motivo_cancelacion = string.Empty;
        //    this._codigo = string.Empty;
        //    this._id_tipo_carga = 0;
        //    this._isActive = false;
        //}
        //#endregion
    }
}
