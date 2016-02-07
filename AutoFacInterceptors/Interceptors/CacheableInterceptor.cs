using AutoFacInterceptors.Interfaces;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFacInterceptors.Interceptors
{
    public class CacheableInterceptor : IInterceptor
    {
        ICache _cache;
        public CacheableInterceptor(ICache cache)
        {
            _cache = cache;
        }
        public void Intercept(IInvocation invocation)
        {
            var attr = Attribute.GetCustomAttribute(invocation.MethodInvocationTarget, typeof(CacheableAttribute)) as CacheableAttribute;

            if (attr == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(attr.Key))
            {
                //Call the method, but don't cache anything and log
                invocation.Proceed();
            }

            if (_cache.Exists(attr.Key))
            {
                invocation.ReturnValue = _cache.Get(attr.Key);
                return;
            }

            invocation.Proceed();
            _cache.Set(attr.Key, invocation.ReturnValue);

        }
    }
}
