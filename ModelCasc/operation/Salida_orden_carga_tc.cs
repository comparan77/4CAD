using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Salida_orden_carga_tc
    {
        #region Campos
        protected int _id;
        protected int _id_salida_orden_carga;
        protected int _id_transporte_condicion;
        protected bool _si_no;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_salida_orden_carga { get { return _id_salida_orden_carga; } set { _id_salida_orden_carga = value; } }
        public int Id_transporte_condicion { get { return _id_transporte_condicion; } set { _id_transporte_condicion = value; } }
        public bool Si_no { get { return _si_no; } set { _si_no = value; } }
        public string Condicion { get; set; }
        public string Categoria { get; set; }
        #endregion

        #region Constructores
        public Salida_orden_carga_tc()
        {
            this._id_salida_orden_carga = 0;
            this._id_transporte_condicion = 0;
            this._si_no = false;
        }
        #endregion
    }
}
