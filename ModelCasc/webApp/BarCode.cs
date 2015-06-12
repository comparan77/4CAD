using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using BarcodeLib;
using System.IO;

namespace ModelCasc.webApp
{
    public class BarCode
    {
        public static string Encode(string StringToEncode)
        {
            string base64String = Convert.ToBase64String(EncodeBytes(StringToEncode));
            return base64String;
        }

        public static Byte[] EncodeBytes(string StringToEncode, bool IsForPdf = false, int width = 150, int height = 50)
        {

            Barcode b = new Barcode();
            Byte[] result = null;
            //Image img;
            MemoryStream ms = new System.IO.MemoryStream();
            if (!IsForPdf)
            {
                b.Encode(TYPE.CODE128, StringToEncode, Color.Black, Color.Transparent, width, height);
                result = b.GetImageData(SaveTypes.PNG);
            }
            else
            {
                b.Encode(TYPE.CODE128, StringToEncode, Color.Black, Color.White, 130, 40);
                result = b.GetImageData(SaveTypes.BMP);
            }
            return result;
            //if (!IsForPdf)
            //{
            //    img = b.Encode(TYPE.CODE128, StringToEncode, Color.Black, Color.Transparent, 150, 50);
            //    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //}
            //else
            //{
            //    b.Encode(TYPE.CODE128, StringToEncode, Color.Black, Color.White, 150, 50);
            //    ms = b.GetImageData(SaveTypes.PNG);
            //}
            //return  ms.ToArray();
        }
    }
}
