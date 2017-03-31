using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.operation;

namespace ModelCasc.report.operation
{
    public interface ICasc028
    {
        IAuditoriaCAEApp PAudOperation { get; }
        string Cliente { get; }
        string Referencia { get; }
        string Informa { get; }
        string Lugar { get; }
        DateTime Fecha { get; }
        string Informado { get; }
        string Relato { get; }
        string Vigilancia { get; }
        string Testigo { get; }
        string Notificado { get; }
    }
}
