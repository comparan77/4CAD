using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Salida_transporte_auditoria
    {
        #region Campos
        protected int _id;
        protected int _id_bodega;
        protected int _id_transporte;
        protected int _id_transporte_tipo;
        protected string _folio;
        protected string _placa;
        protected string _caja;
        protected string _caja1;
        protected string _caja2;
        protected string _operador;
        protected string _prevencion;
        protected bool _aprovada;
        protected string _motivo_rechazo;
        protected string _cp;
        protected string _callenum;
        protected string _estado;
        protected string _municipio;
        protected string _colonia;
        protected bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_bodega { get { return _id_bodega; } set { _id_bodega = value; } }
        public int Id_transporte { get { return _id_transporte; } set { _id_transporte = value; } }
        public int Id_transporte_tipo { get { return _id_transporte_tipo; } set { _id_transporte_tipo = value; } }
        public string Folio { get { return _folio; } set { _folio = value; } }
        public string Placa { get { return _placa; } set { _placa = value; } }
        public string Caja { get { return _caja; } set { _caja = value; } }
        public string Caja1 { get { return _caja1; } set { _caja1 = value; } }
        public string Caja2 { get { return _caja2; } set { _caja2 = value; } }
        public string Operador { get { return _operador; } set { _operador = value; } }
        public string Prevencion { get { return _prevencion; } set { _prevencion = value; } }
        public bool Aprovada { get { return _aprovada; } set { _aprovada = value; } }
        public string Motivo_rechazo { get { return _motivo_rechazo; } set { _motivo_rechazo = value; } }
        public string Cp { get { return _cp; } set { _cp = value; } }
        public string Callenum { get { return _callenum; } set { _callenum = value; } }
        public string Estado { get { return _estado; } set { _estado = value; } }
        public string Municipio { get { return _municipio; } set { _municipio = value; } }
        public string Colonia { get { return _colonia; } set { _colonia = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        public List<Salida_transporte_condicion> PLstSalTransCond { get; set; } 

        #endregion

        #region Constructores
        public Salida_transporte_auditoria()
        {
            this._id_bodega = 0;
            this._id_transporte = 0;
            this._id_transporte_tipo = 0;
            this._folio = String.Empty;
            this._placa = String.Empty;
            this._caja = String.Empty;
            this._caja1 = String.Empty;
            this._caja2 = String.Empty;
            this._operador = String.Empty;
            this._prevencion = String.Empty;
            this._aprovada = false;
            this._motivo_rechazo = String.Empty;
            this._cp = String.Empty;
            this._callenum = String.Empty;
            this._estado = String.Empty;
            this._municipio = String.Empty;
            this._colonia = String.Empty;
            this._IsActive = false;
        }
        #endregion
    }
}
