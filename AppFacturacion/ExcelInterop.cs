using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace ConsoleAppCasc
{
    public class ExcelInterop
    {
        private Application xlApp;

        public void saveXls(Workbook xlWorkBook, Worksheet xlWorkSheet, string path)
        {
            try
            {
                object misValue = System.Reflection.Missing.Value;
                xlWorkBook.SaveAs(path, XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
            }
            catch
            {
                throw;
            }
        }

        public Workbook createExcel()
        {
            Workbook xlWorkBook = null;
            try
            {
                Application xlApp = new Application();

                if (xlApp == null)
                    throw new Exception("Excel is not properly installed!!");

                //Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                //xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            }
            catch
            {
                throw;
            }
            return xlWorkBook;            
        }

        public Workbook openBook(string path, bool readOnly = true)
        {
            Workbook xlWorkBook;
            try
            {
                
                object misValue = System.Reflection.Missing.Value;
                LogCtrl.writeLog(path);   
                xlApp = new Application();
                xlWorkBook = xlApp.Workbooks.Open(path, 0, readOnly, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            }
            catch (Exception e)
            {
                LogCtrl.writeLog("openBookError: " + e.Message);   
                throw;
            }
            return xlWorkBook;
        }

        public void closeBook(Workbook xlWorkBook, Worksheet xlWorkSheet=null, bool saveChanges=false)
        {
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook.Close(saveChanges, misValue, misValue);
            xlApp.Quit();

            if(xlWorkSheet!=null)
                releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        public void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
                //throw new Exception("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        } 
    }
}
