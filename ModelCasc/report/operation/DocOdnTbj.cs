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

                string pathPhoto = Directory.GetParent(path).ToString();

                for (int indFile = 0; indFile < oTS.PLstPasos.Count; indFile++)
                {
                    Maquila_paso mp = oTS.PLstPasos[indFile];
                    dr = dtPaso.NewRow();
                    dr["descripcion"] = mp.Descripcion;
                    dr["imagen"] = DocFormatos.getImg(Path.Combine(pathPhoto, mp.Foto64));
                    dr["numpaso"] = mp.NumPaso;
                    dtPaso.Rows.Add(dr);
                }

                reporte.SetDataSource(ds.Tables["maqpaso"]);

                #region Encabezado
                reporte.SetParameterValue("servicio", oTS.PServ.Nombre);
                reporte.SetParameterValue("referencia", oTS.POrdTbj.Referencia_entrada);
                reporte.SetParameterValue("folio", oTS.POrdTbj.Folio);
                reporte.SetParameterValue("supervisor", oTS.POrdTbj.Supervisor);
                reporte.SetParameterValue("piezas", oTS.Piezas);
                reporte.SetParameterValue("ref1", oTS.Ref1);
                reporte.SetParameterValue("ref2", oTS.Ref2);
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
