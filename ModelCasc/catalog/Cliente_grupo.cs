using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Cliente_grupo
    {
        #region Campos
        protected int _id;
        protected string _nombre;
        protected bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public bool Isactive { get { return _IsActive; } set { _IsActive = value; } }
        public int cantComprador { get; set; }
        public int cantVendor { get; set; }
        public int cantMercancia { get; set; }
        #endregion

        #region Constructores
        public Cliente_grupo()
        {
            this._nombre = String.Empty;
            this._IsActive = false;
        }
        #endregion
    }
}
