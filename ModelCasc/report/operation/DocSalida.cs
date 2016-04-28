using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ModelCasc.operation;
using System.Globalization;
using ModelCasc.operation.almacen;

namespace ModelCasc.report.operation
{
    public class DocSalida
    {
        public static void getSalida(string FilePath, string TemplatePath, Salida oS)
        {
            try
            {
                PdfReader reader = new PdfReader(TemplatePath);
                PdfStamper stamper = new PdfStamper(reader, new FileStream(FilePath, FileMode.Create));
                AcroFields fields = stamper.AcroFields;

                // set form fields

                fields.SetField("BodegaDireccion", oS.PBodega.Direccion);
                fields.SetField("Bodega", oS.PBodega.Nombre);
                fields.SetField("Cortina", oS.PCortina.Nombre);
                fields.SetField("Folio", oS.Folio + oS.Folio_indice);

                CultureInfo ci = new CultureInfo("es-MX");

                fields.SetField("Fecha", oS.Fecha.ToString("dd \\de MMM \\de yyyy", ci));
                fields.SetField("Hora", oS.Hora_salida.ToString());

                fields.SetField("Cliente", oS.PCliente.Razon);
                fields.SetField("Destino", oS.Destino);
                                
                fields.SetField("DocRef", oS.Referencia);

                fields.SetField("Mercancia", oS.Mercancia);

                fields.SetField("Transporte", oS.PTransporte.Nombre);
                fields.SetField("TipoTransporte", oS.PTransporteTipo.Nombre);
                string strPlaca = string.Empty;
                if (string.Compare(oS.Placa, "N.A.") != 0)
                {
                    strPlaca = "Placa: " + oS.Placa;
                    if(string.Compare(oS.Caja, "N.A.")!=0)
                        strPlaca += ", Caja: " + oS.Caja;
                    if (string.Compare(oS.Caja1, "N.A.") != 0)
                    {
                        strPlaca += ", Cont. 1: " + oS.Caja1;
                        if (string.Compare(oS.Caja2, "N.A.") != 0)
                            strPlaca += ", Cont. 2: " + oS.Caja2;
                    }
                    fields.SetField("Placas", strPlaca);
                }

                fields.SetField("Sello", oS.Sello);
                fields.SetField("Talon", oS.Talon);
                fields.SetField("Custodia", oS.PCustodia.Nombre);
                fields.SetField("Operador", oS.Operador);

                fields.SetField("DocAnexos", "SIN DOCUMENTOS");
                StringBuilder sbDocAnexos = new StringBuilder();
                foreach (Salida_documento oSD in oS.PLstSalDoc)
                {
                    sbDocAnexos.Append(oSD.PDocumento.Nombre + ": " + oSD.Referencia).AppendLine();
                }
                if (sbDocAnexos.Length > 0)
                    fields.SetField("DocAnexos", sbDocAnexos.ToString());


                StringBuilder sbCompartida = new StringBuilder();
                int saltoCompartida = 1;
                foreach (Salida_compartida oSC in oS.PLstSalComp)
                {
                    if (string.Compare(oS.Referencia, oSC.Referencia) != 0)
                    {
                        sbCompartida.Append(oSC.Referencia);
                        if (saltoCompartida % 2 == 0)
                        {
                            sbCompartida.AppendLine();
                            saltoCompartida = 0;
                        }
                        else
                            sbCompartida.Append(", ");
                        saltoCompartida++;
                    }
                }

                if (sbCompartida.Length == 0)
                    fields.SetField("Compartida", "NO COMPARTIDA");
                else
                    fields.SetField("Compartida", sbCompartida.ToString().Substring(0, sbCompartida.ToString().Length - 2));
                                                                
                fields.SetField("Pallets", oS.No_pallet.ToString());
                fields.SetField("Bultos", oS.No_bulto.ToString());
                fields.SetField("Piezas", oS.No_pieza.ToString());
                
                string NoSalida = string.Empty;
                if (oS.PSalPar != null && oS.PSalPar.Id > 0)
                {
                    fields.SetField("SalidaParcial","X");
                    fields.SetField("NoSalida", oS.PSalPar.No_salida.ToString());
                    if (oS.PSalPar.Es_ultima)
                        fields.SetField("UltimaParcial", "X");                        
                }
                else
                    fields.SetField("SalidaUnica", "X");

                fields.SetField("Almacen", oS.PUsuario.Nombre);
                fields.SetField("Vigilante", oS.Vigilante);
                fields.SetField("Observaciones", oS.Observaciones);

                stamper.FormFlattening = true;
                stamper.Close();
                reader.Close();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public static void getSalidaOC(string FilePath, string TemplatePath, Salida_orden_carga o)
        {
            try
            {
                List<string> files = new List<string>();
                int idSalida = 0;
                foreach(Salida_orden_carga_rem item in o.LstRem)
                {
                    if (idSalida != item.Id_salida)
                    {
                        idSalida = Convert.ToInt32(item.Id_salida);
                        string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                        getSalida(fileName, TemplatePath, SalidaCtrl.getAllDataById(idSalida));
                        files.Add(fileName);
                    }
                }

                DocConcat.ConcatPdfFiles(files.ToArray(), FilePath);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void getSalidaAlm(string FilePath, string TemplatePath, Salida oS)
        {
            try
            {
                List<string> files = new List<string>();
                string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                fillSalidaAlm(fileName, TemplatePath, oS);
                files.Add(fileName);

                fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                ExportToPdf(oS.PLstTarAlm, fileName, oS.Folio);
                files.Add(fileName);

                DocConcat.ConcatPdfFiles(files.ToArray(), FilePath);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void fillSalidaAlm(string FilePath, string TemplatePath, Salida oS)
        {
            try
            {
                PdfReader reader = new PdfReader(TemplatePath);
                PdfStamper stamper = new PdfStamper(reader, new FileStream(FilePath, FileMode.Create));
                AcroFields fields = stamper.AcroFields;

                // set form fields

                fields.SetField("BodegaDireccion", oS.PBodega.Direccion);
                fields.SetField("Bodega", oS.PBodega.Nombre);
                fields.SetField("Cortina", oS.PCortina.Nombre);
                fields.SetField("Folio", oS.Folio + oS.Folio_indice);

                CultureInfo ci = new CultureInfo("es-MX");

                fields.SetField("Fecha", oS.Fecha.ToString("dd \\de MMM \\de yyyy", ci));
                fields.SetField("Hora", oS.Hora_salida.ToString());

                fields.SetField("Cliente", oS.PCliente.Razon);
                fields.SetField("Destino", oS.Destino);

                fields.SetField("DocRef", oS.Referencia);

                fields.SetField("Mercancia", oS.Mercancia);

                fields.SetField("Transporte", oS.PTransporte.Nombre);
                fields.SetField("TipoTransporte", oS.PTransporteTipo.Nombre);
                string strPlaca = string.Empty;
                if (string.Compare(oS.Placa, "N.A.") != 0)
                {
                    strPlaca = "Placa: " + oS.Placa;
                    if (string.Compare(oS.Caja, "N.A.") != 0)
                        strPlaca += ", Caja: " + oS.Caja;
                    if (string.Compare(oS.Caja1, "N.A.") != 0)
                    {
                        strPlaca += ", Cont. 1: " + oS.Caja1;
                        if (string.Compare(oS.Caja2, "N.A.") != 0)
                            strPlaca += ", Cont. 2: " + oS.Caja2;
                    }
                    fields.SetField("Placas", strPlaca);
                }

                for (int iSTC = 1; iSTC <= oS.PLstSalTransCond.Count; iSTC++)
                {
                    Salida_transporte_condicion oSTC = oS.PLstSalTransCond[iSTC - 1];
                    fields.SetField(iSTC.ToString() + "_" + (oSTC.Si_no ? "si" : "no"), "X");
                }

                fields.SetField("Sello", oS.Sello);
                fields.SetField("Talon", oS.Talon);
                fields.SetField("Custodia", oS.PCustodia.Nombre);
                fields.SetField("Operador", oS.Operador);

                //fields.SetField("DocAnexos", "SIN DOCUMENTOS");
                //StringBuilder sbDocAnexos = new StringBuilder();
                //foreach (Salida_documento oSD in oS.PLstSalDoc)
                //{
                //    sbDocAnexos.Append(oSD.PDocumento.Nombre + ": " + oSD.Referencia).AppendLine();
                //}
                //if (sbDocAnexos.Length > 0)
                //    fields.SetField("DocAnexos", sbDocAnexos.ToString());


                //StringBuilder sbCompartida = new StringBuilder();
                //int saltoCompartida = 1;
                //foreach (Salida_compartida oSC in oS.PLstSalComp)
                //{
                //    if (string.Compare(oS.Referencia, oSC.Referencia) != 0)
                //    {
                //        sbCompartida.Append(oSC.Referencia);
                //        if (saltoCompartida % 2 == 0)
                //        {
                //            sbCompartida.AppendLine();
                //            saltoCompartida = 0;
                //        }
                //        else
                //            sbCompartida.Append(", ");
                //        saltoCompartida++;
                //    }
                //}

                //if (sbCompartida.Length == 0)
                //    fields.SetField("Compartida", "NO COMPARTIDA");
                //else
                //    fields.SetField("Compartida", sbCompartida.ToString().Substring(0, sbCompartida.ToString().Length - 2));

                fields.SetField("total_tarimas", oS.PLstTarAlm.Count.ToString());

                StringBuilder sbCodigos = new StringBuilder();
                IEnumerable<Tarima_almacen> lstDifCodigos = oS.PLstTarAlm.Distinct(new Tarima_almacenComparerCodigos());
                foreach (Tarima_almacen itemCodDif in lstDifCodigos)
                {
                    sbCodigos.Append(itemCodDif.Mercancia_codigo);
                    sbCodigos.Append(", ");
                }
                fields.SetField("docCodigos", sbCodigos.ToString().Substring(0, sbCodigos.ToString().Length - 2));

                //fields.SetField("Bultos", oS.No_bulto.ToString());
                //fields.SetField("Piezas", oS.No_pieza.ToString());

                //string NoSalida = string.Empty;
                //if (oS.PSalPar != null && oS.PSalPar.Id > 0)
                //{
                //    fields.SetField("SalidaParcial", "X");
                //    fields.SetField("NoSalida", oS.PSalPar.No_salida.ToString());
                //    if (oS.PSalPar.Es_ultima)
                //        fields.SetField("UltimaParcial", "X");
                //}
                //else
                //    fields.SetField("SalidaUnica", "X");

                fields.SetField("Almacen", oS.PUsuario.Nombre);
                fields.SetField("Vigilante", oS.Vigilante);
                fields.SetField("Observaciones", oS.Observaciones);

                stamper.FormFlattening = true;
                stamper.Close();
                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void ExportToPdf(List<Tarima_almacen> lst, string FilePath, string folioSalida)
        {
            Document document = new Document(PageSize.LEGAL, 20f, 20f, 55f, 20f);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(FilePath, FileMode.Create));
            document.Open();
            
            _events evtHeader = new _events("Folio:" + folioSalida);
            writer.PageEvent = evtHeader;


            iTextSharp.text.Font font = iTextSharp.text.FontFactory.GetFont(FontFactory.COURIER_BOLD, 6);

            PdfPTable table = new PdfPTable(6);
            
            
            //PdfPRow row = null;
            float[] widths = new float[] { 1.5f, 2f, 2f, 6f, 2f, 2f };

            table.SetWidths(widths);
            table.WidthPercentage = 100;
            table.SpacingAfter = 20;
            //int iCol = 0;
            //string colname = "";
            PdfPCell cell = new PdfPCell(new Phrase("Products"));
            
            cell.Colspan = 6;
            table.AddCell(new PdfPCell() { Phrase = new Phrase("RENGLÓN", font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
            table.AddCell(new PdfPCell() { Phrase = new Phrase("CODIGO", font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
            table.AddCell(new PdfPCell() { Phrase = new Phrase("PALLET", font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
            table.AddCell(new PdfPCell() { Phrase = new Phrase("MERCANCÍA", font), HorizontalAlignment = PdfPCell.ALIGN_LEFT });
            table.AddCell(new PdfPCell() { Phrase = new Phrase("ESTÁNDAR", font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
            table.AddCell(new PdfPCell() { Phrase = new Phrase("RR", font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });

            for (int indTA = 0; indTA < lst.Count; indTA++ )
            {
                Tarima_almacen itemTA = lst[indTA];
                table.AddCell(new PdfPCell() { Phrase = new Phrase((indTA + 1).ToString(), font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
                table.AddCell(new PdfPCell() { Phrase = new Phrase(itemTA.Mercancia_codigo, font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
                table.AddCell(new PdfPCell() { Phrase = new Phrase(itemTA.Folio, font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
                table.AddCell(new PdfPCell() { Phrase = new Phrase(itemTA.Mercancia_nombre, font), HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                table.AddCell(new PdfPCell() { Phrase = new Phrase(itemTA.Estandar, font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
                table.AddCell(new PdfPCell() { Phrase = new Phrase(itemTA.Rr, font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
            }

            document.Add(table);
            document.Close();
        }
    }

    class _events : PdfPageEventHelper
    {
        private string _title;
        public _events(string title)
        {
            this._title = title;
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfPTable table = new PdfPTable(1);
            //table.WidthPercentage = 100; //PdfPTable.writeselectedrows below didn't like this
            table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin; //this centers [table]
            PdfPTable table2 = new PdfPTable(2);

            //logo
            //PdfPCell cell2 = new PdfPCell(Image.GetInstance(@"C:\path\to\file.gif"));
            PdfPCell cell2 = new PdfPCell();
            cell2.Colspan = 2;
            table2.AddCell(cell2);

            //title
            cell2 = new PdfPCell(new Phrase("\n" + this._title, iTextSharp.text.FontFactory.GetFont(FontFactory.COURIER_BOLD, 6)));
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.Colspan = 2;
            table2.AddCell(cell2);

            PdfPCell cell = new PdfPCell(table2);
            table.AddCell(cell);

            table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 36, writer.DirectContent);
        }
    }
}
