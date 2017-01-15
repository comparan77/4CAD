using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;

namespace ModelCasc.operation
{
    public class Transporte_condicion_cliente
    {
        #region Campos
        protected int _id;
        protected int _id_cliente;
        protected int _id_transporte_condicion;
        protected bool _entrada;
        protected bool _salida;
        protected int? _orden;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_cliente { get { return _id_cliente; } set { _id_cliente = value; } }
        public int Id_transporte_condicion { get { return _id_transporte_condicion; } set { _id_transporte_condicion = value; } }
        public bool Entrada { get { return _entrada; } set { _entrada = value; } }
        public bool Salida { get { return _salida; } set { _salida = value; } }
        public int? Orden { get { return _orden; } set { _orden = value; } }
        public Transporte_condicion_categoria PTransporte_condicion_cat { get; set; }
        public List< Transporte_condicion> PLstTransporte_condicion { get; set; }
        #endregion

        #region Constructores
        public Transporte_condicion_cliente()
        {
            this._id_cliente = 0;
            this._id_transporte_condicion = 0;
            this._entrada = false;
            this._salida = false;
            this._orden = null;
        }
        #endregion
    }
}
