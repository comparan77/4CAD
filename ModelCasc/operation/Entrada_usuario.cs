using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Entrada_usuario
    {
        #region Campos
        protected int _id;
        protected int _id_entrada;
        protected int _id_usuario;
        protected string _folio;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada { get { return _id_entrada; } set { _id_entrada = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        public string Folio { get { return _folio; } set { _folio = value; } }
        #endregion

        #region Constructores
        public Entrada_usuario()
        {
            this._id_entrada = 0;
            this._id_usuario = 0;
        }
        #endregion
    }
}
