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

namespace ModelCasc.report.operation
{
    public class DocEntrada
    {
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

        public static void getEntradaAlm(string FilePath, string TemplatePath, string TemplatePathPallet, Entrada oE)
        {
            try
            {
                List<string> files = new List<string>();
                string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                fillEntradaAlm(fileName, TemplatePath, oE);
                files.Add(fileName);

                Tarima_almacen oTA1 = null;
                Tarima_almacen oTA2 = null;

                for (int indTar = 1; indTar <= oE.PLstTarAlm.Count; indTar += 2)
                {
                    oTA1 = oE.PLstTarAlm[indTar - 1];
                    
                    if (indTar < oE.PLstTarAlm.Count)
                        oTA2 = oE.PLstTarAlm[indTar];
                    else
                        oTA2 = new Tarima_almacen();
                    fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                    fillEntradaAlmTar(fileName, TemplatePathPallet, oTA1, oTA2, oE);
                    files.Add(fileName);
                }

                //int btoResiduo = oE.No_bulto_recibido % oE.No_pieza_declarada;
                //if (btoResiduo != 0)
                //{
                //    oTA1 = oE.PLstTarAlm[oE.PLstTarAlm.Count - 1];
                //    oTA2 = new Tarima_almacen();
                //    fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                //    fillEntradaAlmTar(fileName, TemplatePathPallet, oTA1, oTA2, oE);
                //    files.Add(fileName);
                //}

                DocConcat.ConcatPdfFiles(files.ToArray(), FilePath);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void fillEntradaAlm(string FilePath, string TemplatePath, Entrada oE)
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
                //fields.SetField("Origen", oE.Origen);

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

                for (int iETC = 1; iETC <= oE.PLstEntTransCond.Count; iETC++)
                {
                    Entrada_transporte_condicion oETC = oE.PLstEntTransCond[iETC - 1];
                    fields.SetField(iETC.ToString() + "_" + (oETC.Si_no ? "si" : "no"), "X");
                }



                fields.SetField("codigoRR", oE.Referencia);

                fields.SetField("MercanciaCodigo", oE.PCliente.PClienteMercancia.Codigo);
                fields.SetField("Mercancia", oE.PCliente.PClienteMercancia.Nombre);

                fields.SetField("proveedor", CatalogCtrl.Cliente_vendorfillByCliente(1, oE.Origen).First().Nombre);

                fields.SetField("Sello", oE.Sello);
                fields.SetField("Talon", oE.Talon);
                fields.SetField("Custodia", oE.PCustodia.Nombre);
                fields.SetField("Operador", oE.Operador);


                fields.SetField("tar", oE.No_pallet.ToString());
                fields.SetField("totalBultos", oE.No_bulto_recibido.ToString());
                fields.SetField("bul", oE.No_bulto_recibido.ToString());
                fields.SetField("totalPiezas", oE.No_pieza_recibida.ToString());
                fields.SetField("pza", oE.No_pieza_recibida.ToString());
                fields.SetField("pzaXcaja", oE.No_caja_cinta_aduanal.ToString());
                fields.SetField("bultosXtarima", oE.No_pieza_declarada.ToString());

                //Calculo del estandar por tarima
                //int btoCompleto = oE.No_pieza_recibida / oE.No_caja_cinta_aduanal;
                int btoResiduo = oE.No_bulto_recibido % oE.No_pieza_declarada;
                int tarCompleta = oE.No_bulto_recibido / oE.No_pieza_declarada;
                fields.SetField("1_tar", tarCompleta.ToString());
                fields.SetField("1_bul", oE.No_pieza_declarada.ToString());
                fields.SetField("1_pzaXcaja", oE.No_caja_cinta_aduanal.ToString());
                fields.SetField("1_pieza_tot", (tarCompleta * oE.No_pieza_declarada * oE.No_caja_cinta_aduanal).ToString());

                fields.SetField("2_tar", "0");
                fields.SetField("2_bul", "0");
                fields.SetField("2_pzaXcaja", "0");
                fields.SetField("2_pieza_tot", "0");

                if (btoResiduo != 0)
                {
                    fields.SetField("2_tar", "1");
                    fields.SetField("2_bul", btoResiduo.ToString());
                    fields.SetField("2_pzaXcaja", oE.No_caja_cinta_aduanal.ToString());
                    fields.SetField("2_pieza_tot", (btoResiduo * oE.No_caja_cinta_aduanal).ToString());
                }

                int diferenciaBulto = oE.No_bulto_declarado - oE.No_bulto_recibido;
                fields.SetField("caja_sob", "0");
                fields.SetField("caja_fal", "0");

                fields.SetField("pieza_sob", "0");
                fields.SetField("pieza_fal", "0");

                if (diferenciaBulto > 0)
                {
                    fields.SetField("caja_fal", diferenciaBulto.ToString());
                    fields.SetField("pieza_fal", (diferenciaBulto * oE.No_caja_cinta_aduanal).ToString());
                }
                if (diferenciaBulto < 0)
                {
                    fields.SetField("caja_sob", Math.Abs(diferenciaBulto).ToString());
                    fields.SetField("pieza_sob", Math.Abs(diferenciaBulto * oE.No_caja_cinta_aduanal).ToString());
                }


                fields.SetField("caja_dan", oE.No_bulto_danado.ToString());
                fields.SetField("caja_abi", oE.No_bulto_abierto.ToString());

                fields.SetField("Almacen", oE.PUsuario.Nombre);
                fields.SetField("Vigilante", oE.Vigilante);
                fields.SetField("Observaciones", oE.Observaciones);

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

        private static void fillEntradaAlmTar(string FilePath, string TemplatePath, Tarima_almacen tarAlm1, Tarima_almacen tarAlm2, Entrada oE)
        {
            PdfReader reader = null;
            PdfStamper stamper = null;
            try
            {
                reader = new PdfReader(TemplatePath);
                stamper = new PdfStamper(reader, new FileStream(FilePath, FileMode.Create));

                AcroFields fields = stamper.AcroFields;

                fields.SetField("pallet_1", tarAlm1.Folio);
                fields.SetField("codigo_1", tarAlm1.Mercancia_codigo);
                fields.SetField("descripcion_1", tarAlm1.Mercancia_nombre);
                fields.SetField("estandar_1", tarAlm1.Estandar);
                fields.SetField("rr_1", tarAlm1.Rr);
                CultureInfo ci = new CultureInfo("es-MX");
                fields.SetField("fecha_1", oE.Fecha.ToString("dd \\de MMMM \\de yyyy", ci));

                fields.SetField("pallet_2", tarAlm2.Folio);
                fields.SetField("codigo_2", tarAlm2.Mercancia_codigo);
                fields.SetField("descripcion_2", tarAlm2.Mercancia_nombre);
                fields.SetField("estandar_2", tarAlm2.Estandar);
                fields.SetField("rr_2", tarAlm2.Rr);
                fields.SetField("fecha_2", oE.Fecha.ToString("dd \\de MMMM \\de yyyy", ci));

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
