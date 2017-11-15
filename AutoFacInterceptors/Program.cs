using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoFacInterceptors.Cache;
using AutoFacInterceptors.Interceptors;
using AutoFacInterceptors.Interfaces;
using System;

namespace AutoFacInterceptors
{
    class Program
    {
        static void Main(string[] args)
        {
            var di = Build();

            var worker = di.Resolve<IWorker>();

            var message = worker.GetMessage(12);

            message = worker.GetMessage(13);
            Console.WriteLine(message);


            message = worker.GetMessage(13);
            Console.WriteLine(message);

            message = worker.GetMessage(13);
            Console.WriteLine(message);
            Console.ReadKey();
        }

        private static IContainer Build()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<CacheableInterceptorWithKey>().As<CacheableInterceptorWithKey>();

            builder.RegisterType<MemoryCache>().As<ICache>();

            builder.RegisterType<Worker>().As<IWorker>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(CacheableInterceptorWithKey));

            return builder.Build();
        }
    }
}
