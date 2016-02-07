using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFacInterceptors
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheableAttribute : Attribute
    {
    }
}
