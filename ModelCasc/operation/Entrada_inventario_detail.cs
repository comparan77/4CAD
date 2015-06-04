using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Entrada_inventario_detail
    {
        #region Campos
        protected int _id;
        protected int _id_entrada_inventario;
        protected int _bultos;
        protected int _piezasxbulto;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada_inventario { get { return _id_entrada_inventario; } set { _id_entrada_inventario = value; } }
        public int Bultos { get { return _bultos; } set { _bultos = value; } }
        public int Piezasxbulto { get { return _piezasxbulto; } set { _piezasxbulto = value; } }
        public int PiezasTotales { get; set; }
        #endregion

        #region Constructores
        public Entrada_inventario_detail()
        {
            this._id_entrada_inventario = 0;
            this._bultos = 0;
            this._piezasxbulto = 0;
        }
        #endregion
    }
}
