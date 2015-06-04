using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Cliente_vendor: Catalogo
    {
        #region Campos
        protected string _id_fiscal;
        protected int _id_cliente_grupo;
        protected string _codigo;
        protected string _direccion;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Id_fiscal { get { return _id_fiscal; } set { _id_fiscal = value; } } 
        public int Id_cliente_grupo { get { return _id_cliente_grupo; } set { _id_cliente_grupo = value; } }
        public string Codigo { get { return _codigo; } set { _codigo = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Direccion { get { return _direccion; } set { _direccion = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        public string DireccionCorta
        {
            get
            {
                if (this._direccion.Length > 55)
                    return this._direccion.Substring(0, 55) + "...";
                else
                    return this._direccion;
            }
        }
        #endregion

        #region Constructores
        public Cliente_vendor()
        {
            this._id_fiscal = String.Empty;
            this._id_cliente_grupo = 0;
            this._codigo = String.Empty;
            this._nombre = String.Empty;
            this._direccion = String.Empty;
            this._IsActive = false;
        }
        #endregion

        public override string ToString()
        {
            return "{\"value\":\"" + this._id + "\", \"text\":\"" + this._nombre + "\", \"label\":\"" + this._codigo + "\"}";
        }

    }
}
