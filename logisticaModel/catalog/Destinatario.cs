using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logisticaModel.catalog
{
    public class Destinatario
    {
        #region Campos
        protected int _id;
        protected string _destino;
        protected string _direccion;
        protected bool _IsActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public string Destino { get { return _destino; } set { _destino = value; } }
        public string Direccion { get { return _direccion; } set { _direccion = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        #endregion

        #region Constructores
        public Destinatario()
        {
            this._destino = String.Empty;
            this._direccion = String.Empty;
            this._IsActive = false;
        }
        #endregion
    }
}
