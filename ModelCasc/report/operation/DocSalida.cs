using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ModelCasc.operation;
using System.Globalization;

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

                fields.SetField("Fecha", oS.Fecha.ToString("dd \\de MMM \\de yy", ci));
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
    }
}
