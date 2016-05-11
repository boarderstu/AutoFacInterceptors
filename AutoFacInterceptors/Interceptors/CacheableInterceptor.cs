using AutoFacInterceptors.Interfaces;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

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
                var cacheResult = typeof(ICache).GetMethod("Get").MakeGenericMethod(invocation.Method.ReturnType)
                .Invoke(_cache, new object[] { attr.Key });
                invocation.ReturnValue = cacheResult;

                return;
            }

            invocation.Proceed();
            _cache.Set(attr.Key, invocation.ReturnValue);

        }
    }

    public class CacheableInterceptorWithKey : IInterceptor
    {
        ICache _cache;
        public CacheableInterceptorWithKey(ICache cache)
        {
            _cache = cache;
        }

        private string CreateKey(IInvocation invocation)
        {
            var dic = new Dictionary<string, string>();

            var sb = new StringBuilder();

            var p = invocation.Method.GetParameters();

            for (var i = 0; i < p.Length; i++)
            {
                var item = p[i];

                sb.Append($"{item.Name}:{invocation.Arguments[i]}");

            }
            return sb.ToString();


        }
        public void Intercept(IInvocation invocation)
        {
            var attr = Attribute.GetCustomAttribute(invocation.MethodInvocationTarget, typeof(CacheableAttribute)) as CacheableAttribute;

            if (attr == null)
            {
                return;
            }

            var cacheKey = CreateKey(invocation);

            Console.WriteLine($"Key: {cacheKey}");


            if (_cache.Exists(cacheKey))
            {
                var cacheResult = typeof(ICache).GetMethod("Get").MakeGenericMethod(invocation.Method.ReturnType)
                .Invoke(_cache, new object[] { cacheKey });
                invocation.ReturnValue = cacheResult;
                Console.WriteLine("CACHE HIT");
                return;
            }

            Console.WriteLine("CACHE MISS");
            invocation.Proceed();
            _cache.Set(cacheKey, invocation.ReturnValue);

        }
    }
}
