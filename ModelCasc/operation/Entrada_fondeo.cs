using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;

namespace ModelCasc.operation
{
    public class Entrada_fondeo
    {
        #region Campos
        protected int _id;
        protected DateTime _fecha;
        protected string _importador;
        protected string _aduana;
        protected string _referencia;
        protected string _factura;
        protected string _codigo;
        protected string _orden;
        protected string _vendor;
        protected int _piezas;
        protected double _valorfactura;
        protected string _folio;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public string Importador { get { return _importador; } set { _importador = value; } }
        public string Aduana { get { return _aduana; } set { _aduana = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public string Factura { get { return _factura; } set { _factura = value; } }
        public string Codigo { get { return _codigo; } set { _codigo = value; } }
        public string Orden { get { return _orden; } set { _orden = value; } }
        public string Vendor { get { return _vendor; } set { _vendor = value; } }
        public int Piezas { get { return _piezas; } set { _piezas = value; } }
        public double Valorfactura { get { return _valorfactura; } set { _valorfactura = value; } }
        public string Folio { get { return _folio; } set { _folio = value; } }

        public List<Cliente_vendor> LstClienteVendor { get; set; }
        public List<Cliente_mercancia> LstClienteMercancia { get; set; }
        public Entrada_inventario PEntInv { get; set; }
        public string Estatus { get; set; }
		#endregion

		#region Constructores
		public Entrada_fondeo()
		{
			this._fecha = default(DateTime);
            this._importador = string.Empty;
			this._aduana = String.Empty;
			this._referencia = String.Empty;
			this._factura = String.Empty;
			this._codigo = String.Empty;
			this._orden = String.Empty;
			this._vendor = String.Empty;
			this._piezas = 0;
			this._valorfactura = 0;
            this._folio = string.Empty;
		}
		#endregion
    }
}
