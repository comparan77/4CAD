using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Salida_orden_carga_rem
    {
        #region Campos
        protected int _id;
        protected int _id_salida_remision;
        protected int _id_salida_orden_carga;
        protected int? _id_salida;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_salida_remision { get { return _id_salida_remision; } set { _id_salida_remision = value; } }
        public int Id_salida_orden_carga { get { return _id_salida_orden_carga; } set { _id_salida_orden_carga = value; } }
        public int? Id_salida { get { return _id_salida; } set { _id_salida = value; } }
        public Salida_remision PSalRem { get; set; }
        #endregion

        #region Constructores
        public Salida_orden_carga_rem()
        {
            this._id_salida_remision = 0;
            this._id_salida_orden_carga = 0;
            this._id_salida = null;
        }
        #endregion
    }
}
