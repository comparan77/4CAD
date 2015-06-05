using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace ModelCasc
{
    public class CommonCasc
    {
        public static string GetMaxRequestLength()
        {
            try
            {
                return CommonFunctions.GetMaxRequestLength().ToString();
            }
            catch
            {
                throw;
            }
        }

        public static string FormatDate(DateTime day, string format)
        {
            try
            {
                return CommonFunctions.FormatDate(day, format);
            }
            catch
            {
                throw;
            }
        }
    }
}
