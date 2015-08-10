using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelCasc.operation;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Globalization;
using ModelCasc.webApp;

namespace ModelCasc.report.operation
{
    public class DocOrdenCarga
    {
        private static void addBarCodes(PdfStamper stamper, string folioOrdCarga)
        {
            PdfContentByte contentByte;
            try
            {
                contentByte = stamper.GetOverContent(1);
                int CTE_HEIGHT_CONST = 540;
                int CTE_X_POS_INI = 47;
                Image image = Image.GetInstance(BarCode.EncodeBytes(folioOrdCarga, false, 250, 100));
                image.SetAbsolutePosition(CTE_X_POS_INI, CTE_HEIGHT_CONST);// set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
                contentByte.AddImage(image);

            }
            catch
            {
                throw;
            }
        }

        public static void buildOrdenCarga(string FilePath, string TemplatePath, Salida_orden_carga o, List<Salida_orden_carga_rem> lst)
        {
            try
            {
                PdfReader reader = new PdfReader(TemplatePath);
                PdfStamper stamper = new PdfStamper(reader, new FileStream(FilePath, FileMode.Create));
                AcroFields fields = stamper.AcroFields;


                // set form fields

                fields.SetField("folio", o.Folio_orden_carga);
                fields.SetField("lblFolio", "FOLIO CARGA");

                CultureInfo ci = new CultureInfo("es-MX");
                fields.SetField("fecha_solicitud", o.PSalidaTrafico.Fecha_solicitud.ToString("dddd, dd \\de MMMM \\de yyyy", ci));
                fields.SetField("fecha_carga_solicitada", o.PSalidaTrafico.Fecha_carga_solicitada.ToString("dddd, dd \\de MMMM \\de yyyy", ci));
                fields.SetField("hora_carga_solicitada", o.PSalidaTrafico.Hora_carga_solicitada);
                fields.SetField("fecha_cita", Convert.ToDateTime(o.PSalidaTrafico.Fecha_cita).ToString("dddd, dd \\de MMMM \\de yyyy", ci));
                fields.SetField("hora_cita", o.PSalidaTrafico.Hora_cita);

                fields.SetField("tipo_carga", o.TipoCarga);

                IEnumerable<Salida_orden_carga_rem> lstDifPedimentos = o.LstRem.Distinct(new OrdeCargaRemComparer());
                int countDifPedimentos = 0;
                foreach (var salOrdCarRem in lstDifPedimentos)
                {
                    countDifPedimentos++;
                }

                o.TipoEnvio = (countDifPedimentos > 1 ? "COMPARTIDO" : "INDIVIDUAL");
                fields.SetField("tipo_envio", o.TipoEnvio);
                fields.SetField("destino", o.PSalidaTrafico.Destino);

                for (int iRem = 0; iRem < lst.Count; iRem++)
                {
                    Salida_orden_carga_rem oRem = lst[iRem];
                    fields.SetField("remision_" + iRem.ToString(), oRem.PSalRem.Folio_remision);
                    fields.SetField("referencia_" + iRem.ToString(), oRem.PSalRem.Referencia);
                    fields.SetField("codigo_" + iRem.ToString(), oRem.PSalRem.Codigo);
                    fields.SetField("orden_" + iRem.ToString(), oRem.PSalRem.Orden);
                }

                fields.SetField("lineaTransporte", o.PSalidaTrafico.Transporte);
                fields.SetField("tipoTransporte", o.PSalidaTrafico.Transporte_tipo_cita);

                stamper.FormFlattening = true;
                stamper.Close();
                reader.Close();

                //addBarCodes(FilePath, oSR);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public static void buildOrdenCarga(string FilePath, string TemplatePath, Salida_orden_carga o)
        //{
        //    try
        //    {
        //        PdfReader reader = new PdfReader(TemplatePath);
        //        PdfStamper stamper = new PdfStamper(reader, new FileStream(FilePath, FileMode.Create));
        //        AcroFields fields = stamper.AcroFields;


        //        // set form fields

        //        fields.SetField("folio", o.Folio_orden_carga);
        //        fields.SetField("lblFolio", "FOLIO CARGA");

        //        CultureInfo ci = new CultureInfo("es-MX");
        //        fields.SetField("fecha_solicitud", o.Fecha_solicitud.ToString("dddd, dd \\de MMMM \\de yyyy", ci));
        //        fields.SetField("fecha_carga_solicitada", o.Fecha_carga_solicitada.ToString("dddd, dd \\de MMMM \\de yyyy", ci));
        //        fields.SetField("hora_carga_solicitada", o.Hora_carga_solicitada);
        //        fields.SetField("fecha_cita", o.Fecha_cita.ToString("dddd, dd \\de MMMM \\de yyyy", ci));
        //        fields.SetField("hora_cita", o.Hora_cita);

        //        fields.SetField("tipo_carga", o.TipoCarga);
        //        //addBarCodes(stamper, o.Folio_orden_carga);

        //        IEnumerable<Salida_orden_carga_rem> lstDifPedimentos = o.LstRem.Distinct(new OrdeCargaRemComparer());
        //        int countDifPedimentos = 0;
        //        foreach (var salOrdCarRem in lstDifPedimentos)
        //        {
        //            countDifPedimentos++;
        //        }

        //        o.TipoEnvio = (countDifPedimentos > 1 ? "COMPARTIDO" : "INDIVIDUAL");
        //        fields.SetField("tipo_envio", o.TipoEnvio);
        //        fields.SetField("destino", o.Destino);

        //        for (int iRem = 0; iRem < o.LstRem.Count; iRem++)
        //        { 
        //            Salida_orden_carga_rem oRem = o.LstRem[iRem];
        //            fields.SetField("remision_" + iRem.ToString(), oRem.PSalRem.Folio_remision);
        //            fields.SetField("referencia_" + iRem.ToString(), oRem.PSalRem.Referencia);
        //            fields.SetField("codigo_" + iRem.ToString(), oRem.PSalRem.Codigo);
        //            fields.SetField("orden_" + iRem.ToString(), oRem.PSalRem.Orden);
        //        }

        //        fields.SetField("lineaTransporte", o.Transporte);
        //        fields.SetField("tipoTransporte", o.TipoTransporte);

        //        stamper.FormFlattening = true;
        //        stamper.Close();
        //        reader.Close();

        //        //addBarCodes(FilePath, oSR);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public static void getOrdenCarga(string FilePath, string TemplatePath, Salida_orden_carga o)
        {
            try
            {
                List<string> files = new List<string>();
                int noPag = o.LstRem.Count / Globals.ORDEN_CARGA_CANT_REM_X_HOJA; 
                int ultPag =o.LstRem.Count % Globals.ORDEN_CARGA_CANT_REM_X_HOJA; 
                int noRemImpresas = 0;
                for (int i = 0; i <= noPag; i++)
                {
                    string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                    List<Salida_orden_carga_rem> lst = o.LstRem.GetRange(noRemImpresas, i == noPag ? ultPag : Globals.ORDEN_CARGA_CANT_REM_X_HOJA);
                    buildOrdenCarga(fileName, TemplatePath, o, lst);
                    files.Add(fileName);
                    noRemImpresas += Globals.ORDEN_CARGA_CANT_REM_X_HOJA;
                }

                DocConcat.ConcatPdfFiles(files.ToArray(), FilePath);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
