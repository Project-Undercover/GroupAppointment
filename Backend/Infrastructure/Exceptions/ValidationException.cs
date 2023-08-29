using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(string key, params string[] args) : base(key, "Validation Exception", args)
        {
        }
    }
}
