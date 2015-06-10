using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Entrada_fondeoFacturaComparer : IEqualityComparer<Entrada_fondeo>
    {
        public bool Equals(Entrada_fondeo x, Entrada_fondeo y)
        {
            // Check whether the compared objects reference the same data. 
            if (Object.ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null. 
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            // Check whether the products' properties are equal. 
            return string.Compare(x.Factura, y.Factura) == 0;
        }

        public int GetHashCode(Entrada_fondeo obj)
        {
            // Check whether the object is null. 
            if (Object.ReferenceEquals(obj, null)) return 0;

            // Get the hash code for the Name field if it is not null. 
            int hashProductName = obj == null ? 0 : obj.Factura.GetHashCode();

            // Get the hash code for the Code field. 
            int hashProductCode = obj.Factura.GetHashCode();

            // Calculate the hash code for the product. 
            return hashProductName ^ hashProductCode;
        }
    }
}
