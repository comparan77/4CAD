using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleAppCasc
{
    public class LogCtrl
    {
        public static void writeLog(string msg)
        {
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {
                var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\log.txt";
                ostrm = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                ostrm.Close();
                writer = new StreamWriter(path, true);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open log.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(writer);
            Console.WriteLine(msg);
            Console.SetOut(oldOut);
            writer.Close();
        }
    }
}
