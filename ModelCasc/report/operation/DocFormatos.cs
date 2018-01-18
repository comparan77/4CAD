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
        public static byte[] getImg(string fileImagePath)
        {
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
            // add row into the datatable 
            br.Close();
            // close the binary reader 
            fs.Close();
            return imgbyte;
        }

        public static void getCasc028(string FilePath, string rptPath, ICasc028 o, DataSet ds)
        {
            try
            {
                CultureInfo ci = new CultureInfo("es-MX");
                ReportDocument reporte = new ReportDocument();
                reporte.Load(rptPath);

                #region Imagenes
                DataTable dt028 = ds.Tables["casc028"];
                string fileImagePath = string.Empty;
                int imgSide = 1;
                DataRow dr = null;
                for (int indFile = 0; indFile < o.PAudOperation.PLstAudImg.Count; indFile++)
                {
                    IAudImage itemFile = o.PAudOperation.PLstAudImg[indFile];
                    switch (imgSide)
                    {
                        case 1:
                            dr = dt028.NewRow();
                            dr["imgLeft"] = getImg(itemFile.Path);
                            dt028.Rows.Add(dr);
                            break;
                        case 2:
                            dr["imgCenter"] = getImg(itemFile.Path);
                            break;
                        case 3:
                            dr["imgRight"] = getImg(itemFile.Path);
                            imgSide = 0;
                            break;
                    }
                    imgSide++;
                    
                }
                reporte.SetDataSource(ds.Tables["casc028"]);
                #endregion
                #region Encabezado 2
                reporte.SetParameterValue("cliente", o.Cliente);
                reporte.SetParameterValue("referencia", o.Referencia);
                reporte.SetParameterValue("informa", o.Informa);
                reporte.SetParameterValue("lugar", o.Lugar);
                reporte.SetParameterValue("fecha", o.Fecha);
                reporte.SetParameterValue("informado", o.Informado);
                reporte.SetParameterValue("relato", o.Relato);
                reporte.SetParameterValue("vigilancia", o.Vigilancia);
                reporte.SetParameterValue("testigo", o.Testigo);
                reporte.SetParameterValue("notificado", o.Notificado);
                

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
