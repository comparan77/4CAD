using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;

namespace ModelCasc.operation
{
    public class Transporte_condicion_transporte_tipo
    {
        #region Campos
        protected int _id;
        protected int _id_transporte_tipo;
        protected int _id_transporte_condicion;
        protected bool _entrada;
        protected bool _salida;
        protected int _orden;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_transporte_tipo { get { return _id_transporte_tipo; } set { _id_transporte_tipo = value; } }
        public int Id_transporte_condicion { get { return _id_transporte_condicion; } set { _id_transporte_condicion = value; } }
        public bool Entrada { get { return _entrada; } set { _entrada = value; } }
        public bool Salida { get { return _salida; } set { _salida = value; } }
        public int Orden { get { return _orden; } set { _orden = value; } }
        public Transporte_condicion PTransCond { get; set; }
        #endregion

        #region Constructores
        public Transporte_condicion_transporte_tipo()
        {
            this._id_transporte_tipo = 0;
            this._id_transporte_condicion = 0;
            this._entrada = false;
            this._salida = false;
            this._orden = 0;
        }
        #endregion
    }
}
