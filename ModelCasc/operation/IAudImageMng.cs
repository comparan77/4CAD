using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ModelCasc.operation
{
    public interface IAudImageMng
    {
        IAudImage O_Aud_Img { get; set; }
        List<IAudImage> Lst { get; set; }
        void add(IDbTransaction trans);
    }
}
