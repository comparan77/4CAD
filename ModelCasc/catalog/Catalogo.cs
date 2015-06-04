using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public abstract class Catalogo
    {

        #region Campos
        protected int _id;
        protected string _nombre;
        protected bool _IsActive;
        #endregion

        public override string ToString()
        {
            return "{\"value\":\"" + this._id + "\", \"label\":\"" + this._nombre + "\"}";
        }

    }
}
