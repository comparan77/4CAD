using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCasc.operation
{
    public interface IAuditoriaCAEApp
    {
        int Id { get; set; }
        int Id_fk { get; set; }
        string Referencia { get; }
        string Informa { get; set; }
        DateTime Fecha { get; set; }
        string Relato { get; set; }
        string Vigilancia { get; set; }
        string Notificado { get; set; }
        List<IAudImage> PLstAudImg { get; set; }
        string prefixImg { get; }
        IAuditoriaCAECppMng Mng { get; }
    }
}
