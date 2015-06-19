using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        protected int _id_tipo_carga;
        protected string _destino;
        protected DateTime? _fecha_cita;
        protected string _hora_cita;
        protected string _folio_cita;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public DateTime Fecha_solicitud { get { return _fecha_solicitud; } set { _fecha_solicitud = value; } }
        public DateTime Fecha_carga_solicitada { get { return _fecha_carga_solicitada; } set { _fecha_carga_solicitada = value; } }
        public string Hora_carga_solicitada { get { return _hora_carga_solicitada; } set { _hora_carga_solicitada = value; } }
        public int? Id_transporte { get { return _id_transporte; } set { _id_transporte = value; } }
        public int Id_transporte_tipo { get { return _id_transporte_tipo; } set { _id_transporte_tipo = value; } }
        public int Id_tipo_carga { get { return _id_tipo_carga; } set { _id_tipo_carga = value; } }
        public string Destino { get { return _destino; } set { _destino = value; } }
        public DateTime? Fecha_cita { get { return _fecha_cita; } set { _fecha_cita = value; } }
        public string Hora_cita { get { return _hora_cita; } set { _hora_cita = value; } }
        public string Folio_cita { get { return _folio_cita; } set { _folio_cita = value; } }

        public string Tipo_transporte { get; set; }
        public string Tipo_carga { get; set; }
        #endregion

        #region Constructores
        public Salida_trafico()
		{
			this._fecha_solicitud = default(DateTime);
			this._fecha_carga_solicitada = default(DateTime);
			this._hora_carga_solicitada = String.Empty;
			this._id_transporte = null;
			this._id_transporte_tipo = 0;
			this._id_tipo_carga = 0;
			this._destino = String.Empty;
			this._fecha_cita = null;
			this._hora_cita = null;
			this._folio_cita = null;
		}
        #endregion
    }
}
