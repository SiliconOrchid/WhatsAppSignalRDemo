using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace WhatsAppSignalRDemo.Web
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            DefaultFilesOptions options = new DefaultFilesOptions();
                options.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(options);

            app.UseStaticFiles();
        }
    }
}
