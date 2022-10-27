using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.DAL.Encryption
{
    public class EncryptionService : IEncryptionService
    {
        public string Decrypt(string password)
        {
            /* string result = string.Empty;
             byte[] decryted = Convert.FromBase64String(password);
             //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
             result = System.Text.Encoding.Unicode.GetString(decryted);
             return result;
           */
            return null;
        }

        public string Encrypt(string password)
        {

            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
            /*
                string result = string.Empty;
                byte[] encryted = System.Text.Encoding.Unicode.GetBytes(password);
                result = Convert.ToBase64String(encryted);
                return result;
                */
        }
    }

}
