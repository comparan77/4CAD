using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gios.Pdf;
using System.Data;
using System.Collections;
using System.Drawing;
using Model;
using ModelCasc.operation;
using ModelCasc.catalog;

namespace ModelCasc.report
{
    public class RptEntrada
    {
        
        private static void setHeader(PdfDocument PdfDocument, PdfPage PdfPage, Entrada oE)
        {
            PdfTextArea taBodega = new PdfTextArea(new Font("Verdana", 24, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 0, 20, 590, 65), ContentAlignment.MiddleRight, "Bodega: " + oE.PBodega.Nombre + "\nCortina: " + oE.PCortina.Nombre);
            PdfPage.Add(taBodega);

            PdfTextArea taTitleEntrada = new PdfTextArea(new Font("Verdana", 24, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 0, 70, 590, 30), ContentAlignment.MiddleCenter, "Entrada de Almacén");
            PdfPage.Add(taTitleEntrada);

            PdfTextArea taFolio = new PdfTextArea(new Font("Verdana", 18, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 0, 105, 590, 20), ContentAlignment.MiddleRight, "Folio: " + oE.Folio + oE.Folio_indice);
            PdfPage.Add(taFolio);
        }

        private static void setBody(PdfDocument PdfDocument, PdfPage PdfPage, Entrada oE)
        {
            int lblPositionY = 135;
            int lblMarginY = 30;
            int lblWidth = 500;

            PdfTextArea taFecha = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "Fecha: " + oE.Fecha.ToString("dd MMM yy").ToUpper() + " Hora: " + oE.Hora);
            PdfPage.Add(taFecha);

            lblPositionY += lblMarginY;
            PdfTextArea taCliente = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "Cliente: " + oE.PCliente.Razon);
            PdfPage.Add(taCliente);

            string pedimento = "N.A.";
            if (oE.PLstEntDoc.Exists(p => p.Id_documento == 1))
                pedimento = oE.PLstEntDoc.Find(p => p.Id_documento == 1).Referencia;

            lblPositionY += lblMarginY;
            PdfTextArea taPedimento = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "Pedimento: " + pedimento);
            PdfPage.Add(taPedimento);

            //string strPlaca = string.Empty;
            //if (string.Compare(oE.Placa, "N.A.") != 0)
            //{
            //    strPlaca = ", Placa: " + oE.Placa;
            //    if (string.Compare(oE.Caja1, "N.A.") != 0)
            //    {
            //        strPlaca += ", Caja 1: " + oE.Caja1;
            //        if (string.Compare(oE.Caja2, "N.A.") != 0)
            //            strPlaca += ", Caja2: " + oE.Caja2;
            //    }
            //}

            //lblPositionY += lblMarginY;
            //PdfTextArea taTransporte = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
            //    , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "Transporte: " + oE.PTransporte.Nombre + strPlaca);
            //PdfPage.Add(taTransporte);

            //lblPositionY += lblMarginY;
            //PdfTextArea taTipoTransporte = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
            //    , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "Tipo de transporte: " + oE.PTransporteTipo.Nombre);
            //PdfPage.Add(taTipoTransporte);

            lblPositionY += lblMarginY;
            PdfTextArea taTalon = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "Tal\\363n: " + (oE.Talon.Length == 0 ? "N.A." : oE.Talon) + "Sello: " + (oE.Sello.Length == 0 ? "N.A." : oE.Sello));
            PdfPage.Add(taTalon);

            lblPositionY += lblMarginY;
            PdfTextArea taCustodia = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "Custodia: " + oE.PCustodia.Nombre);
            PdfPage.Add(taCustodia);

            lblPositionY += lblMarginY;
            PdfTextArea taDocEntragados = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "DOCUMENTOS ENTREGADOS POR EL TRANSPORTISTA: ");
            PdfPage.Add(taDocEntragados);

            lblPositionY += lblMarginY - 10;
            int lblDocEntMarginY = 0;
            int lblPedCompMarginY = lblPositionY + lblDocEntMarginY;
            foreach (Entrada_documento oED in oE.PLstEntDoc)
            {
                PdfTextArea taDocEnt = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                    , new PdfArea(PdfDocument, 90, lblPositionY + lblDocEntMarginY, lblWidth-200, 20), ContentAlignment.MiddleLeft, oED.PDocumento.Nombre + ": " + oED.Referencia);
                PdfPage.Add(taDocEnt);
                lblDocEntMarginY += 20;
            }

            if (oE.PLstEntComp.Count > 1)
            {
                PdfTextArea taLblPedComp = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                        , new PdfArea(PdfDocument, 480, lblPedCompMarginY, 100, 20), ContentAlignment.MiddleLeft, "COMPARTIDO");
                PdfPage.Add(taLblPedComp);
                lblPedCompMarginY += 20;
                List<Entrada_compartida> lstEC = oE.PLstEntComp.FindAll(p => p.Id_entrada != oE.Id);
                foreach (Entrada_compartida oEC in lstEC)
                {
                    PdfTextArea taPedComp = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                        , new PdfArea(PdfDocument, 480, lblPedCompMarginY, 100, 20), ContentAlignment.MiddleLeft, oEC.Referencia);
                    PdfPage.Add(taPedComp);
                    lblPedCompMarginY += 20;
                }
            }

            lblPositionY += lblMarginY + lblDocEntMarginY - 20;
            PdfTextArea taBultoDanado = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "Bultos Da\\361ados: " + oE.No_bulto_danado.ToString());
            PdfPage.Add(taBultoDanado);

            lblMarginY = 25;

            lblPositionY += lblMarginY;
            PdfTextArea taBultoAbierto = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "Bultos Abiertos: " + oE.No_bulto_abierto.ToString());
            PdfPage.Add(taBultoAbierto);

            lblPositionY += lblMarginY;
            PdfTextArea taBultoFaltante = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "Bultos Faltantes: " + (oE.No_bulto_declarado > oE.No_bulto_recibido ? oE.No_bulto_declarado - oE.No_bulto_recibido : 0).ToString());
            PdfPage.Add(taBultoFaltante);

            lblPositionY += lblMarginY;
            PdfTextArea taBultoSobrante = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "Bultos Sobrantes: " + (oE.No_bulto_declarado < oE.No_bulto_recibido ? oE.No_bulto_recibido - oE.No_bulto_declarado : 0).ToString());
            PdfPage.Add(taBultoSobrante);

            lblPositionY += lblMarginY;
            PdfTextArea taCintaAduanal = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "Cajas con cinta Aduanal: " + oE.No_caja_cinta_aduanal.ToString());
            PdfPage.Add(taCintaAduanal);

            lblPositionY += lblMarginY;
            PdfTextArea taPallet = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "No. de Pallets: " + oE.No_pallet.ToString());
            PdfPage.Add(taPallet);

            lblPositionY += lblMarginY;
            PdfTextArea taButosRecibidos = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "No. Total de Bultos Recibidos: " + oE.No_bulto_recibido.ToString());
            PdfPage.Add(taButosRecibidos);

            lblPositionY += lblMarginY;
            PdfTextArea taPzaRecibida = new PdfTextArea(new Font("Verdana", 14, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 20, lblPositionY, lblWidth, 20), ContentAlignment.MiddleLeft, "Piezas que dice contener: " + oE.No_pieza_declarada.ToString());
            PdfPage.Add(taPzaRecibida);
        }

        private static void setFooter(PdfDocument PdfDocument, PdfPage PdfPage, Entrada oE)
        {
            int lblPositionY = 700;

            PdfTextArea taTransportista = new PdfTextArea(new Font("Verdana", 13, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 20, lblPositionY, 200, 90), ContentAlignment.TopLeft, "--------------------------------------------\nNombre y firma del Transportista Acepto los términos de recepci\\363n y estado de los bultos del presente embarque revisado en mi presencia");
            PdfPage.Add(taTransportista);

            PdfTextArea taAlmacenista = new PdfTextArea(new Font("Verdana", 13, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 255, lblPositionY - 15, 200, 15), ContentAlignment.TopLeft, oE.PUsuario.Nombre);
            PdfPage.Add(taAlmacenista);

            PdfTextArea taSignAlmacenista = new PdfTextArea(new Font("Verdana", 13, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 255, lblPositionY, 200, 90), ContentAlignment.TopLeft, "--------------------------------------\nNombre y firma de Almacén.");
            PdfPage.Add(taSignAlmacenista);

            PdfTextArea taVigilancia = new PdfTextArea(new Font("Verdana", 13, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 470, lblPositionY - 15, 200, 15), ContentAlignment.TopLeft, oE.Vigilante);
            PdfPage.Add(taVigilancia);

            PdfTextArea taSignVigilancia = new PdfTextArea(new Font("Verdana", 13, FontStyle.Regular), Color.Black
                , new PdfArea(PdfDocument, 470, lblPositionY, 200, 90), ContentAlignment.TopLeft, "---------------------------\nVIGILANCIA CASC.");
            PdfPage.Add(taSignVigilancia);
        }

        public static PdfDocument getPdf(Entrada oE, string pathImg)
        {
            PdfDocument myPdfDocument = new PdfDocument(PdfDocumentFormat.InCentimeters(21.59, 27.94));
            PdfPage newPdfPage = myPdfDocument.NewPage();

            // This will load the image without placing into the document. The good thing
            // is that the image will be written into the document just once even if we put it
            // more times and in different sizes and places!
            PdfImage LogoImage = myPdfDocument.NewImage(pathImg);
            //// now we start putting the logo into the right place with a high resoluton...
            newPdfPage.Add(LogoImage, 10, 20, 300);

            setHeader(myPdfDocument, newPdfPage, oE);
            setBody(myPdfDocument, newPdfPage, oE);
            setFooter(myPdfDocument, newPdfPage, oE);
            newPdfPage.SaveToDocument();

            return myPdfDocument;
        }

        public static void getReport(string path, Entrada oE, string pathImg)
        {
            PdfDocument myPdfDocument = new PdfDocument(PdfDocumentFormat.InCentimeters(21.59, 27.94));

            PdfPage newPdfPage = myPdfDocument.NewPage();

            // This will load the image without placing into the document. The good thing
            // is that the image will be written into the document just once even if we put it
            // more times and in different sizes and places!
            PdfImage LogoImage = myPdfDocument.NewImage(pathImg);
            //// now we start putting the logo into the right place with a high resoluton...
            newPdfPage.Add(LogoImage, 10, 20, 300);

            setHeader(myPdfDocument, newPdfPage, oE);
            setBody(myPdfDocument, newPdfPage, oE);
            setFooter(myPdfDocument, newPdfPage, oE);
            
            try
            {
                newPdfPage.SaveToDocument();
                myPdfDocument.SaveToFile(path);
            }
            catch
            {
                throw;
            }
        }
    }
}

