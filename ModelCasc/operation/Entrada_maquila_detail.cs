using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Entrada_maquila_detail
    {
        #region Campos
        protected int _id;
        protected int _id_entrada_maquila;
        protected bool _danado;
        protected int _bultos;
        protected int _piezasxbulto;
        protected string _lote;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada_maquila { get { return _id_entrada_maquila; } set { _id_entrada_maquila = value; } }
        public bool Danado { get { return _danado; } set { _danado = value; } }
        public int Bultos { get { return _bultos; } set { _bultos = value; } }
        public int Piezasxbulto { get { return _piezasxbulto; } set { _piezasxbulto = value; } }
        public string Lote { get { return _lote; } set { _lote = value; } }
        public int PiezasTotales { get; set; }
        public int BultoSR { get; set; }
        public int BultoD { get; set; }
        public bool Tiene_remision { get; set; }
        public string cssLocked { get; set; }
        #endregion

        #region Constructores
        public Entrada_maquila_detail()
        {
            this._id_entrada_maquila = 0;
            this._danado = false;
            this._bultos = 0;
            this._piezasxbulto = 0;
            this._lote = null; 
        }
        #endregion

        
    }
}
