using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    public class Cliente_codigo
    {
        #region Campos
        protected int _id;
        protected int _id_cliente_grupo;
        protected string _clave;
        protected int _digitos;
        protected int _consec_arribo;
        protected int _anio_actual;
        protected bool _dif_codigo;
        protected int _consec_embarque;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_cliente_grupo { get { return _id_cliente_grupo; } set { _id_cliente_grupo = value; } }
        public string Clave { get { return _clave; } set { _clave = value; } }
        public int Digitos { get { return _digitos; } set { _digitos = value; } }
        public int Consec_arribo { get { return _consec_arribo; } set { _consec_arribo = value; } }
        public int Anio_actual { get { return _anio_actual; } set { _anio_actual = value; } }
        public bool Dif_codigo { get { return _dif_codigo; } set { _dif_codigo = value; } }
        public int Consec_embarque { get { return _consec_embarque; } set { _consec_embarque = value; } }
        #endregion

        #region Constructores
        public Cliente_codigo()
        {
            this._id_cliente_grupo = 0;
            this._clave = String.Empty;
            this._digitos = 0;
            this._consec_arribo = 0;
            this._anio_actual = 0;
            this._dif_codigo = false;
            this._consec_embarque = 0;
        }
        #endregion
    }
}
