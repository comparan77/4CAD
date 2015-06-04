using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    [Serializable]
    public class Entrada_inventario
    {
        #region Campos
        protected int _id;
        protected int _id_entrada;
        protected int _id_usuario;
        protected int? _id_entrada_fondeo;
        protected string _codigo_cliente;
        protected string _referencia;
        protected string _orden_compra;
        protected string _codigo;
        //protected int _id_ubicacion;
        //protected int _id_comprador;
        protected int _id_vendor;
        protected string _mercancia;
        protected int _id_nom;
        protected string _solicitud;
        protected string _factura;
        protected double _valor_unitario;
        protected double _valor_factura;
        protected int _piezas;
        protected int _bultos;
        protected int _pallets;

        protected int _piezas_recibidas;
        protected int _bultos_recibidos;

        protected int _piezas_falt;
        protected int _piezas_sobr;
        protected int _bultos_falt;
        protected int _bultos_sobr;
        protected string _observaciones;
        protected DateTime _fecha_maquila;
        protected int _id_estatus;

        protected List<Entrada_inventario_detail> _lstEntInvDet;
        protected List<Entrada_inventario_lote> _lstEntInvLote;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada { get { return _id_entrada; } set { _id_entrada = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        public int? Id_entrada_fondeo { get { return _id_entrada_fondeo; } set { _id_entrada_fondeo = value; } } 
        public string Codigo_cliente { get { return _codigo_cliente; } set { _codigo_cliente = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public string Orden_compra { get { return _orden_compra; } set { _orden_compra = value; } }
        public string Codigo { get { return _codigo; } set { _codigo = value; } }
        //public int Id_ubicacion { get { return _id_ubicacion; } set { _id_ubicacion = value; } }
        //public int Id_comprador { get { return _id_comprador; } set { _id_comprador = value; } }
        public int Id_vendor { get { return _id_vendor; } set { _id_vendor = value; } }
        public string Mercancia { get { return _mercancia; } set { _mercancia = value; } }
        public int Id_nom { get { return _id_nom; } set { _id_nom = value; } }
        public string Solicitud { get { return _solicitud; } set { _solicitud = value; } }
        public string Factura { get { return _factura; } set { _factura = value; } }
        public double Valor_unitario { get { return _valor_unitario; } set { _valor_unitario = value; } }
        public double Valor_factura { get { return _valor_factura; } set { _valor_factura = value; } }
        public int Piezas { get { return _piezas; } set { _piezas = value; } }
        public int Bultos { get { return _bultos; } set { _bultos = value; } }
        public int Pallets { get { return _pallets; } set { _pallets = value; } }

        public int Piezas_recibidas { get { return _piezas_recibidas; } set { _piezas_recibidas = value; } }
        public int Bultos_recibidos { get { return _bultos_recibidos; } set { _bultos_recibidos = value; } }

        public int Piezas_falt { get { return _piezas_falt; } set { _piezas_falt = value; } }
        public int Piezas_sobr { get { return _piezas_sobr; } set { _piezas_sobr = value; } }
        public int Bultos_falt { get { return _bultos_falt; } set { _bultos_falt = value; } }
        public int Bultos_sobr { get { return _bultos_sobr; } set { _bultos_sobr = value; } }
        public string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public DateTime Fecha_maquila { get { return _fecha_maquila; } set { _fecha_maquila = value; } }
        public string StrFecha_maquila { get { return this._fecha_maquila.ToString("dd/MM/yyyy"); } }
        public int Id_estatus { get { return _id_estatus; } set { _id_estatus = value; } }
        public List<Entrada_inventario_detail> LstEntInvDet { get { return _lstEntInvDet; } set { _lstEntInvDet = value; } }
        public List<Entrada_inventario_lote> LstEntInvLote { get { return _lstEntInvLote; } set { _lstEntInvLote = value; } }

        public int Consec { get; set; }
        public string Ubicacion { get; set; }
        public string Comprador { get; set; }
        public string Proveedor { get; set; }
        public string Nom { get; set; }
        public string FolioEntrada { get; set; }
        //public int PzasPorBulto { get; set; }
        #endregion

        #region Constructores
        public Entrada_inventario()
        {
            this._id_entrada = 0;
            this._id_usuario = 0;
            this._id_entrada_fondeo = null;
            this._codigo_cliente = String.Empty;
            this._referencia = String.Empty;
            this._orden_compra = String.Empty;
            this._codigo = String.Empty;
            //this._id_ubicacion = 0;
            //this._id_comprador = 0;
            this._id_vendor = 0;
            this._mercancia = String.Empty;
            this._id_nom = 0;
            this._solicitud = String.Empty;
            this._factura = String.Empty;
            this._valor_unitario = 0;
            this._valor_factura = 0;
            this._piezas = 0;
            this._bultos = 0;
            this._pallets = 0;

            this._piezas_recibidas = 0;
            this._bultos_recibidos = 0;

            this._piezas_falt = 0;
            this._piezas_sobr = 0;
            this._bultos_falt = 0;
            this._bultos_sobr = 0;
            this._observaciones = String.Empty;
            this._fecha_maquila = default(DateTime);
            this._id_estatus = 0;
        }
        #endregion

    }
}
