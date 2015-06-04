using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public interface IDocumentoEncontrado
    {
        int IdEntrada { get; set; }
        string FolioEntrada {get; set; }
    }
}
