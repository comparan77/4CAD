using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using logisticaModel.process;

namespace logisticaModel.operation.warehouse
{
    public class Entrada
    {
        #region Campos
        protected int _id;
        protected string _referencia;
        protected DateTime _fecha;
        protected string _sku;
        protected string _mercancia;
        protected string _ubicacion;
        protected string _serielote;
        protected string _sid;
        protected int _cantidad;
        protected string _calidad;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public string Sku { get { return _sku; } set { _sku = value; } }
        public string Mercancia { get { return _mercancia; } set { _mercancia = value; } }
        public string Ubicacion { get { return _ubicacion; } set { _ubicacion = value; } }
        public string Serielote { get { return _serielote; } set { _serielote = value; } }
        public string Sid { get { return _sid; } set { _sid = value; } }
        public int Cantidad { get { return _cantidad; } set { _cantidad = value; } }
        public string Calidad { get { return _calidad; } set { _calidad = value; } }
        public string ErrUpload { get; set; }
        #endregion

        #region Constructores
        public Entrada()
        {
            this._referencia = String.Empty;
            this._fecha = default(DateTime);
            this._sku = String.Empty;
            this._mercancia = String.Empty;
            this._ubicacion = String.Empty;
            this._serielote = String.Empty;
            this._sid = String.Empty;
            this._cantidad = 0;
            this._calidad = String.Empty;
        }
        #endregion

        public Asn PAsn { get; set; }
    }
}
