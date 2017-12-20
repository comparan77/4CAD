using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    [Serializable]
    public class Maquila_paso
    {
        #region Campos
        protected int _id;
        protected int _id_ord_tbj_srv;
        protected string _foto64;
        protected string _descripcion;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_ord_tbj_srv { get { return _id_ord_tbj_srv; } set { _id_ord_tbj_srv = value; } }
        public string Foto64 { get { return _foto64; } set { _foto64 = value; } }
        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public int NumPaso { get; set; }
        #endregion

        #region Constructores
        public Maquila_paso()
        {
            this._id_ord_tbj_srv = 0;
            this._foto64 = String.Empty;
            this._descripcion = String.Empty;
        }
        #endregion
    }
}
