using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public interface IAuditoriaCAECppMng
    {
        IAuditoriaCAEApp O_aud { get; set; }
        void selByIdWithImg();
    }
}
