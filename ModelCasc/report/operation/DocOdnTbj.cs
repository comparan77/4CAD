using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.operation;
using iTextSharp.text.pdf;
using System.IO;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

namespace ModelCasc.report.operation
{
    public class DocOdnTbj
    {
        public static void getOdnTbjSrv(string path, string rptPath, Orden_trabajo_servicio oTS, DataSet ds)
        {
            try
            {
                CultureInfo ci = new CultureInfo("es-MX");
                ReportDocument reporte = new ReportDocument();
                reporte.Load(rptPath);

                DataTable dtPaso = ds.Tables["maqpaso"];
                string fileImagePath = string.Empty;

                DataRow dr = null;
                for (int indFile = 0; indFile < oTS.PLstPasos.Count; indFile++)
                {
                    Maquila_paso mp = oTS.PLstPasos[indFile];
                    dr = dtPaso.NewRow();
                    dr["descripcion"] = mp.Descripcion;
                    dr["imagen"] = DocFormatos.getImg(mp.Foto64);
                    dr["numpaso"] = mp.NumPaso;
                    dtPaso.Rows.Add(dr);
                }

                reporte.SetDataSource(ds.Tables["maqpaso"]);

                #region Encabezado
                //reporte.SetParameterValue("referencia", );
                #endregion

                reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, path);
            }
            catch
            {
                throw;
            }
        }
    }
}
