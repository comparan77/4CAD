using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    [Serializable]
    public class Entrada_partida
    {
        #region Campos
        protected int _id;
        protected int _id_entrada;
        protected string _ref_entrada;
        protected int _piezas;
        protected bool _nom;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada { get { return _id_entrada; } set { _id_entrada = value; } }
        public string Ref_entrada { get { return _ref_entrada; } set { _ref_entrada = value; } }
        public int Piezas { get { return _piezas; } set { _piezas = value; } }
        public bool Nom { get { return _nom; } set { _nom = value; } }
        #endregion

        #region Constructores
        public Entrada_partida()
        {
            this._id_entrada = 0;
            this._ref_entrada = String.Empty;
            this._piezas = 0;
            this._nom = false;
        }
        #endregion
    }
}
