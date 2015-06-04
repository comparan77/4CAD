using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Salida_parcial
    {
        #region Campos
        protected int _id;
        protected int _id_salida;
        protected int _id_usuario;
        protected int _no_salida;
        protected string _referencia;
        protected bool _es_ultima;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_salida { get { return _id_salida; } set { _id_salida = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        public int No_salida { get { return _no_salida; } set { _no_salida = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public bool Es_ultima { get { return _es_ultima; } set { _es_ultima = value; } }
        #endregion

        #region Constructores
        public Salida_parcial()
        {
            this._id_salida = 0;
            this._id_usuario = 0;
            this._no_salida = 0;
            this._referencia = String.Empty;
            this._es_ultima = false;
        }
        #endregion
    }
}
