using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(WebNursePlanning.Areas.Identity.IdentityHostingStartup))]

namespace WebNursePlanning.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}