using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Vigilante
    {
        #region Campos
        protected int _id;
        protected int _id_bodega;
        protected string _nombre;
        protected bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_bodega { get { return _id_bodega; } set { _id_bodega = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        #endregion

        #region Constructores
        public Vigilante()
        {
            this._id_bodega = 0;
            this._nombre = String.Empty;
            this._IsActive = false;
        }
        #endregion
    }
}
