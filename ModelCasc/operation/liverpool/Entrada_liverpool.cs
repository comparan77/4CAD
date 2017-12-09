using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation.liverpool
{
    public class Entrada_liverpool
    {
        #region Campos
        protected int _id;
        protected int _id_entrada;
        protected string _proveedor;
        protected string _trafico;
        protected int _pedido;
        protected int _piezas;
        protected DateTime _fecha_confirma;
        protected int _piezas_maq;
        protected DateTime _fecha_maquila;
        protected int _num_pasos;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada { get { return _id_entrada; } set { _id_entrada = value; } }
        public string Proveedor { get { return _proveedor; } set { _proveedor = value; } }
        public string Trafico { get { return _trafico; } set { _trafico = value; } }
        public int Pedido { get { return _pedido; } set { _pedido = value; } }
        public int Piezas { get { return _piezas; } set { _piezas = value; } }
        public DateTime Fecha_confirma { get { return _fecha_confirma; } set { _fecha_confirma = value; } }
        public int Piezas_maq { get { return _piezas_maq; } set { _piezas_maq = value; } }
        public DateTime Fecha_maquila { get { return _fecha_maquila; } set { _fecha_maquila = value; } }
        public int Num_pasos { get { return _num_pasos; } set { _num_pasos = value; } }
        public int Piezas_maquiladas_hoy { get; set; }
        public List<Entrada_liverpool_maquila> PLstMaquila { get; set; }
        #endregion

        #region Constructores
        public Entrada_liverpool()
        {
            this._id_entrada = 0;
            this._proveedor = String.Empty;
            this._trafico = String.Empty;
            this._pedido = 0;
            this._piezas = 0;
            this._fecha_confirma = default(DateTime);
            this._piezas_maq = 0;
            this._fecha_maquila = default(DateTime);
            this._num_pasos = 0;
        }
        #endregion
    }
}
