using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation.liverpool
{
    public class Entrada_liverpool_servicio
    {
        #region Campos
        protected int _id;
        protected int _id_liverpool_maquila;
        protected int _id_servicio;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_liverpool_maquila { get { return _id_liverpool_maquila; } set { _id_liverpool_maquila = value; } }
        public int Id_servicio { get { return _id_servicio; } set { _id_servicio = value; } }
        #endregion

        #region Constructores
        public Entrada_liverpool_servicio()
        {
            this._id_liverpool_maquila = 0;
            this._id_servicio = 0;
        }
        #endregion
    }
}
