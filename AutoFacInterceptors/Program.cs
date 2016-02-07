using AutoFacInterceptors.Interfaces;
using Autofac;
using AutoFacInterceptors.Cache;

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

            builder.RegisterType<MemoryCache>().As<ICache>();

            builder.RegisterType<Worker>().As<IWorker>();

            return builder.Build();
        }
    }
}
