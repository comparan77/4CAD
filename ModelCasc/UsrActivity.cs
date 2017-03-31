using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;

namespace ModelCasc
{
    public abstract class UsrActivity
    {
        private Usuario _usuario;
        public Usuario PUsuario { get { return this._usuario; } set { this._usuario = value; } }
    }
}
