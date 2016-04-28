using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.catalog;
using ModelCasc.operation.almacen;

namespace ModelCasc.operation
{
    public class IncidenciaCtrl
    {
        public static void AlmacenWH(Entrada oE, Tarima_almacen oTA, string mailFrom)
        {
            try
            {
                string msgSub = "Incidencia - RR:" + oE.Referencia + ", código:" + oTA.Mercancia_codigo;
                StringBuilder msgBody = new StringBuilder("<h1>Entrada con Incidencias</h1>");

                StringBuilder incidencia = new StringBuilder();
                if (oE.No_bulto_declarado != oE.No_bulto_recibido)
                {
                    int dif = Math.Abs(oE.No_bulto_declarado - oE.No_bulto_recibido);
                    incidencia.Append("<li>" + (oE.No_bulto_recibido < oE.No_bulto_declarado ? "Bultos Faltantes: " + dif.ToString() : "Bultos Sobrantes" + dif.ToString()) + "</li>");
                }

                if (oE.No_bulto_abierto > 0)
                    incidencia.Append("<li>Bultos Abiertos: " + oE.No_bulto_abierto.ToString() + "</li>");

                if (oE.No_bulto_danado > 0)
                    incidencia.Append("<li>Bultos Dañados: " + oE.No_bulto_danado.ToString() + "</li>");

                msgBody.Append("<p>C&oacute;digo:" + oTA.Mercancia_codigo + " Descripci&oacute;n" + oTA.Mercancia_nombre + "</p>");
                msgBody.Append("<ul>" + incidencia.ToString() + "</ul>");

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
