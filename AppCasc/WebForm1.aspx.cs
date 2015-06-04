using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace AppCasc
{

    public class Encryption
    {
        private readonly string secretKey;
        public Encryption(string secretKey)
        {
            this.secretKey = secretKey;
        }
        public string Encrypt(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(Encrypt(plainBytes, GetRijndaelManaged(secretKey)));
        }
        public string Decrypt(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            return Encoding.UTF8.GetString(Decrypt(encryptedBytes, GetRijndaelManaged(secretKey)));
        }
        private byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            byte[] transformFinalBlock = rijndaelManaged.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            return transformFinalBlock;
        }
        private byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }
        private RijndaelManaged GetRijndaelManaged(string key)
        {
            var keyBytes = new byte[16];
            byte[] secretKeyBytes = Encoding.UTF8.GetBytes(key);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            var rijndaelManaged = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = keyBytes
            };
            return rijndaelManaged;
        }
    }

    public partial class WebForm1 : System.Web.UI.Page
    {
        private const string AesIV = @"!QAZ2WSX#EDC4RFV";
        private const string AesKey = @"5TGB&YHN7UJM(IK<";
        protected string Message;
        protected string EncryptMsg;

        private int encodeToNumber(string dato)
        {

            string number = string.Empty;
            int length = dato.Length;
            for (int i = 0; i < length; i++)
                number += ((int)dato[i]).ToString();
            return Convert.ToInt32(number.Substring(0,6));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Message = DateTime.Now.ToString("yyyyMMddHHmmss");
                Encryption crypt = new Encryption("52855327-D1DC-4D");
                EncryptMsg = encodeToNumber( crypt.Encrypt(Message)).ToString();
                
                //aes.Key = Convert.FromBase64String("000102030405060708090a0b0c0d0e0f");
                //aes.IV =  Convert.FromBase64String("101112131415161718191a1b1c1d1e1f");
                //aes.Key = "000102030405060708090a0b0c0d0e0f";
                //aes.Key = GetBytes(ByteArrayToString(GetBytes("000102030405060708090a0b0c0d0e0f")));
                //aes.IV = GetBytes("101112131415161718191a1b1c1d1e1f");

                //using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                //{
                //    MemoryStream ms = new MemoryStream();
                //    CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                    
                //    cs.Write(bytes, 0, bytes.Length);
                //    cs.FlushFinalBlock();
                //    ms.Position = 0;
                //    bytes = new byte[ms.Length];
                //    ms.Read(bytes, 0, bytes.Length);
                //    ciphertext = Convert.ToBase64String(bytes);
                //}

               
                //using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                //{
                //    MemoryStream ms = new MemoryStream();
                //    CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write);
                //    byte[] bytes = Convert.FromBase64String(ciphertext);
                //    cs.Write(bytes, 0, bytes.Length);
                //    cs.FlushFinalBlock();
                //    ms.Position = 0;
                //    bytes = new byte[ms.Length];
                //    ms.Read(bytes, 0, bytes.Length);
                //    decryptedtext = utf8.GetString(bytes);
                //}    
            }
        }
    }
}