using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;

namespace ModelCasc.operation
{
    public class IncidenciaCtrl
    {
        public static void OrdenTrabajo(Entrada oE, Entrada_maquila o, string mailFrom)
        {
            try
            {
                string msgSub = "Incidencia - Pedimento:" + oE.Referencia + ", orden:" + oE.PEntInv.Orden_compra + ", código:" + oE.PEntInv.Codigo;
                StringBuilder msgBody = new StringBuilder("<h1>Maquila con Incidencias</h1>");
                msgBody.Append("<p>Código:" + oE.PEntInv.Codigo + " hay " + (o.Pieza_faltante > 0 ? o.Pieza_faltante.ToString() + "(s) Faltante(s)" : o.Pieza_sobrante.ToString() + "(s) Sobrante(s)") + "</p>");

                Cliente_ejecutivoMng oCEMng = new Cliente_ejecutivoMng() { O_Cliente_ejecutivo = new Cliente_ejecutivo() { Id = oE.Id_cliente } };
                oCEMng.fillByCliente();
                string mailTo = string.Empty;
                foreach (Cliente_ejecutivo item in oCEMng.Lst)
                {
                    mailTo += item.Email + ",";
                }
                mailTo = mailTo.Substring(0, mailTo.Length - 1);
                Mail.SendMail(msgSub, msgBody.ToString(), mailTo, mailFrom);
            }
            catch
            {
                throw;
            }
        }
    }
}
