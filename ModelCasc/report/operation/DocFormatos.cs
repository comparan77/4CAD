using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using ModelCasc.catalog;
using ModelCasc.operation;
using System.IO;

namespace ModelCasc.report.operation
{
    public class DocFormatos
    {
        public static void getCasc028(string FilePath, string rptPath, Entrada_pre_carga o, DataSet ds)
        {
            try
            {
                CultureInfo ci = new CultureInfo("es-MX");
                ReportDocument reporte = new ReportDocument();
                reporte.Load(rptPath);

                #region Imagenes
                DataTable dt028 = ds.Tables["casc028"];
                string fileImagePath = string.Empty;
                for (int indFile = 0; indFile < o.PEntAudUni.PLstEntAudUniFiles.Count; indFile++)
                {
                    Entrada_aud_uni_files itemFile = o.PEntAudUni.PLstEntAudUniFiles[indFile];
                    DataRow dr = dt028.NewRow();
                    dr["id"] = itemFile.Id;
                    fileImagePath = itemFile.Path;
                    FileStream fs = null;
                    // define te binary reader to read the bytes of image 
                    BinaryReader br;
                    // check the existance of image 
                    if (File.Exists(fileImagePath))
                    {
                        // open image in file stream 
                        fs = new FileStream(fileImagePath, FileMode.Open);
                    }
                    else
                    {
                        // if phot does not exist show the nophoto.jpg file 
                        //fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "NoPhoto.jpg", FileMode.Open);
                    }
                    // initialise the binary reader from file streamobject 
                    br = new BinaryReader(fs);
                    // define the byte array of filelength 
                    byte[] imgbyte = new byte[fs.Length + 1];
                    // read the bytes from the binary reader 
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
                    dr["img"] = imgbyte;
                    // add the image in bytearray 
                    dt028.Rows.Add(dr);
                    // add row into the datatable 
                    br.Close();
                    // close the binary reader 
                    fs.Close(); 

                }
                reporte.SetDataSource(ds.Tables["casc028"]);
                #endregion

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
