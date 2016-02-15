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
                //Console.Write(args[0]);
                Console.WriteLine(args[0].ToString());
                Console.WriteLine(args[1].ToString());

                facturacionCtrl.procesaFacturacion(args[0].ToString(), args[1].ToString());
                //facturacionCtrl.procesaFacturacion(@"Z:\CaSC\2016\facturacionAvon\Facturación 25-29 Enero 16.xlsx", @"Z:\CaSC\2016\facturacionAvon\borra.xlsx");
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);
                Console.Read();
            }
            
        }
    }
}
