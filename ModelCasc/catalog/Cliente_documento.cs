using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.catalog
{
    [Serializable]
    public class Cliente_documento
    {
        #region Campos
        protected int _id;
        protected int _id_cliente;
        protected int _id_documento;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_cliente { get { return _id_cliente; } set { _id_cliente = value; } }
        public int Id_documento { get { return _id_documento; } set { _id_documento = value; } }
        /// <summary>
        /// Se utiliza como bandera en la captura de la entrada, para validar los documentos necesarios 
        /// definidos para cada cliente.
        /// </summary>
        public bool IsAdd { get; set; } 
        #endregion

        #region Constructores
        public Cliente_documento()
        {
            this._id_cliente = 0;
            this._id_documento = 0;
        }
        #endregion
    }
}
