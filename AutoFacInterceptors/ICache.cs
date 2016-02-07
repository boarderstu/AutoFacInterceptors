namespace AutoFacInterceptors
{
    public interface ICache
    {
        T Get<T>(string key) where T : class; 

        void Set(string key, object item); 
    }
}