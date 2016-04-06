using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Cliente
    {
        #region Campos
        protected int _id;
        protected int _id_cliente_grupo;
        protected string _nombre;
        protected string _rfc;
        protected string _razon;
        protected int _id_cuenta_tipo;
        protected bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_cliente_grupo { get { return _id_cliente_grupo; } set { _id_cliente_grupo = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Rfc { get { return _rfc; } set { _rfc = value; } }
        public string Razon { get { return _razon; } set { _razon = value; } }
        public int Id_cuenta_tipo { get { return _id_cuenta_tipo; } set { _id_cuenta_tipo = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } } 
        public string cuenta_tipo { get; set; }
        public string Grupo { get; set; }
        public List<Cliente_documento> PLstDocReq { get; set; }
        public string Mascara { get; set; }
        public string Documento { get; set; }
        public bool EsFondeo { get; set; }
        public Cliente_mercancia PClienteMercancia { get; set; }
        #endregion

        #region Constructores
        public Cliente()
        {
            this._id_cliente_grupo = 0;
            this._nombre = String.Empty;
            this._rfc = String.Empty;
            this._razon = String.Empty;
            this._id_cuenta_tipo = 0;
            this._IsActive = false;
        }
        #endregion
    }
}
