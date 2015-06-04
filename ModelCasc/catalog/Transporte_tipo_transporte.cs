using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Transporte_tipo_transporte
    {
        #region Campos
        protected int _id;
        protected int _id_transporte;
        protected int _id_transporte_tipo;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_transporte { get { return _id_transporte; } set { _id_transporte = value; } }
        public int Id_transporte_tipo { get { return _id_transporte_tipo; } set { _id_transporte_tipo = value; } }
        #endregion

        #region Constructores
        public Transporte_tipo_transporte()
        {
            this._id_transporte = 0;
            this._id_transporte_tipo = 0;
        }
        #endregion
    }
}
