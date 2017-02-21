using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ModelCasc.operation;
using System.Globalization;
using ModelCasc.catalog;
using ModelCasc.operation.almacen;

using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using ModelCasc.webApp;

namespace ModelCasc.report.operation
{
    public class DocEntrada
    {
        private static void addBarCodes(PdfStamper stamper, Tarima_almacen tarAlm1, Tarima_almacen tarAlm2, Tarima_almacen tarAlm3, Tarima_almacen tarAlm4)
        {
            PdfContentByte contentByte;
            try
            {
                contentByte = stamper.GetOverContent(1);
                int CTE_HEIGHT_CONST = 430;
                int CTE_X_POS_INI = 30;
                int CTE_Y_SPACE = -375;

                Image image = Image.GetInstance(BarCode.EncodeBytes(tarAlm1.Folio, false, 300, 50));
                image.SetAbsolutePosition(CTE_X_POS_INI, CTE_HEIGHT_CONST);// set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
                //image.ScaleToFit(200, 25);
                contentByte.AddImage(image);

                //setAbsolutePosition 54 + 127
                if (tarAlm2.Folio.Length > 0)
                {
                    image = Image.GetInstance(BarCode.EncodeBytes(tarAlm2.Folio, false, 300, 50));
                    image.SetAbsolutePosition(CTE_X_POS_INI + 280, CTE_HEIGHT_CONST);// set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
                    //image.ScaleToFit(200, 25);
                    contentByte.AddImage(image);
                }

                if (tarAlm3.Folio.Length > 0)
                {

                    image = Image.GetInstance(BarCode.EncodeBytes(tarAlm3.Folio, false, 300, 50));
                    image.SetAbsolutePosition(CTE_X_POS_INI, CTE_HEIGHT_CONST + CTE_Y_SPACE);// set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
                    //image.ScaleToFit(200, 25);
                    contentByte.AddImage(image);
                }

                if (tarAlm4.Folio.Length > 0)
                {

                    image = Image.GetInstance(BarCode.EncodeBytes(tarAlm4.Folio, false, 300, 50));
                    image.SetAbsolutePosition(CTE_X_POS_INI + 280, CTE_HEIGHT_CONST + CTE_Y_SPACE);// set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
                    //image.ScaleToFit(200, 25);
                    contentByte.AddImage(image);
                }
            }
            catch
            {
                throw;
            }
        }

        public static void getEntrada(string FilePath, string rptPath, Entrada oE, DataSet ds)
        {
            try
            {
                int bultoRecibido = 0;

                CultureInfo ci = new CultureInfo("es-MX");
                ReportDocument reporte = new ReportDocument();
                reporte.Load(rptPath);

                foreach (Entrada_documento oED in oE.PLstEntDoc)
                {
                    DataRow dr = ds.Tables["entsaldoc"].NewRow();
                    dr["documento"] = oED.PDocumento.Nombre;
                    dr["referencia"] = oED.Referencia;
                    ds.Tables["entsaldoc"].Rows.Add(dr);
                }
                reporte.Subreports[0].SetDataSource(ds.Tables["entsaldoc"]);
                #region Datos de la entrada
                reporte.SetParameterValue("direccion_bodega", oE.PBodega.Direccion);
                reporte.SetParameterValue("bodega", oE.PBodega.Nombre);
                reporte.SetParameterValue("cortina", oE.PCortina.Nombre);
                reporte.SetParameterValue("cliente", oE.PCliente.Razon);
                reporte.SetParameterValue("folio", oE.Folio + oE.Folio_indice);
                reporte.SetParameterValue("fecha", oE.Fecha.ToString("dd \\de MMM \\de yyyy", ci));
                reporte.SetParameterValue("hora", oE.Hora.ToString());
                reporte.SetParameterValue("origen", oE.Origen);
                reporte.SetParameterValue("tipoEntrada", "Entrada Única");
                #endregion

                #region Datos de la mercancia
                reporte.SetParameterValue("documento", oE.Referencia);
                reporte.SetParameterValue("mercancia", oE.Mercancia);
                reporte.SetParameterValue("pallet", oE.No_pallet.ToString());
                reporte.SetParameterValue("caja_declarada", oE.No_bulto_declarado.ToString());
                reporte.SetParameterValue("caja_recibida", oE.No_bulto_recibido.ToString());

                reporte.SetParameterValue("cajaxrecibir", 0);
                reporte.SetParameterValue("piezaxrecibir", 0);

                bultoRecibido = oE.No_bulto_recibido;

                if(oE.PEntPar !=null)
                    if (oE.PEntPar.No_entrada > 0)
                    {
                        reporte.SetParameterValue("tipoEntrada", "Parcial");
                        reporte.SetParameterValue("no_entrada", "No: " + oE.PEntPar.No_entrada.ToString() + (oE.PEntPar.Es_ultima ? " - Última" : string.Empty));
                        reporte.SetParameterValue("cajaxrecibir", Convert.ToString(oE.No_bulto_declarado - oE.No_bulto_recibido));
                        Entrada_parcial oEP = EntradaCtrl.ParcialGetByReferencia(oE.Referencia, true, oE.Id);
                        int piezasPorRecibir = oE.No_pieza_declarada - oEP.No_pieza_recibidas;
                        reporte.SetParameterValue("piezaxrecibir", piezasPorRecibir.ToString());
                        bultoRecibido = oEP.No_bulto_recibido;
                    }
                int diferenciaBulto = oE.No_bulto_declarado - bultoRecibido;
                reporte.SetParameterValue("caja_sobrante", "0");
                reporte.SetParameterValue("caja_faltante", "0");
                if (diferenciaBulto > 0)
                    reporte.SetParameterValue("caja_faltante", diferenciaBulto.ToString());
                if (diferenciaBulto < 0)
                    reporte.SetParameterValue("caja_sobrante", Math.Abs(diferenciaBulto).ToString());

                if (oE.PEntPar != null)
                    if (!oE.PEntPar.Es_ultima)
                    {
                        reporte.SetParameterValue("caja_faltante", 0);
                    }
                    else
                    {
                        reporte.SetParameterValue("cajaxrecibir", 0);
                        reporte.SetParameterValue("piezaxrecibir", 0);
                    }

                reporte.SetParameterValue("cajadanada", oE.No_bulto_danado.ToString());
                reporte.SetParameterValue("cajaabierta", oE.No_bulto_abierto.ToString());
                reporte.SetParameterValue("pieza_declarada", oE.No_pieza_declarada.ToString());
                reporte.SetParameterValue("pieza_recibida", oE.No_pieza_recibida.ToString());
                #endregion

                #region Documentos Entrada

                StringBuilder sbCompartida = new StringBuilder();
                int saltoCompartida = 1;
                List<Entrada_compartida> lstECActive = oE.PLstEntComp.FindAll(p => p.IsActive == true);
                foreach (Entrada_compartida oEC in lstECActive)
                {
                    if (string.Compare(oE.Referencia, oEC.Referencia) != 0)
                    {
                        sbCompartida.Append(oEC.Referencia);
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
                foreach (Entrada_transporte oET in oE.PLstEntTrans)
                {
                    sbET.Append("Linea: " + oET.Transporte_linea + ", Tipo: " + oET.Transporte_tipo);
                    if (string.Compare(oET.Placa, "N.A.") != 0)
                        sbET.Append(", Placa: " + oET.Placa);
                    if (string.Compare(oET.Caja, "N.A.") != 0)
                        sbET.Append(", Caja: " + oET.Caja);
                    if (string.Compare(oET.Caja1, "N.A.") != 0)
                        sbET.Append(", Contenedor 1: " + oET.Caja1);
                    if (string.Compare(oET.Caja2, "N.A.") != 0)
                        sbET.Append(", Contenedor 2: " + oET.Caja2);
                    sbET.AppendLine();
                }

                reporte.SetParameterValue("transporte", sbET.ToString());
                reporte.SetParameterValue("sello", oE.Sello);
                reporte.SetParameterValue("custodia", oE.PCustodia.Nombre);

                #endregion

                #region Otros Datos

                reporte.SetParameterValue("horaDescarga", oE.Hora_descarga.ToString());
                reporte.SetParameterValue("tipoDescarga", oE.PTipoCarga.Nombre);
                reporte.SetParameterValue("observaciones", "Se recibe unidad y/o contenedor sin daños o menoscabos. " + oE.Observaciones);

                #endregion

                #region Firmas

                reporte.SetParameterValue("operador", oE.Operador);
                reporte.SetParameterValue("usuario", oE.PUsuario.Nombre);
                reporte.SetParameterValue("vigilante", oE.Vigilante);

                #endregion

                reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, FilePath);
            }
            catch
            {
                throw;
            }
        }

        public static void getEntrada(string FilePath, string TemplatePath, Entrada oE)
        {
            PdfReader reader = null;
            PdfStamper stamper = null;
            try
            {
                reader = new PdfReader(TemplatePath);
                stamper = new PdfStamper(reader, new FileStream(FilePath, FileMode.Create));

                AcroFields fields = stamper.AcroFields;

                // set form fields

                fields.SetField("BodegaDireccion", oE.PBodega.Direccion);
                fields.SetField("Bodega", oE.PBodega.Nombre);
                fields.SetField("Cortina", oE.PCortina.Nombre);
                fields.SetField("Folio", oE.Folio + oE.Folio_indice);

                CultureInfo ci = new CultureInfo("es-MX");

                fields.SetField("Fecha", oE.Fecha.ToString("dd \\de MMM \\de yyyy", ci));
                fields.SetField("Hora", oE.Hora.ToString());

                fields.SetField("Cliente", oE.PCliente.Razon);
                fields.SetField("Origen", oE.Origen);

                StringBuilder sbET = new StringBuilder();
                foreach (Entrada_transporte oET in oE.PLstEntTrans)
                {
                    sbET.Append("Linea: " + oET.Transporte_linea + ", Tipo: " + oET.Transporte_tipo);
                    if (string.Compare(oET.Placa, "N.A.") != 0)
                        sbET.Append(", Placa: " + oET.Placa);
                    if (string.Compare(oET.Caja, "N.A.") != 0)
                        sbET.Append(", Caja: " + oET.Caja);
                    if (string.Compare(oET.Caja1, "N.A.") != 0)
                        sbET.Append(", Contenedor 1: " + oET.Caja1);
                    if (string.Compare(oET.Caja2, "N.A.") != 0)
                        sbET.Append(", Contenedor 2: " + oET.Caja2);
                    sbET.AppendLine();
                }
                fields.SetField("Transporte", sbET.ToString());

                fields.SetField("DocRef", oE.Referencia);

                fields.SetField("Mercancia", oE.Mercancia);
                fields.SetField("Sello", oE.Sello);
                fields.SetField("Talon", oE.Talon);
                fields.SetField("Custodia", oE.PCustodia.Nombre);
                fields.SetField("Operador", oE.Operador);

                StringBuilder sbDocAnexos = new StringBuilder();
                foreach (Entrada_documento oED in oE.PLstEntDoc)
                {
                    sbDocAnexos.Append(oED.PDocumento.Nombre + ": " + oED.Referencia).AppendLine();
                }
                fields.SetField("DocAnexos", sbDocAnexos.ToString());
                if (sbDocAnexos.Length == 0)
                    fields.SetField("DocAnexos", "SIN DOCUMENTOS");

                StringBuilder sbCompartida = new StringBuilder();
                int saltoCompartida = 1;
                List<Entrada_compartida> lstECActive = oE.PLstEntComp.FindAll(p => p.IsActive == true);
                foreach (Entrada_compartida oEC in lstECActive)
                {
                    if (string.Compare(oE.Referencia, oEC.Referencia) != 0)
                    {
                        sbCompartida.Append(oEC.Referencia);
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

                fields.SetField("NoEntrada", string.Empty);

                fields.SetField("TipoEntrada", "Única");
                fields.SetField("BultoPorRecibir", "0");
                if(oE.PEntPar !=null)
                    if (oE.PEntPar.No_entrada > 0)
                    {
                        fields.SetField("NoEntrada", "Parcial No: " + oE.PEntPar.No_entrada.ToString() + (oE.PEntPar.Es_ultima ? "-Ultima" : string.Empty));
                        fields.SetField("TipoEntrada", "Parcial");
                        fields.SetField("BultoPorRecibir", Convert.ToString(oE.No_bulto_declarado - oE.No_bulto_recibido));
                        Entrada_parcial oEP = EntradaCtrl.ParcialGetByReferencia(oE.Referencia, true);
                        int piezasPorRecibir = oE.No_pieza_declarada - oEP.No_pieza_recibidas;
                        fields.SetField("PiezaPorRecibir", piezasPorRecibir.ToString());
                    }
                if (!oE.IsActive)
                    if (!oE.Es_unica)
                        fields.SetField("NoEntrada", "Parcial Cancelada");

                fields.SetField("CintaAduanal", oE.No_caja_cinta_aduanal.ToString());
                fields.SetField("Pallet", oE.No_pallet.ToString());
                fields.SetField("BultoDeclarado", oE.No_bulto_declarado.ToString());
                fields.SetField("BultoRecibido", oE.No_bulto_recibido.ToString());

                int diferenciaBulto = oE.No_bulto_declarado - oE.No_bulto_recibido;
                fields.SetField("BultoSobrante", "0");
                fields.SetField("BultoFaltante", "0");
                if (diferenciaBulto > 0)
                    fields.SetField("BultoFaltante", diferenciaBulto.ToString());
                if(diferenciaBulto < 0)
                    fields.SetField("BultoSobrante", Math.Abs(diferenciaBulto).ToString());

                fields.SetField("PiezaDeclarada", oE.No_pieza_declarada.ToString());
                fields.SetField("PiezaRecibida", oE.No_pieza_recibida.ToString());
                fields.SetField("BultoDanado", oE.No_bulto_danado.ToString());
                fields.SetField("BultoAbierto", oE.No_bulto_abierto.ToString());

                fields.SetField("Almacen", oE.PUsuario.Nombre);
                fields.SetField("Vigilante", oE.Vigilante);
                fields.SetField("Observaciones", "Se recibe unidad y/o contenedor sin daños o menoscabos. " + oE.Observaciones);
                fields.SetField("tipo_carga", oE.PTipoCarga.Nombre);

                stamper.FormFlattening = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                stamper.Close();
                reader.Close();
            }
        }

        public static void getEntradaAlm(string FilePath, string TemplatePath, string TemplatePathPallet, Entrada oE, DataSet ds, bool withDetail = true)
        {
            try
            {
                List<string> files = new List<string>();
                string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";

                fillEntradaAlm(fileName, TemplatePath, oE, ds);
                files.Add(fileName);

                if (withDetail)
                {
                    

                    int maxTar = oE.PLstTarAlm.Count;

                    for (int indTar = 1; indTar <= maxTar; indTar += 4)
                    {
                        //oTA1 = oE.PLstTarAlm[indTar - 1];

                        //if (indTar < oE.PLstTarAlm.Count)
                        //    oTA2 = oE.PLstTarAlm[indTar];
                        //else
                        //    oTA2 = new Tarima_almacen();

                        Tarima_almacen oTA1 = new Tarima_almacen();
                        Tarima_almacen oTA2 = new Tarima_almacen();
                        Tarima_almacen oTA3 = new Tarima_almacen();
                        Tarima_almacen oTA4 = new Tarima_almacen();

                        oTA1 = oE.PLstTarAlm[indTar - 1];
                        if (indTar < maxTar)
                            oTA2 = oE.PLstTarAlm[indTar];
                        if (indTar + 1 < maxTar)
                            oTA3 = oE.PLstTarAlm[indTar + 1];
                        if (indTar + 2 < maxTar)
                            oTA4 = oE.PLstTarAlm[indTar + 2];

                        fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                        fillEntradaAlmTar(fileName, TemplatePathPallet, oTA1, oTA2, oTA3, oTA4, oE);
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

        private static void fillEntradaAlm(string FilePath, string rptPath, Entrada oE, DataSet ds)
        {
            
            try
            {
                CultureInfo ci = new CultureInfo("es-MX");
                ReportDocument reporte = new ReportDocument();
                reporte.Load(rptPath);

                List<Entrada_transporte_condicion> lstETC = AlmacenCtrl.entradaTransporteCondicionGet(oE.PLstEntTrans.First().Id);
                foreach (Entrada_transporte_condicion itemETC in lstETC)
                {
                    DataRow dr = ds.Tables["entrada_transporte_condicion"].NewRow();
                    dr["condicion"] = itemETC.Condicion;
                    dr["si_no"] = itemETC.Si_no;
                    ds.Tables["entrada_transporte_condicion"].Rows.Add(dr);
                }

                List<Tarima_almacen> lstTA = AlmacenCtrl.tarimaAlacenFillByEntrada(oE.Id);
                foreach (Tarima_almacen itemTA in lstTA)
                {
                    DataRow drTA = ds.Tables["entrada_tarima"].NewRow();
                    drTA["tarima"] = itemTA.Tarimas;
                    drTA["estandar"] = itemTA.Estandar;
                    drTA["cajas"] = itemTA.Bultos;
                    drTA["piezas"] = itemTA.Piezas;
                    ds.Tables["entrada_tarima"].Rows.Add(drTA);
                }

                reporte.Subreports[0].SetDataSource(ds.Tables["entrada_transporte_condicion"]);
                reporte.SetDataSource(ds.Tables["entrada_tarima"]);

                reporte.SetParameterValue("direccion_bodega", oE.PBodega.Direccion);
                reporte.SetParameterValue("bodega", oE.PBodega.Nombre);
                reporte.SetParameterValue("cortina", oE.PCortina.Nombre);
                reporte.SetParameterValue("cliente", oE.PCliente.Razon);
                reporte.SetParameterValue("folio", oE.Folio);
                reporte.SetParameterValue("fecha", oE.Fecha.ToString("dd \\de MMM \\de yyyy", ci));
                reporte.SetParameterValue("hora", oE.Hora.ToString());
                reporte.SetParameterValue("proveedor", CatalogCtrl.Cliente_vendorfillByCliente(1, oE.Origen).First().Nombre);
                reporte.SetParameterValue("mercancia_codigo", oE.PCliente.PClienteMercancia.Codigo);
                reporte.SetParameterValue("mercancia_descripcion", oE.PCliente.PClienteMercancia.Nombre);
                reporte.SetParameterValue("mercancia_tipo", oE.PCliente.PClienteMercancia.Negocio);

                reporte.SetParameterValue("caja_declarada", oE.No_bulto_declarado);
                reporte.SetParameterValue("caja_recibida", oE.No_bulto_recibido);

                int diferenciaBulto = oE.No_bulto_declarado - oE.No_bulto_recibido;
                reporte.SetParameterValue("caja_faltante", 0);
                reporte.SetParameterValue("caja_sobrante", 0);

                if (diferenciaBulto > 0)
                    reporte.SetParameterValue("caja_faltante", diferenciaBulto);
                if (diferenciaBulto < 0)
                    reporte.SetParameterValue("caja_sobrante", Math.Abs(diferenciaBulto));

                reporte.SetParameterValue("pieza_declarada", oE.No_pieza_declarada);
                reporte.SetParameterValue("pieza_recibida", oE.No_pieza_recibida);
                int diferenciaCaja = oE.No_pieza_declarada - oE.No_pieza_recibida;
                reporte.SetParameterValue("pieza_faltante", 0);
                reporte.SetParameterValue("pieza_sobrante", 0);

                if (diferenciaCaja > 0)
                    reporte.SetParameterValue("pieza_faltante", diferenciaCaja);
                if (diferenciaCaja < 0)
                    reporte.SetParameterValue("pieza_sobrante", Math.Abs(diferenciaCaja));



                reporte.SetParameterValue("caja_danada", oE.PLstTarAlm.Count(p => p.Resto > 0));
                reporte.SetParameterValue("caja_abierta", oE.PLstTarAlm.Sum(p=> p.Resto));
                reporte.SetParameterValue("rr", oE.Referencia);

                reporte.SetParameterValue("piezaxcaja", oE.PTarAlmEstd.Piezasxcaja);
                reporte.SetParameterValue("cajaxtarima", oE.PTarAlmEstd.Cajasxtarima);


                #region Transporte
                reporte.SetParameterValue("operador", oE.Operador);
                StringBuilder sbET = new StringBuilder();
                foreach (Entrada_transporte oET in oE.PLstEntTrans)
                {
                    sbET.Append("Linea: " + oET.Transporte_linea + ", Tipo: " + oET.Transporte_tipo);
                    if (string.Compare(oET.Placa, "N.A.") != 0)
                        sbET.Append(", Placa: " + oET.Placa);
                    if (string.Compare(oET.Caja, "N.A.") != 0)
                        sbET.Append(", Caja: " + oET.Caja);
                    if (string.Compare(oET.Caja1, "N.A.") != 0)
                        sbET.Append(", Contenedor 1: " + oET.Caja1);
                    if (string.Compare(oET.Caja2, "N.A.") != 0)
                        sbET.Append(", Contenedor 2: " + oET.Caja2);
                    sbET.AppendLine();
                }
                reporte.SetParameterValue("transporte", sbET.ToString());
                reporte.SetParameterValue("custodia", oE.PCustodia.Nombre);
                reporte.SetParameterValue("sello", oE.Sello);
                reporte.SetParameterValue("carta_porte", oE.Talon);

                #endregion

                reporte.SetParameterValue("hora_descarga", oE.Hora_descarga);
                reporte.SetParameterValue("observaciones", oE.Observaciones);
                reporte.SetParameterValue("usuario", oE.PUsuario.Nombre.ToUpper());
                reporte.SetParameterValue("vigilante", oE.Vigilante);

                reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, FilePath);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }

        private static void fillEntradaAlmTar(string FilePath, string TemplatePath, Tarima_almacen tarAlm1, Tarima_almacen tarAlm2, Tarima_almacen tarAlm3, Tarima_almacen tarAlm4, Entrada oE)
        {
            PdfReader reader = null;
            PdfStamper stamper = null;
            try
            {
                CultureInfo ci = new CultureInfo("es-MX");
                reader = new PdfReader(TemplatePath);
                stamper = new PdfStamper(reader, new FileStream(FilePath, FileMode.Create));

                AcroFields fields = stamper.AcroFields;

                fields.SetField("pallet_1", tarAlm1.Folio);
                fields.SetField("codigo_1", tarAlm1.Mercancia_codigo);
                fields.SetField("descripcion_1", tarAlm1.Mercancia_nombre);
                fields.SetField("bto_1", tarAlm1.Bultos.ToString());
                fields.SetField("resto_1", tarAlm1.Resto.ToString() + " Pz");
                fields.SetField("estandar_1", tarAlm1.Estandar);
                fields.SetField("rr_1", tarAlm1.Rr);
                fields.SetField("fecha_1", oE.Fecha.ToString("dd-MM-yyyy", ci));
                //if(!oE.IsActive)
                //    fields.SetField("cancelado_1", "CANCELADO");
                if (tarAlm2.Folio.Length > 0)
                {
                    fields.SetField("pallet_2", tarAlm2.Folio);
                    fields.SetField("codigo_2", tarAlm2.Mercancia_codigo);
                    fields.SetField("descripcion_2", tarAlm2.Mercancia_nombre);
                    fields.SetField("bto_2", tarAlm2.Bultos.ToString());
                    fields.SetField("resto_2", tarAlm2.Resto.ToString() + " Pz");
                    fields.SetField("estandar_2", tarAlm2.Estandar);
                    fields.SetField("rr_2", tarAlm2.Rr);
                    fields.SetField("fecha_2", oE.Fecha.ToString("dd-MM-yyyy", ci));
                    //if (!oE.IsActive)
                    //    fields.SetField("cancelado_2", "CANCELADO");
                }

                if (tarAlm3.Folio.Length > 0)
                {
                    fields.SetField("pallet_3", tarAlm3.Folio);
                    fields.SetField("codigo_3", tarAlm3.Mercancia_codigo);
                    fields.SetField("descripcion_3", tarAlm3.Mercancia_nombre);
                    fields.SetField("bto_3", tarAlm3.Bultos.ToString());
                    fields.SetField("resto_3", tarAlm3.Resto.ToString() + " Pz");
                    fields.SetField("estandar_3", tarAlm3.Estandar);
                    fields.SetField("rr_3", tarAlm3.Rr);
                    fields.SetField("fecha_3", oE.Fecha.ToString("dd-MM-yyyy", ci));
                    //if (!oE.IsActive)
                    //    fields.SetField("cancelado_2", "CANCELADO");
                }


                if (tarAlm4.Folio.Length > 0)
                {
                    fields.SetField("pallet_4", tarAlm4.Folio);
                    fields.SetField("codigo_4", tarAlm4.Mercancia_codigo);
                    fields.SetField("descripcion_4", tarAlm4.Mercancia_nombre);
                    fields.SetField("bto_4", tarAlm4.Bultos.ToString());
                    fields.SetField("resto_4", tarAlm4.Resto.ToString() + " Pz");
                    fields.SetField("estandar_4", tarAlm4.Estandar);
                    fields.SetField("rr_4", tarAlm4.Rr);
                    fields.SetField("fecha_4", oE.Fecha.ToString("dd-MM-yyyy", ci));
                    //if (!oE.IsActive)
                    //    fields.SetField("cancelado_2", "CANCELADO");
                }
                addBarCodes(stamper, tarAlm1, tarAlm2, tarAlm3, tarAlm4);

                stamper.FormFlattening = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                stamper.Close();
                reader.Close();
            }
        }
    }
}
