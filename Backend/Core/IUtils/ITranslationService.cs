using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IUtils
{
    public interface ITranslationService
    {
        string GetByKey(string key, string langKey = "EN", params string[] args);
    }
}
