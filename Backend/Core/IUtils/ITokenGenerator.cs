using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IUtils
{
    public interface ITokenGenerator
    {
        public (string token, DateTime expireAt) GenerateToken(Guid userId);
        string ValidateToken(string token);
    }
}
