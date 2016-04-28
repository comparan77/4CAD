using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation.almacen
{
    public class Tarima_almacen_carga
    {
        #region Campos
        protected int _id;
        protected int _id_tipo_carga;
        protected int _id_usuario;
        protected int _id_tarima_almacen_trafico;
        protected string _folio_orden_carga;
        protected bool _tiene_salida;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_tipo_carga { get { return _id_tipo_carga; } set { _id_tipo_carga = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        public int Id_tarima_almacen_trafico { get { return _id_tarima_almacen_trafico; } set { _id_tarima_almacen_trafico = value; } }
        public string Folio_orden_carga { get { return _folio_orden_carga; } set { _folio_orden_carga = value; } }
        public bool Tiene_salida { get { return _tiene_salida; } set { _tiene_salida = value; } }

        public Tarima_almacen_trafico PTarAlmTrafico { get; set; }
        public List<Tarima_almacen_carga_format> PLstTACRpt { get; set; }
        #endregion

        #region Constructores
        public Tarima_almacen_carga()
		{
			this._id_tipo_carga = 0;
			this._id_usuario = 0;
			this._id_tarima_almacen_trafico = 0;
			this._folio_orden_carga = String.Empty;
			this._tiene_salida = false;
            this.PLstTACRpt = new List<Tarima_almacen_carga_format>();
		}
        #endregion
    }
}
