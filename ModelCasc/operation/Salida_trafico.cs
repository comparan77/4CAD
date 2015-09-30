using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;

namespace ModelCasc.operation
{
    public class Salida_trafico
    {
        #region Campos
        protected int _id;
        protected DateTime _fecha_solicitud;
        protected DateTime _fecha_carga_solicitada;
        protected string _hora_carga_solicitada;
        protected int? _id_transporte;
        protected int _id_transporte_tipo;
        protected int? _id_transporte_tipo_cita;
        protected int _id_tipo_carga;
        protected int _id_usuario_solicita;
        protected int? _id_usuario_asigna;
        protected int _id_salida_destino;
        protected DateTime? _fecha_cita;
        protected string _hora_cita;
        protected string _folio_cita;
        protected string _operador;
        protected string _placa;
        protected string _caja;
        protected string _caja1;
        protected string _caja2;
        protected int? _pallet;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public DateTime Fecha_solicitud { get { return _fecha_solicitud; } set { _fecha_solicitud = value; } }
        public DateTime Fecha_carga_solicitada { get { return _fecha_carga_solicitada; } set { _fecha_carga_solicitada = value; } }
        public string Hora_carga_solicitada { get { return _hora_carga_solicitada; } set { _hora_carga_solicitada = value; } }
        public int? Id_transporte { get { return _id_transporte; } set { _id_transporte = value; } }
        public int Id_transporte_tipo { get { return _id_transporte_tipo; } set { _id_transporte_tipo = value; } }
        public int? Id_transporte_tipo_cita { get { return _id_transporte_tipo_cita; } set { _id_transporte_tipo_cita = value; } }
        public int Id_tipo_carga { get { return _id_tipo_carga; } set { _id_tipo_carga = value; } }
        public int Id_usuario_solicita { get { return _id_usuario_solicita; } set { _id_usuario_solicita = value; } }
        public int? Id_usuario_asigna { get { return _id_usuario_asigna; } set { _id_usuario_asigna = value; } }
        public int Id_salida_destino { get { return _id_salida_destino; } set { _id_salida_destino = value; } }
        public DateTime? Fecha_cita { get { return _fecha_cita; } set { _fecha_cita = value; } }
        public string Hora_cita { get { return _hora_cita; } set { _hora_cita = value; } }
        public string Folio_cita { get { return _folio_cita; } set { _folio_cita = value; } }
        public string Operador { get { return _operador; } set { _operador = value; } }
        public string Placa { get { return _placa; } set { _placa = value; } }
        public string Caja { get { return _caja; } set { _caja = value; } }
        public string Caja1 { get { return _caja1; } set { _caja1 = value; } }
        public string Caja2 { get { return _caja2; } set { _caja2 = value; } }
        public int? Pallet { get { return _pallet; } set { _pallet = value; } }

        public string Transporte_tipo { get; set; }
        public string Transporte_tipo_cita { get; set; }
        public string Tipo_carga { get; set; }
        public string Transporte { get; set; }
        public string Solicitante { get; set; }
        public string Asignante { get; set; }
        public List<Salida_remision> PLstSalRem { get; set; }
        public Transporte PTransporte { get; set; }
        public Transporte_tipo PTransporteTipo { get; set; }
        public Salida_orden_carga PSalidaOrdenCarga { get; set; }
        public Salida_destino PSalidaDestino { get; set; }
        #endregion

        #region Constructores
        public Salida_trafico()
		{
            this._fecha_solicitud = default(DateTime);
            this._fecha_carga_solicitada = default(DateTime);
            this._hora_carga_solicitada = String.Empty;
            this._id_transporte = null;
            this._id_transporte_tipo = 0;
            this._id_transporte_tipo_cita = null;
            this._id_tipo_carga = 0;
            this._id_usuario_solicita = 0;
            this._id_usuario_asigna = null; 
            this._id_salida_destino = 0;
            this._fecha_cita = null;
            this._hora_cita = null;
            this._folio_cita = null;
            this._operador = null;
            this._placa = null;
            this._caja = null;
            this._caja1 = null;
            this._caja2 = null;
            this._pallet = null;
		}
        #endregion
    }
}
