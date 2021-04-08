using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PoC.Calendar.MassInsert.App;
using System;
using System.Threading.Tasks;

namespace PoC.Calendar.MassInsert
{
  class Program
  {
    static async Task Main(string[] args)
    {
      var host = Host.CreateDefaultBuilder()
         .ConfigureServices((context, services) => {
           services.AddHttpClient();
           services.AddSingleton<IAppHost, AppHost>();
         }).Build();

      await host.Services.GetService<IAppHost>().RunAsync();
    }
  }
}
