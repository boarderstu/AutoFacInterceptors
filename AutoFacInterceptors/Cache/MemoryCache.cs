using AutoFacInterceptors.Interfaces;
using System;
using System.Collections.Generic;

namespace AutoFacInterceptors.Cache
{
    public class MemoryCache : ICache
    {
        private static IDictionary<string, object> _internalCache;

        public MemoryCache()
        {
            _internalCache = new Dictionary<string, object>();
        }

        public T Get<T>(string key) where T : class
        {
            if (!_internalCache.ContainsKey(key))
            {
                return default(T);
            }

            var item = _internalCache[key];

            if (item is T)
            {
                return item as T;
            }

            return default(T);
        }

        public void Set(string key, object item)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("Key");
            }

            if (item == null)
            {
                throw new ArgumentNullException("Item");
            }

            //Extra glitter to overwrite an existing key - 
            // in a normal cache implementation, this will be done for you.
            if (_internalCache.ContainsKey(key))
            {
                _internalCache.Remove(key);
            }

            _internalCache.Add(key, item);
        }

        public bool Exists(string key)
        {
            return _internalCache.ContainsKey(key);
        }
    }
}
