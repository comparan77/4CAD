using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.catalog
{
    public class Mercancia_servicio
    {
        #region Campos
        protected int _id;
        protected int _id_cliente_mercancia;
        protected string _sku;
        protected int _id_servicio;
        protected float _precio;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_cliente_mercancia { get { return _id_cliente_mercancia; } set { _id_cliente_mercancia = value; } }
        public string Sku { get { return _sku; } set { _sku = value; } }
        public int Id_servicio { get { return _id_servicio; } set { _id_servicio = value; } }
        public float Precio { get { return _precio; } set { _precio = value; } }
        #endregion

        #region Constructores
        public Mercancia_servicio()
		{
			this._id_cliente_mercancia = 0;
			this._sku = String.Empty;
			this._id_servicio = 0;
            this._precio = 0;
		}
        #endregion
    }
}
