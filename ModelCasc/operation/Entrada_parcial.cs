using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Entrada_parcial
    {
        #region Campos
        protected int _id;
        protected int _id_entrada;
        protected int _id_usuario;
        protected int _no_entrada;
        protected string _referencia;
        protected bool _es_ultima;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada { get { return _id_entrada; } set { _id_entrada = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        public int No_entrada { get { return _no_entrada; } set { _no_entrada = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public bool Es_ultima { get { return _es_ultima; } set { _es_ultima = value; } }
        public int No_pieza_recibidas { get; set; }
        public int No_bulto_recibido { get; set; }
        #endregion

        #region Constructores
        public Entrada_parcial()
        {
            this._id_entrada = 0;
            this._id_usuario = 0;
            this._no_entrada = 0;
            this._referencia = String.Empty;
            this._es_ultima = false;
        }
        #endregion
    }
}
