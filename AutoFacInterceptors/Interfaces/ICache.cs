﻿namespace AutoFacInterceptors.Interfaces
{
    public interface ICache
    {
        T Get<T>(string key) where T : class; 

        void Set(string key, object item);

        bool Exists(string key);
    }
}