using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;

namespace ModelCasc.operation
{
    [Serializable]
    public class Entrada_documento: IDocumentoEncontrado
    {
        #region Campos
        protected int _id;
        protected int _id_entrada;
        protected int _id_documento;
        protected string _referencia;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int Id_entrada { get { return _id_entrada; } set { _id_entrada = value; } }
        public int Id_documento { get { return _id_documento; } set { _id_documento = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public Documento PDocumento { get; set; }
        #endregion

        #region Constructores
        public Entrada_documento()
        {
            this._id_entrada = 0;
            this._id_documento = 0;
            this._referencia = String.Empty;
        }
        #endregion

        public int IdEntrada
        {
            get
            {
                return this._id_entrada;
            }
            set
            {
                this._id_entrada = value;
            }
        }

        public string FolioEntrada
        {
            get;
            set;
        }
    }
}
