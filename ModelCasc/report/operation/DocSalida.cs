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
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

namespace ModelCasc.report.operation
{
    public class DocSalida
    {
        public static void getSalida(string path, string rptPath, Salida oS, DataSet ds)
        {
            try
            {
                CultureInfo ci = new CultureInfo("es-MX");
                ReportDocument reporte = new ReportDocument();
                reporte.Load(rptPath);

                foreach (Salida_documento oSD in oS.PLstSalDoc)
                {
                    DataRow dr = ds.Tables["entsaldoc"].NewRow();
                    dr["documento"] = oSD.PDocumento.Nombre;
                    dr["referencia"] = oSD.Referencia;
                    ds.Tables["entsaldoc"].Rows.Add(dr);
                }
                reporte.Subreports[0].SetDataSource(ds.Tables["entsaldoc"]);
                #region Datos de la entrada
                reporte.SetParameterValue("direccion_bodega", oS.PBodega.Direccion);
                reporte.SetParameterValue("bodega", oS.PBodega.Nombre);
                reporte.SetParameterValue("cortina", oS.PCortina.Nombre);
                reporte.SetParameterValue("cliente", oS.PCliente.Razon);
                reporte.SetParameterValue("folio", oS.Folio);
                reporte.SetParameterValue("fecha", oS.Fecha.ToString("dd \\de MMM \\de yyyy", ci));
                reporte.SetParameterValue("hora", oS.Hora_salida.ToString());
                reporte.SetParameterValue("destino", oS.Destino);
                //reporte.SetParameterValue("tipoEntrada", "Entrada Única");
                #endregion

                #region Datos de la mercancia
                reporte.SetParameterValue("documento", oS.Referencia);
                reporte.SetParameterValue("mercancia", oS.Mercancia);
                reporte.SetParameterValue("pallet", oS.No_pallet.ToString());
                reporte.SetParameterValue("bulto", oS.No_bulto.ToString());
                reporte.SetParameterValue("pieza", oS.No_pieza.ToString());

                reporte.SetParameterValue("unica", "---");
                reporte.SetParameterValue("parcial", "---");
                reporte.SetParameterValue("no_parcial", "---");
                reporte.SetParameterValue("es_ultima", "---");
                string NoSalida = string.Empty;
                if (oS.PSalPar != null && oS.PSalPar.Id > 0)
                {
                    reporte.SetParameterValue("parcial", "X");
                    reporte.SetParameterValue("no_parcial", oS.PSalPar.No_salida.ToString());
                    if (oS.PSalPar.Es_ultima)
                        reporte.SetParameterValue("es_ultima", "X");
                }
                else
                    reporte.SetParameterValue("unica", "X");
                #endregion

                #region Documentos Salida
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
                    reporte.SetParameterValue("compartidas", "NO COMPARTIDA");
                else
                    reporte.SetParameterValue("compartidas", sbCompartida.ToString().Substring(0, sbCompartida.ToString().Length - 2));

                #endregion

                #region Datos del Transporte

                StringBuilder sbET = new StringBuilder();

                sbET.Append(oS.PTransporte.Nombre + ", Tipo: " + oS.PTransporteTipo.Nombre);

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
                    sbET.Append(" " + strPlaca);
                }

                reporte.SetParameterValue("transporte", sbET.ToString());
                reporte.SetParameterValue("sello", oS.Sello);
                reporte.SetParameterValue("custodia", oS.PCustodia.Nombre);

                #endregion

                #region Otros Datos

                //reporte.SetParameterValue("horaDescarga", oS.Hora_descarga.ToString());
                //reporte.SetParameterValue("tipoDescarga", oS.PTipoCarga.Nombre);
                reporte.SetParameterValue("observaciones", oS.Observaciones);

                #endregion

                #region Firmas

                reporte.SetParameterValue("operador", oS.Operador);
                reporte.SetParameterValue("usuario", oS.PUsuario.Nombre);
                reporte.SetParameterValue("vigilante", oS.Vigilante);

                #endregion

                reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, path);
            }
            catch
            {
                
                throw;
            }
        }

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

        //public static void getSalidaAlm(string FilePath, string TemplatePath, Salida oS)
        //{
        //    try
        //    {
        //        List<string> files = new List<string>();
        //        string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
        //        fillSalidaAlm(fileName, TemplatePath, oS);
        //        files.Add(fileName);

        //        fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
        //        ExportToPdf(oS.PLstTarAlm, fileName, oS.Folio);
        //        files.Add(fileName);

        //        DocConcat.ConcatPdfFiles(files.ToArray(), FilePath);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //private static void fillSalidaAlm(string FilePath, string TemplatePath, Salida oS)
        //{
        //    try
        //    {
        //        PdfReader reader = new PdfReader(TemplatePath);
        //        PdfStamper stamper = new PdfStamper(reader, new FileStream(FilePath, FileMode.Create));
        //        AcroFields fields = stamper.AcroFields;

        //        // set form fields

        //        fields.SetField("BodegaDireccion", oS.PBodega.Direccion);
        //        fields.SetField("Bodega", oS.PBodega.Nombre);
        //        fields.SetField("Cortina", oS.PCortina.Nombre);
        //        fields.SetField("Folio", oS.Folio + oS.Folio_indice);

        //        CultureInfo ci = new CultureInfo("es-MX");

        //        fields.SetField("Fecha", oS.Fecha.ToString("dd \\de MMM \\de yyyy", ci));
        //        fields.SetField("Hora", oS.Hora_salida.ToString());

        //        fields.SetField("Cliente", oS.PCliente.Razon);
        //        fields.SetField("Destino", oS.Destino);

        //        fields.SetField("DocRef", oS.Referencia);

        //        fields.SetField("Mercancia", oS.Mercancia);

        //        fields.SetField("Transporte", oS.PTransporte.Nombre);
        //        fields.SetField("TipoTransporte", oS.PTransporteTipo.Nombre);
        //        string strPlaca = string.Empty;
        //        if (string.Compare(oS.Placa, "N.A.") != 0)
        //        {
        //            strPlaca = "Placa: " + oS.Placa;
        //            if (string.Compare(oS.Caja, "N.A.") != 0)
        //                strPlaca += ", Caja: " + oS.Caja;
        //            if (string.Compare(oS.Caja1, "N.A.") != 0)
        //            {
        //                strPlaca += ", Cont. 1: " + oS.Caja1;
        //                if (string.Compare(oS.Caja2, "N.A.") != 0)
        //                    strPlaca += ", Cont. 2: " + oS.Caja2;
        //            }
        //            fields.SetField("Placas", strPlaca);
        //        }

        //        for (int iSTC = 1; iSTC <= oS.PLstSalTransCond.Count; iSTC++)
        //        {
        //            Salida_transporte_condicion oSTC = oS.PLstSalTransCond[iSTC - 1];
        //            fields.SetField(iSTC.ToString() + "_" + (oSTC.Si_no ? "si" : "no"), "X");
        //        }

        //        fields.SetField("Sello", oS.Sello);
        //        fields.SetField("Talon", oS.Talon);
        //        fields.SetField("Custodia", oS.PCustodia.Nombre);
        //        fields.SetField("Operador", oS.Operador);

        //        //fields.SetField("DocAnexos", "SIN DOCUMENTOS");
        //        //StringBuilder sbDocAnexos = new StringBuilder();
        //        //foreach (Salida_documento oSD in oS.PLstSalDoc)
        //        //{
        //        //    sbDocAnexos.Append(oSD.PDocumento.Nombre + ": " + oSD.Referencia).AppendLine();
        //        //}
        //        //if (sbDocAnexos.Length > 0)
        //        //    fields.SetField("DocAnexos", sbDocAnexos.ToString());


        //        //StringBuilder sbCompartida = new StringBuilder();
        //        //int saltoCompartida = 1;
        //        //foreach (Salida_compartida oSC in oS.PLstSalComp)
        //        //{
        //        //    if (string.Compare(oS.Referencia, oSC.Referencia) != 0)
        //        //    {
        //        //        sbCompartida.Append(oSC.Referencia);
        //        //        if (saltoCompartida % 2 == 0)
        //        //        {
        //        //            sbCompartida.AppendLine();
        //        //            saltoCompartida = 0;
        //        //        }
        //        //        else
        //        //            sbCompartida.Append(", ");
        //        //        saltoCompartida++;
        //        //    }
        //        //}

        //        //if (sbCompartida.Length == 0)
        //        //    fields.SetField("Compartida", "NO COMPARTIDA");
        //        //else
        //        //    fields.SetField("Compartida", sbCompartida.ToString().Substring(0, sbCompartida.ToString().Length - 2));

        //        fields.SetField("total_tarimas", oS.PLstTarAlm.Count.ToString());

        //        StringBuilder sbCodigos = new StringBuilder();
        //        IEnumerable<Tarima_almacen> lstDifCodigos = oS.PLstTarAlm.Distinct(new Tarima_almacenComparerCodigos());
        //        foreach (Tarima_almacen itemCodDif in lstDifCodigos)
        //        {
        //            sbCodigos.Append(itemCodDif.Mercancia_codigo);
        //            sbCodigos.Append(", ");
        //        }
        //        fields.SetField("docCodigos", sbCodigos.ToString().Substring(0, sbCodigos.ToString().Length - 2));

        //        //fields.SetField("Bultos", oS.No_bulto.ToString());
        //        //fields.SetField("Piezas", oS.No_pieza.ToString());

        //        //string NoSalida = string.Empty;
        //        //if (oS.PSalPar != null && oS.PSalPar.Id > 0)
        //        //{
        //        //    fields.SetField("SalidaParcial", "X");
        //        //    fields.SetField("NoSalida", oS.PSalPar.No_salida.ToString());
        //        //    if (oS.PSalPar.Es_ultima)
        //        //        fields.SetField("UltimaParcial", "X");
        //        //}
        //        //else
        //        //    fields.SetField("SalidaUnica", "X");

        //        fields.SetField("Almacen", oS.PUsuario.Nombre);
        //        fields.SetField("Vigilante", oS.Vigilante);
        //        fields.SetField("Observaciones", oS.Observaciones);

        //        stamper.FormFlattening = true;
        //        stamper.Close();
        //        reader.Close();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public static void ExportToPdf(List<Tarima_almacen> lst, string FilePath, string folioSalida)
        //{
        //    Document document = new Document(PageSize.LEGAL, 20f, 20f, 55f, 20f);
        //    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(FilePath, FileMode.Create));
        //    document.Open();
            
        //    _events evtHeader = new _events("Folio:" + folioSalida);
        //    writer.PageEvent = evtHeader;


        //    iTextSharp.text.Font font = iTextSharp.text.FontFactory.GetFont(FontFactory.COURIER_BOLD, 6);

        //    PdfPTable table = new PdfPTable(6);
            
            
        //    //PdfPRow row = null;
        //    float[] widths = new float[] { 1.5f, 2f, 2f, 6f, 2f, 2f };

        //    table.SetWidths(widths);
        //    table.WidthPercentage = 100;
        //    table.SpacingAfter = 20;
        //    //int iCol = 0;
        //    //string colname = "";
        //    PdfPCell cell = new PdfPCell(new Phrase("Products"));
            
        //    cell.Colspan = 6;
        //    table.AddCell(new PdfPCell() { Phrase = new Phrase("RENGLÓN", font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
        //    table.AddCell(new PdfPCell() { Phrase = new Phrase("CODIGO", font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
        //    table.AddCell(new PdfPCell() { Phrase = new Phrase("PALLET", font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
        //    table.AddCell(new PdfPCell() { Phrase = new Phrase("MERCANCÍA", font), HorizontalAlignment = PdfPCell.ALIGN_LEFT });
        //    table.AddCell(new PdfPCell() { Phrase = new Phrase("ESTÁNDAR", font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
        //    table.AddCell(new PdfPCell() { Phrase = new Phrase("RR", font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });

        //    for (int indTA = 0; indTA < lst.Count; indTA++ )
        //    {
        //        Tarima_almacen itemTA = lst[indTA];
        //        table.AddCell(new PdfPCell() { Phrase = new Phrase((indTA + 1).ToString(), font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
        //        table.AddCell(new PdfPCell() { Phrase = new Phrase(itemTA.Mercancia_codigo, font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
        //        table.AddCell(new PdfPCell() { Phrase = new Phrase(itemTA.Folio, font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
        //        table.AddCell(new PdfPCell() { Phrase = new Phrase(itemTA.Mercancia_nombre, font), HorizontalAlignment = PdfPCell.ALIGN_LEFT });
        //        table.AddCell(new PdfPCell() { Phrase = new Phrase(itemTA.Estandar, font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
        //        table.AddCell(new PdfPCell() { Phrase = new Phrase(itemTA.Rr, font), HorizontalAlignment = PdfPCell.ALIGN_CENTER });
        //    }

        //    document.Add(table);
        //    document.Close();
        //}

        #region Salida Almacen

        public static void getSalidaAlm(string FilePath, string TemplatePath, Salida oS, DataSet ds)
        {
            try
            {
                List<string> files = new List<string>();
                string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";

                fillSalidaAlm(fileName, TemplatePath, oS, ds);
                files.Add(fileName);

                fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                fillSalidaAlmTar(fileName, TemplatePath, oS, ds);
                files.Add(fileName);

                DocConcat.ConcatPdfFiles(files.ToArray(), FilePath);
            }
            catch
            {
                throw;
            }
        }

        private static void fillSalidaAlm(string FilePath, string rptPath, Salida oS, DataSet ds)
        {
            ReportDocument reporte = new ReportDocument();
            try
            {
                CultureInfo ci = new CultureInfo("es-MX");
                
                reporte.Load(rptPath);

                foreach (Salida_transporte_condicion itemSTC in oS.PLstSalTransCond)
                {
                    DataRow dr = ds.Tables["entrada_transporte_condicion"].NewRow();
                    dr["condicion"] = itemSTC.Condicion;
                    dr["si_no"] = itemSTC.Si_no;
                    ds.Tables["entrada_transporte_condicion"].Rows.Add(dr);
                }

                foreach (Tarima_almacen_carga_format itemTACf in oS.PTAlmCarga.PLstTACRpt)
                {
                    DataRow dr = ds.Tables["salida"].NewRow();
                    dr["folio_remision"] = itemTACf.Folio_remision;
                    dr["codigo"] = itemTACf.Mercancia_codigo;
                    dr["rr"] = itemTACf.Rr;
                    dr["tarimas"] = itemTACf.Tarimas;
                    dr["cajas"] = itemTACf.Bultos;
                    dr["piezas"] = itemTACf.Piezas;
                    ds.Tables["salida"].Rows.Add(dr);
                }

                reporte.Subreports[0].SetDataSource(ds.Tables["entrada_transporte_condicion"]);
                reporte.SetDataSource(ds.Tables["salida"]);

                reporte.SetParameterValue("direccion_bodega", oS.PBodega.Direccion);
                reporte.SetParameterValue("bodega", oS.PBodega.Nombre);
                reporte.SetParameterValue("cortina", oS.PCortina.Nombre);
                reporte.SetParameterValue("cliente", oS.PCliente.Razon);
                reporte.SetParameterValue("folio", oS.Folio);
                reporte.SetParameterValue("fecha", oS.Fecha.ToString("dd \\de MMM \\de yyyy", ci));
                reporte.SetParameterValue("hora", oS.Hora_salida.ToString());
                reporte.SetParameterValue("destino", oS.Destino);

                //Transporte
                reporte.SetParameterValue("sello", oS.Sello);
                reporte.SetParameterValue("carta_porte", oS.Talon);
                reporte.SetParameterValue("custodia", oS.PCustodia.Nombre);
                reporte.SetParameterValue("operador", oS.Operador);
                StringBuilder strTransporte = new StringBuilder();
                strTransporte.Append("Linea: " + oS.PTransporte.Nombre);
                strTransporte.Append(", Tipo: " + oS.PTransporteTipo.Nombre);
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
                    strTransporte.Append(". " + strPlaca);
                }

                reporte.SetParameterValue("transporte", strTransporte.ToString());

                //pie reporte
                reporte.SetParameterValue("observaciones", oS.Observaciones);
                reporte.SetParameterValue("usuario", oS.PUsuario.Nombre);
                reporte.SetParameterValue("Vigilante", oS.Vigilante);

                reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, FilePath);
            }
            catch
            {

                throw;
            }
            finally
            {
                reporte.Close();
                reporte.Dispose();
            }
        }

        private static void fillSalidaAlmTar(string FilePath, string rptPath, Salida oS, DataSet ds)
        {
            ReportDocument reporte = new ReportDocument();
            try
            {
                CultureInfo ci = new CultureInfo("es-MX");
                reporte.Load(rptPath.Replace("salm","sart"));

                foreach (Tarima_almacen itemTA in oS.PLstTarAlm)
                {
                    DataRow dr = ds.Tables["carga"].NewRow();
                    dr["folio_remision"] = itemTA.Mercancia_nombre;
                    dr["codigo"] = itemTA.Mercancia_codigo;
                    dr["rr"] = itemTA.Rr;
                    dr["folio_tarima"] = itemTA.Folio;
                    dr["estandar"] = itemTA.Estandar;
                    dr["bultos"] = itemTA.Bultos;
                    dr["piezas"] = itemTA.Piezas;
                    dr["resto"] = itemTA.Resto;
                    ds.Tables["carga"].Rows.Add(dr);
                }
                reporte.SetDataSource(ds.Tables["carga"]);
                reporte.SetParameterValue("folio_salida", oS.Folio);

                reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, FilePath);
            }
            catch
            {
                throw;
            }
            finally
            {
                reporte.Close();
                reporte.Dispose();
            }
        }

        #endregion

        public static void getSalidaAlmXls(string FilePath, string rptPath, Salida oS, DataSet ds)
        {
            ReportDocument reporte = new ReportDocument();
            try
            {
                CultureInfo ci = new CultureInfo("es-MX");
                reporte.Load(rptPath.Replace("salm", "sart"));

                foreach (Tarima_almacen itemTA in oS.PLstTarAlm)
                {
                    DataRow dr = ds.Tables["carga"].NewRow();
                    dr["folio_remision"] = itemTA.Mercancia_nombre;
                    dr["codigo"] = itemTA.Mercancia_codigo;
                    dr["rr"] = itemTA.Rr;
                    dr["folio_tarima"] = itemTA.Folio;
                    dr["estandar"] = itemTA.Estandar;
                    dr["bultos"] = itemTA.Bultos;
                    dr["piezas"] = itemTA.Piezas;
                    dr["resto"] = itemTA.Resto;
                    ds.Tables["carga"].Rows.Add(dr);
                }
                reporte.SetDataSource(ds.Tables["carga"]);
                reporte.SetParameterValue("folio_salida", oS.Folio);

                reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, FilePath);
            }
            catch
            {
                throw;
            }
            finally
            {
                reporte.Close();
                reporte.Dispose();
            }
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
