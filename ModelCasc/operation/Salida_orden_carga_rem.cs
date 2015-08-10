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
        protected int _pallet;
        protected string _referencia;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_salida_remision { get { return _id_salida_remision; } set { _id_salida_remision = value; } }
        public int Id_salida_orden_carga { get { return _id_salida_orden_carga; } set { _id_salida_orden_carga = value; } }
        public int? Id_salida { get { return _id_salida; } set { _id_salida = value; } }
        public Salida_remision PSalRem { get; set; }
        public int Pallet { get { return _pallet; } set { _pallet = value; } } 
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        #endregion

        #region Constructores
        public Salida_orden_carga_rem()
        {
            this._id_salida_remision = 0;
            this._id_salida_orden_carga = 0;
            this._id_salida = null;
            this._pallet = 0;
            this._referencia = string.Empty;
        }
        #endregion
    }
}
