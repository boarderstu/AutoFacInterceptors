using AutoFacInterceptors.Interfaces;
using Autofac;
using AutoFacInterceptors.Cache;
using Autofac.Extras.DynamicProxy2;
using AutoFacInterceptors.Interceptors;

namespace AutoFacInterceptors
{
    class Program
    {
        static void Main(string[] args)
        {
            var di = Build();

            var worker = di.Resolve<IWorker>();

            var message = worker.GetMessage(12);
        }

        private static IContainer Build()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<CacheableInterceptor>().As<CacheableInterceptor>();

            builder.RegisterType<MemoryCache>().As<ICache>();

            builder.RegisterType<Worker>().As<IWorker>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(CacheableInterceptor));

            return builder.Build();
        }
    }
}
