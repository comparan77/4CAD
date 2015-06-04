using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ModelCasc.operation;
using System.Globalization;
using ModelCasc.webApp;
using ModelCasc.catalog;

namespace ModelCasc.report.operation
{
    public class DocRemision
    {
        private static void addBarCodes(PdfStamper stamper, Salida_remision oSR1)
        {
            //using (Stream inputPdfStream = new FileStream("input.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            //using (Stream inputImageStream = new FileStream("some_image.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
            //using (Stream outputPdfStream = new FileStream("result.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            //{
            //    var reader = new PdfReader(inputPdfStream);
            //    var stamper = new PdfStamper(reader, outputPdfStream);
            //    var pdfContentByte = stamper.GetOverContent(1);

            //    Image image = Image.GetInstance(inputImageStream);
            //    image.SetAbsolutePosition(100, 100);
            //    pdfContentByte.AddImage(image);
            //    stamper.Close();
            //}
            PdfContentByte contentByte;
            try
            {
                contentByte = stamper.GetOverContent(1);
                int CTE_HEIGHT_CONST = 440;
                int CTE_X_POS_INI = 47;
                int CTE_X_SPACE = 127;
                Image image = Image.GetInstance(BarCode.EncodeBytes(oSR1.Codigo, true));
                image.SetAbsolutePosition(CTE_X_POS_INI, CTE_HEIGHT_CONST);// set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
                //image.ScaleToFit(200, 25);
                contentByte.AddImage(image);

                //setAbsolutePosition 54 + 127
                image = Image.GetInstance(BarCode.EncodeBytes(oSR1.Orden, true));
                image.SetAbsolutePosition(CTE_X_POS_INI + CTE_X_SPACE, CTE_HEIGHT_CONST);// set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
                //image.ScaleToFit(200, 25);
                contentByte.AddImage(image);

                //var fs = new BinaryWriter(new FileStream(@"C:\Documents and Settings\Gilberto\Mis documentos\Visual Studio 2010\Projects\CASC\AppCasc\rpt\" + "\\codigo2.png", FileMode.Append, FileAccess.Write));
                //fs.Write(BarCode.EncodeBytes(oSR1.Vendor, true));
                //fs.Close();

                //181 + 127 + 127 
                //image = Image.GetInstance(@"C:\Documents and Settings\Gilberto\Mis documentos\Visual Studio 2010\Projects\CASC\AppCasc\rpt\codigo.png");
                //image = Image.GetInstance(@"C:\Documents and Settings\Gilberto\Mis documentos\Visual Studio 2010\Projects\CASC\AppCasc\rpt\codigo2.png");
                image = Image.GetInstance(BarCode.EncodeBytes(oSR1.Vendor, true));
                image.SetAbsolutePosition(CTE_X_POS_INI + CTE_X_SPACE * 3, CTE_HEIGHT_CONST);// set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
                //image.ScaleToFit(200, 25);
                contentByte.AddImage(image, true);

                //Código del folio de la remisión
                //image = Image.GetInstance(BarCode.EncodeBytes(oSR1.Id.ToString(), true));
                //image.SetAbsolutePosition(CTE_X_POS_INI + CTE_X_SPACE * 3 + 20, CTE_HEIGHT_CONST + 260);
                //contentByte.AddImage(image);
            }
            catch
            {
                throw;
            }
        }

        

        public static void getRemision(string FilePath, string TemplatePath, Salida_remision oSR)
        {
            try
            {
                PdfReader reader = new PdfReader(TemplatePath);
                PdfStamper stamper = new PdfStamper(reader, new FileStream(FilePath, FileMode.Create));
                AcroFields fields = stamper.AcroFields;
                

                // set form fields

                fields.SetField("folioRemision", oSR.Folio_remision);
                fields.SetField("lblFolioRemision", "FOLIO REMISION");

                CultureInfo ci = new CultureInfo("es-MX");
                fields.SetField("fecha_remision", oSR.Fecha_remision.ToString("dddd, dd \\de MMMM \\de yy", ci));

                fields.SetField("referencia", oSR.Referencia);
                Cliente oC = CatalogCtrl.ClienteGetByIdEntrada(oSR.Id_entrada);
                fields.SetField("cliente", oC.Razon.ToUpper());

                //fields.SetField("cliente"
                fields.SetField("proveedor", oSR.Proveedor);
                fields.SetField("proveedor_direccion", oSR.Proveedor_direccion);

                fields.SetField("codigo_k", oSR.Codigo);
                fields.SetField("codigo", oSR.Codigo);
                fields.SetField("orden_k", oSR.Orden);
                fields.SetField("orden", oSR.Orden);
                fields.SetField("vendor_k", oSR.Vendor);
                fields.SetField("vendor", oSR.Vendor);

                string lotePdf = string.Empty;
                string loteAct = string.Empty;
                string loteAnt = string.Empty;

                for (int indDet = 0; indDet < oSR.LstSRDetail.Count; indDet++)
                {
                    Salida_remision_detail item = oSR.LstSRDetail[indDet];
                    loteAct = item.Lote + string.Empty;
                    if (string.Compare(loteAct, loteAnt) != 0)
                    {
                        loteAnt = loteAct;
                        lotePdf += loteAct + ", ";
                    }
                    switch (indDet)
                    {
                        case 0:
                            fields.SetField("bulto", item.Bulto.ToString("N0"));
                            fields.SetField("piezaxb", item.Piezaxbulto.ToString("N0"));
                            fields.SetField("pieza", item.Piezas.ToString("N0"));
                            break;
                        case 1:
                            fields.SetField("bulto_i", item.Bulto.ToString("N0"));
                            fields.SetField("piezaxb_i", item.Piezaxbulto.ToString("N0"));
                            fields.SetField("pieza_i", item.Piezas.ToString("N0"));
                            break;
                        default:
                            break;
                    }
                }
                if (lotePdf.Length > 0)
                    lotePdf = lotePdf.Substring(0, lotePdf.Length - 2);
                fields.SetField("mercancia_k", oSR.Mercancia + (lotePdf.Length > 0 ? "\nLote: " + lotePdf : ""));
                addBarCodes(stamper, oSR);

                //fields.SetField("bulto", oSR.Bulto.ToString("N0"));
                //fields.SetField("bulto_i", oSR2.Bulto.ToString("N0"));

                //fields.SetField("piezaxb", oSR.Piezaxbulto.ToString("N0"));
                //fields.SetField("piezaxb_i", oSR2.Piezaxbulto.ToString("N0"));

                //fields.SetField("pieza", oSR.Pieza.ToString("N0"));
                //fields.SetField("pieza_i", oSR2.Pieza.ToString("N0"));

                //int total = oSR2.Pieza + oSR.Pieza;

                fields.SetField("piezatotal", oSR.PiezaTotal.ToString("N0"));


                fields.SetField("danada", "MERCANCÍA DAÑADA (" + oSR.Dano_especifico + ")");
                if (oSR.Dano_especifico.Trim().Length == 0)
                    fields.RemoveField("danada");

                fields.SetField("elaboro", oSR.Elaboro);
                fields.SetField("autorizo", oSR.Autorizo);

                stamper.FormFlattening = true;
                stamper.Close();
                reader.Close();

                //addBarCodes(FilePath, oSR1);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
