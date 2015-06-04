using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.operation;

namespace ModelCasc.report.operation
{
    internal class OrdeCargaRemComparer: IEqualityComparer<Salida_orden_carga_rem>
    {
        public bool Equals(Salida_orden_carga_rem x, Salida_orden_carga_rem y)
        {
            // Check whether the compared objects reference the same data. 
            if (Object.ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null. 
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            // Check whether the products' properties are equal. 
            return x.PSalRem.Referencia == y.PSalRem.Referencia;
        }

        public int GetHashCode(Salida_orden_carga_rem obj)
        {
            // Check whether the object is null. 
        if (Object.ReferenceEquals(obj, null)) return 0;

        // Get the hash code for the Name field if it is not null. 
        int hashProductName = obj.PSalRem == null ? 0 : obj.PSalRem.Referencia.GetHashCode();

        // Get the hash code for the Code field. 
        int hashProductCode = obj.PSalRem.Referencia.GetHashCode();

        // Calculate the hash code for the product. 
        return hashProductName ^ hashProductCode;
        }
    }
}
