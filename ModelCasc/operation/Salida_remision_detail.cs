using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    [Serializable]
    public class Salida_remision_detail
    {
        #region Campos
        protected int _id;
        protected int _id_salida_remision;
        protected int _bulto;
        protected int _piezaxbulto;
        protected int _piezas;
        protected bool _danado;
        public string _lote;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_salida_remision { get { return _id_salida_remision; } set { _id_salida_remision = value; } }
        public int Bulto { get { return _bulto; } set { _bulto = value; } }
        public int Piezaxbulto { get { return _piezaxbulto; } set { _piezaxbulto = value; } }
        public int Piezas { get { return _piezas; } set { _piezas = value; } }
        public bool Danado { get { return _danado; } set { _danado = value; } }
        public string Lote { get { return _lote; } set { _lote = value; } }
        #endregion

        #region Constructores
        public Salida_remision_detail()
        {
            this._id_salida_remision = 0;
            this._bulto = 0;
            this._piezaxbulto = 0;
            this._piezas = 0;
            this._danado = false;
            this._lote = null;
        }
        #endregion
    }
}
