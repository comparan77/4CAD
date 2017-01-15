using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Salida_transporte_condicion
    {
        #region Campos
        protected int _id;
        protected int _id_salida;
        protected int _id_transporte_condicion;
        protected bool _si_no;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_salida { get { return _id_salida; } set { _id_salida = value; } }
        public int Id_transporte_condicion { get { return _id_transporte_condicion; } set { _id_transporte_condicion = value; } }
        public bool Si_no { get { return _si_no; } set { _si_no = value; } }
        public string Condicion { get; set; }
        public string Categoria { get; set; }
        #endregion

        #region Constructores
        public Salida_transporte_condicion()
        {
            this._id_salida = 0;
            this._id_transporte_condicion = 0;
            this._si_no = false;
        }
        #endregion

        
    }
}
