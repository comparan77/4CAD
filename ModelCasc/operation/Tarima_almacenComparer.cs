using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public class Tarima_almacenComparer : IEqualityComparer<Tarima_almacen>
    {
        public bool Equals(Tarima_almacen x, Tarima_almacen y)
        {
            // Check whether the compared objects reference the same data. 
            if (Object.ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null. 
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            // Check whether the products' properties are equal. 
            return string.Compare(x.Rr, y.Rr) == 0;
        }

        public int GetHashCode(Tarima_almacen obj)
        {
            // Check whether the object is null. 
            if (Object.ReferenceEquals(obj, null)) return 0;

            // Get the hash code for the Name field if it is not null. 
            int hashProductName = obj == null ? 0 : obj.Rr.GetHashCode();

            // Get the hash code for the Code field. 
            int hashProductCode = obj.Rr.GetHashCode();

            // Calculate the hash code for the product. 
            return hashProductName ^ hashProductCode;
        }
    }

    public class Tarima_almacenComparerCodigos : IEqualityComparer<Tarima_almacen>
    {
        public bool Equals(Tarima_almacen x, Tarima_almacen y)
        {
            // Check whether the compared objects reference the same data. 
            if (Object.ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null. 
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            // Check whether the products' properties are equal. 
            return string.Compare(x.Mercancia_codigo, y.Mercancia_codigo) == 0;
        }

        public int GetHashCode(Tarima_almacen obj)
        {
            // Check whether the object is null. 
            if (Object.ReferenceEquals(obj, null)) return 0;

            // Get the hash code for the Name field if it is not null. 
            int hashProductName = obj == null ? 0 : obj.Mercancia_codigo.GetHashCode();

            // Get the hash code for the Code field. 
            int hashProductCode = obj.Mercancia_codigo.GetHashCode();

            // Calculate the hash code for the product. 
            return hashProductName ^ hashProductCode;
        }
    }
}
