using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation.liverpool
{
    public class Entrada_liverpool_paso
    {
        #region Campos
        protected int _id;
        protected int _id_liverpool_maquila;
        protected int _descripcion;
        protected string _foto;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_liverpool_maquila { get { return _id_liverpool_maquila; } set { _id_liverpool_maquila = value; } }
        public int Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public string Foto { get { return _foto; } set { _foto = value; } }
        #endregion

        #region Constructores
        public Entrada_liverpool_paso()
        {
            this._id_liverpool_maquila = 0;
            this._descripcion = 0;
            this._foto = String.Empty;
        }
        #endregion
    }
}
