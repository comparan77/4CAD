using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public interface IAudImage
    {
        int Id { get; set; }
        int Id_operation_aud { get; set; }
        string Path { get; set; }
    }
}
