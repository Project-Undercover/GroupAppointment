using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IUtils
{
    public interface IPasswordHash
    {
        public string Hash(string password);
        bool VerifyHash(string password, string hashedPassword);
    }
}
