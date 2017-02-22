using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Transporte_condicion
    {
        #region Campos
        protected int _id;
        protected int? _id_transporte_condicion_categoria;
        protected string _nombre;
        protected bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int? Id_transporte_condicion_categoria { get { return _id_transporte_condicion_categoria; } set { _id_transporte_condicion_categoria = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        public Transporte_condicion_categoria PTransCondCat { get; set; }
        #endregion

        #region Constructores
        public Transporte_condicion()
        {
            this._id_transporte_condicion_categoria = null;
            this._nombre = String.Empty;
            this._IsActive = false;
        }
        #endregion
    }
}
