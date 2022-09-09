using CSharp2TScript.Service.Interface;
using CSharp2TScript.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CSharp2TScript
{
    class Program
    {
        public static void Main(string[] args) =>
             ConfigureServices(args).
                  GetService<App>()!.
                  Run();

        private static ServiceProvider ConfigureServices(string[] args)
        {
            var serviceColl = new ServiceCollection();

            // Services
            serviceColl.AddTransient<IFormatService, FormatService>();

            // Main App
            serviceColl.AddTransient<App>();

            return serviceColl.BuildServiceProvider();
        }
    }
}