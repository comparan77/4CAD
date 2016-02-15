using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleAppCasc.facturacion;


namespace ConsoleAppCasc
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                facturacionCtrl.procesaFacturacion(args[0].ToString(), args[1].ToString());
                //facturacionCtrl.procesaFacturacion(@"Z:\CaSC\2016\facturacionAvon\facturacionCorta.xlsx", @"Z:\CaSC\2016\facturacionAvon\borra.xlsx");
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);
                Console.Read();
            }
            
        }
    }
}
