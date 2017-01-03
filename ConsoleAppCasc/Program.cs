using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleAppCasc.facturacion;
using System.IO;
using System.Windows.Forms;

namespace ConsoleAppCasc
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //string origen = string.Empty;
                //string destino = string.Empty;

                //facturacionCtrl.procesaFacturacion(@"Z:\CaSC\2016\facturacionAvon\17-21_Octubre_16.xlsx", @"Z:\CaSC\2016\facturacionAvon\borra.xlsx");

                ////origen = args[0].ToString();
                ////destino = args[1].ToString();

                //LogCtrl.writeLog("--------------------------------------------------------------");
                //LogCtrl.writeLog("Inicia programa:" + DateTime.Now);
                //LogCtrl.writeLog("origen: " + origen);
                //LogCtrl.writeLog("destino: " + destino);

                //origen = origen.Replace("/", @"\");
                //destino = destino.Replace("/", @"\");

                //facturacionCtrl.procesaFacturacion(origen, destino);
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);
                Console.Read();
                LogCtrl.writeLog(e.Message);
            }
            
        }
    }
}
