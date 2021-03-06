﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.catalog
{
    public class Bodega_zona
    {
        #region Campos
        protected int _id;
        protected int _id_bodega;
        protected string _clave;
        protected string _nombre;
        protected bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_bodega { get { return _id_bodega; } set { _id_bodega = value; } }
        public string Clave { get { return _clave; } set { _clave = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        #endregion

        #region Constructores
        public Bodega_zona()
        {
            this._id_bodega = 0;
            this._clave = String.Empty;
            this._nombre = String.Empty;
            this._IsActive = false;
        }
        #endregion
    }
}
