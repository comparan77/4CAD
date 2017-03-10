using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using ModelCasc.catalog;
using ModelCasc.operation;

namespace ModelCasc.report.operation
{
    public class DocFormatos
    {
        public static void getCasc028(string FilePath, string rptPath, Entrada_pre_carga o)
        {
            try
            {
                CultureInfo ci = new CultureInfo("es-MX");
                ReportDocument reporte = new ReportDocument();
                reporte.Load(rptPath);

                #region Encabezado 2
                reporte.SetParameterValue("cliente", o.Cliente);
                reporte.SetParameterValue("referencia", o.Referencia);
                reporte.SetParameterValue("informa", o.PEntAudUni.Informa);
                reporte.SetParameterValue("lugar", o.Bodega);
                reporte.SetParameterValue("fecha", o.PEntAudUni.Fecha);
                reporte.SetParameterValue("informado", o.Ejecutivo);
                reporte.SetParameterValue("relato", o.PEntAudUni.Acta_informativa);
                reporte.SetParameterValue("vigilancia", o.PEntAudUni.Vigilante);
                reporte.SetParameterValue("testigo", string.Empty);
                reporte.SetParameterValue("notificado", o.PEntAudUni.Operador);
                #endregion
                reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, FilePath);
            }
            catch
            {
                throw;
            }
        }
    }
}
