﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.catalog
{
    public class Cliente_mercancia
    {
        #region
        protected int _id;
		protected int _id_cliente;
		protected int _id_rotacion;
        protected int _id_unidad_empaque;
		protected string _sku;
		protected int _upc;
		protected string _nombre;
		protected decimal _precio;
		protected int _piezas_x_caja;
		protected int _cajas_x_tarima;
		#endregion

		#region Propiedades
		public int Id { get { return _id; } set { _id = value; } } 
		public int Id_cliente { get { return _id_cliente; } set { _id_cliente = value; } } 
		public int Id_rotacion { get { return _id_rotacion; } set { _id_rotacion = value; } }
        public int Id_unidad_empaque { get { return _id_unidad_empaque; } set { _id_unidad_empaque = value; } }
		public string Sku { get { return _sku; } set { _sku = value; } } 
		public int Upc { get { return _upc; } set { _upc = value; } } 
		public string Nombre { get { return _nombre; } set { _nombre = value; } } 
		public decimal Precio { get { return _precio; } set { _precio = value; } } 
		public int Piezas_x_caja { get { return _piezas_x_caja; } set { _piezas_x_caja = value; } } 
		public int Cajas_x_tarima { get { return _cajas_x_tarima; } set { _cajas_x_tarima = value; } } 
		#endregion

		#region Constructores
		public Cliente_mercancia()
		{
			this._id_cliente = 0;
			this._id_rotacion = 0;
            this._id_unidad_empaque = 0;
			this._sku = String.Empty;
			this._upc = 0;
			this._nombre = String.Empty;
			this._precio = 0;
			this._piezas_x_caja = 0;
			this._cajas_x_tarima = 0;
		}
		#endregion

        public decimal Tarifa { get; set; }
        public Cliente_mercancia_rotacion PRotacion { get; set; }

    }
}
