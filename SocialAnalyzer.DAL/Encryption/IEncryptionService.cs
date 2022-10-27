using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.DAL.Encryption
{
    public interface IEncryptionService
    {
        string Encrypt(string password);
        string Decrypt(string password);
    }
}
