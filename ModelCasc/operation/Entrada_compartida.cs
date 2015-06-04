using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Entrada_compartida : IEquatable<Entrada_compartida>
    {
        #region Campos
        protected int _id;
        protected int? _id_entrada;
        protected int _id_usuario;
        protected string _folio;
        protected string _referencia;
        protected bool _capturada;
        protected bool _isActive;
        #endregion

        #region Propiedades
        public int Id { get { return _id; } set { _id = value; } }
        public int? Id_entrada { get { return _id_entrada; } set { _id_entrada = value; } }
        public int Id_usuario { get { return _id_usuario; } set { _id_usuario = value; } }
        public string Folio { get { return _folio; } set { _folio = value; } }
        public string Referencia { get { return _referencia; } set { _referencia = value; } }
        public bool Capturada { get { return _capturada; } set { _capturada = value; } }
        public bool IsActive { get { return _isActive; } set { _isActive = value; } }
        #endregion

        #region Constructores
        public Entrada_compartida()
        {
            this._id_usuario = 0;
            this._folio = String.Empty;
            this._referencia = String.Empty;
            this._capturada = false;
            this._isActive = false;
        }
        #endregion

        public bool Equals(Entrada_compartida other)
        {
            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal. 
            return Folio.Equals(other.Folio); // Folio.Equals(other.fo Code.Equals(other.Code) && Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null. 
            int hashProductName = Folio == null ? 0 : Folio.GetHashCode();

            //Calculate the hash code for the product. 
            return hashProductName;
        }
    }
}
