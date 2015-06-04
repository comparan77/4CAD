using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Salida_orden_carga
    {
        #region Campos
        protected int _id;
        protected int _id_tipo_carga;
        protected int _id_transporte;
        protected int _id_transporte_tipo;
        protected string _folio_orden_carga;
        protected DateTime _fecha_solicitud;
        protected DateTime _fecha_carga_solicitada;
        protected string _hora_carga_solicitada;
        protected DateTime _fecha_cita;
        protected string _hora_cita;
        protected string _destino;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_tipo_carga { get { return _id_tipo_carga; } set { _id_tipo_carga = value; } }
        public int Id_transporte { get { return _id_transporte; } set { _id_transporte = value; } }
        public int Id_transporte_tipo { get { return _id_transporte_tipo; } set { _id_transporte_tipo = value; } }
        public string Folio_orden_carga { get { return _folio_orden_carga; } set { _folio_orden_carga = value; } }
        public DateTime Fecha_solicitud { get { return _fecha_solicitud; } set { _fecha_solicitud = value; } }
        public DateTime Fecha_carga_solicitada { get { return _fecha_carga_solicitada; } set { _fecha_carga_solicitada = value; } }
        public string Hora_carga_solicitada { get { return _hora_carga_solicitada; } set { _hora_carga_solicitada = value; } }
        public DateTime Fecha_cita { get { return _fecha_cita; } set { _fecha_cita = value; } }
        public string Hora_cita { get { return _hora_cita; } set { _hora_cita = value; } }
        public string Destino { get { return _destino; } set { _destino = value; } }
        public List<Salida_orden_carga_rem> LstRem { get; set; }
        public string TipoCarga { get; set; }
        public string TipoEnvio { get; set; }
        public string Transporte { get; set; }
        public string TipoTransporte { get; set; }
        public int Id_usuario { get; set; } 
        #endregion

        #region Constructores
        public Salida_orden_carga()
		{
			this._id_tipo_carga = 0;
			this._id_transporte = 0;
			this._id_transporte_tipo = 0;
			this._folio_orden_carga = String.Empty;
			this._fecha_solicitud = default(DateTime);
			this._fecha_carga_solicitada = default(DateTime);
			this._hora_carga_solicitada = String.Empty;
			this._fecha_cita = default(DateTime);
			this._hora_cita = String.Empty;
			this._destino = String.Empty;
		}
        #endregion
    }
}
