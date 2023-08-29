using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException(string key, params string[] args) : base(key, "You're forbidden", args)
        {
        }
    }
}
