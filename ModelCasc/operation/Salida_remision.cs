using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    [Serializable]
    public class Salida_remision
    {
        #region Campos
        protected int _id;
        protected int _id_entrada;
        protected int? _id_entrada_inventario;
        protected int _id_usuario_elaboro;
        protected int _id_usuario_autorizo;
        protected int _id_salida_trafico;
        protected string _folio_remision;
        protected string _referencia;
        protected string _codigo_cliente;
        protected string _codigo;
        protected string _orden;
        protected DateTime _fecha_remision;
        protected string _etiqueta_rr;
        protected DateTime? _fecha_recibido;
        protected string _dano_especifico;
        protected int _id_estatus;
        protected bool _es_devolucion;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada { get { return _id_entrada; } set { _id_entrada = value; } }
        public int? Id_entrada_inventario { get { return _id_entrada_inventario; } set { _id_entrada_inventario = value; } }
        public int Id_usuario_elaboro { get { return _id_usuario_elaboro; } set { _id_usuario_elaboro = value; } }
        public int Id_usuario_autorizo { get { return _id_usuario_autorizo; } set { _id_usuario_autorizo = value; } }
        public int Id_salida_trafico { get { return _id_salida_trafico; } set { _id_salida_trafico = value; } }
        public string Folio_remision { get { return _folio_remision; } set { _folio_remision = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public string Codigo_cliente { get { return _codigo_cliente; } set { _codigo_cliente = value; } }
        public string Codigo { get { return _codigo; } set { _codigo = value; } }
        public string Orden { get { return _orden; } set { _orden = value; } }
        public DateTime Fecha_remision { get { return _fecha_remision; } set { _fecha_remision = value; } }
        public string Etiqueta_rr { get { return _etiqueta_rr; } set { _etiqueta_rr = value; } }
        public DateTime? Fecha_recibido { get { return _fecha_recibido; } set { _fecha_recibido = value; } }
        public string Dano_especifico { get { return _dano_especifico; } set { _dano_especifico = value; } }
        public int Id_estatus { get { return _id_estatus; } set { _id_estatus = value; } }
        public bool Es_devolucion { get { return _es_devolucion; } set { _es_devolucion = value; } }

        public List<Salida_remision_detail> LstSRDetail { get; set; }
        public string Mercancia { get; set; }
        public string Vendor { get; set; }
        public string Proveedor { get; set; }
        public string Proveedor_direccion { get; set; }
        public string Autorizo { get; set; }
        public string Elaboro { get; set; }
        public int PiezaTotal { get; set; }
        public int BultoTotal { get; set; }
        public int PiezaTotalInv { get; set; }
        public int BultoTotalInv { get; set; }
        public int Pallet { get; set; }
        public int CantParciales { get; set; }
        public bool TieneOrdenCarga { get; set; }
        /// <summary>
        /// La forma de una salida puede ser única, parcial o última
        /// </summary>
        public int Forma { get; set; }
        public Salida_trafico PTrafico { get; set; }
        #endregion

        #region Constructores
        public Salida_remision()
        {
            this._id_entrada = 0;
            this._id_entrada_inventario = null;
            this._id_usuario_elaboro = 0;
            this._id_usuario_autorizo = 0;
            this._id_salida_trafico = 0;
            this._folio_remision = String.Empty;
            this._referencia = String.Empty;
            this._codigo_cliente = String.Empty;
            this._codigo = String.Empty;
            this._orden = String.Empty;
            this._fecha_remision = default(DateTime);
            this._etiqueta_rr = null;
            this._fecha_recibido = null;
            this._dano_especifico = null;
            this._id_estatus = 0;
            this._es_devolucion = false;
        }
        #endregion
    }
}
