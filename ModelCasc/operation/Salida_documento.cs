using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;

namespace ModelCasc.operation
{
    [Serializable]
    public class Salida_documento
    {
        #region Campos
        protected int _id;
        protected int _id_salida;
        protected int _id_documento;
        protected string _referencia;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_salida { get { return _id_salida; } set { _id_salida = value; } }
        public int Id_documento { get { return _id_documento; } set { _id_documento = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public Documento PDocumento { get; set; }
        #endregion

        #region Constructores
        public Salida_documento()
        {
            this._id_salida = 0;
            this._id_documento = 0;
            this._referencia = String.Empty;
        }
        #endregion
    }
}
