using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Entrada_transporte_condicion
    {
        #region Campos
        protected int _id;
        protected int _id_entrada_transporte;
        protected int _id_transporte_condicion;
        protected bool _si_no;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada_transporte { get { return _id_entrada_transporte; } set { _id_entrada_transporte = value; } }
        public int Id_transporte_condicion { get { return _id_transporte_condicion; } set { _id_transporte_condicion = value; } }
        public bool Si_no { get { return _si_no; } set { _si_no = value; } }
        public string Condicion { get; set; }
        #endregion

        #region Constructores
        public Entrada_transporte_condicion()
        {
            this._id_entrada_transporte = 0;
            this._id_transporte_condicion = 0;
            this._si_no = false;
        }
        #endregion
    }
}
