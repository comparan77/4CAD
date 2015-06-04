using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Cliente_mercancia
    {
        #region Campos
        protected int _id;
        protected int _id_cliente_grupo;
        protected string _codigo;
        protected string _nombre;
        protected string _clase;
        protected string _negocio;
        protected double _valor_unitario;
        protected string _unidad;
        protected int _presentacion_x_bulto;
        protected int _bultos_x_tarima;
        protected bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_cliente_grupo { get { return _id_cliente_grupo; } set { _id_cliente_grupo = value; } }
        public string Codigo { get { return _codigo; } set { _codigo = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Clase { get { return _clase; } set { _clase = value; } }
        public string Negocio { get { return _negocio; } set { _negocio = value; } }
        public double Valor_unitario { get { return _valor_unitario; } set { _valor_unitario = value; } }
        public string Unidad { get { return _unidad; } set { _unidad = value; } }
        public int Presentacion_x_bulto { get { return _presentacion_x_bulto; } set { _presentacion_x_bulto = value; } }
        public int Bultos_x_tarima { get { return _bultos_x_tarima; } set { _bultos_x_tarima = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        #endregion

        #region Constructores
        public Cliente_mercancia()
        {
            this._id_cliente_grupo = 0;
            this._codigo = String.Empty;
            this._nombre = String.Empty;
            this._clase = String.Empty;
            this._negocio = String.Empty;
            this._valor_unitario = 0;
            this._unidad = String.Empty;
            this._presentacion_x_bulto = 0;
            this._bultos_x_tarima = 0;
            this._IsActive = false;
        }
        #endregion

        public override string ToString()
        {
            return "{\"value\":\"" + this._id + "\", \"clase\":\"" + this._clase + "\", \"label\":\"" + this._codigo + "\", \"name\":\"" + this._nombre + "\"}";
        }
    }
}
