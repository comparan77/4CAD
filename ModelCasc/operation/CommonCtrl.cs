using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace ModelCasc.operation
{
    internal class CommonCtrl
    {
        public static void AddImgToDirectory(string path, string fileName, string img)
        {
            try
            {
                System.IO.Directory.CreateDirectory(path);
                string filePath = string.Empty;

                var bytes = Convert.FromBase64String(img);
                fileName = fileName + ".jpg";
                filePath = Path.Combine(path, fileName);
                using (var imageFile = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }
            }
            catch
            {
                throw;
            }
        }

        public static void AuditoriaAddImg(IAudImageMng oMng, IAuditoriaCAEApp o, IDbTransaction trans, string path)
        {
            try
            {
                //path = Path.Combine(path, o.Referencia + @"\");
                System.IO.Directory.CreateDirectory(path);
                string filePath = string.Empty;
                string fileName = string.Empty;
                for (int indEAUF = 0; indEAUF < o.PLstAudImg.Count; indEAUF++)
                {
                    IAudImage itemFile = o.PLstAudImg[indEAUF];
                    itemFile.Id_operation_aud = o.Id;

                    var bytes = Convert.FromBase64String(itemFile.Path);
                    fileName = o.prefixImg + indEAUF + "_.jpeg";
                    filePath = Path.Combine(path, fileName);
                    using (var imageFile = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.Write(bytes, 0, bytes.Length);
                        imageFile.Flush();
                    }
                    itemFile.Path = filePath;
                    oMng.O_Aud_Img = itemFile;
                    oMng.add(trans);
                }
            }
            catch
            {
                throw;
            }
        }

        #region Activity log

        internal static void ActivityLogAdd(Activity_log o, IDbTransaction trans)
        {
            try
            {
                Activity_logMng oMng = new Activity_logMng() { O_Activity_log = o };
                oMng.add(trans);
            }
            catch
            {

                throw;
            }
        }

        #endregion
    }
}
