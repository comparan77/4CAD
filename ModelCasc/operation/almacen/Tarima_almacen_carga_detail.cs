using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation.almacen
{
    public class Tarima_almacen_carga_detail
    {
        #region Campos
        protected int _id;
        protected int _id_tarima_almacen_carga;
        protected int _id_tarima_almacen_remision_detail;
        protected int _id_tarima_almacen;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_tarima_almacen_carga { get { return _id_tarima_almacen_carga; } set { _id_tarima_almacen_carga = value; } }
        public int Id_tarima_almacen_remision_detail { get { return _id_tarima_almacen_remision_detail; } set { _id_tarima_almacen_remision_detail = value; } }
        public int Id_tarima_almacen { get { return _id_tarima_almacen; } set { _id_tarima_almacen = value; } }
        #endregion

        #region Constructores
        public Tarima_almacen_carga_detail()
        {
            this._id_tarima_almacen_carga = 0;
            this._id_tarima_almacen_remision_detail = 0;
            this._id_tarima_almacen = 0;
        }
        #endregion
    }
}
