using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Entrada_inventario_lote
    {
        #region Campos
        protected int _id;
        protected int _id_entrada_inventario;
        protected string _codigo;
        protected string _ordencompra;
        protected string _lote;
        protected int _piezas;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada_inventario { get { return _id_entrada_inventario; } set { _id_entrada_inventario = value; } }
        public string Codigo { get { return _codigo; } set { _codigo = value; } }
        public string Ordencompra { get { return _ordencompra; } set { _ordencompra = value; } }
        public string Lote { get { return _lote; } set { _lote = value; } }
        public int Piezas { get { return _piezas; } set { _piezas = value; } }
        #endregion

        #region Constructores
        public Entrada_inventario_lote()
        {
            this._id_entrada_inventario = 0;
            this._codigo = String.Empty;
            this._ordencompra = String.Empty;
            this._lote = String.Empty;
            this._piezas = 0;
        }
        #endregion
    }
}
