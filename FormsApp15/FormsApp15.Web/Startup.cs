using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FormsApp15.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseWebSockets();
            app.UseMiddleware<EasyChatService>();
            app.Run(async context => await context.Response.WriteAsync("Easy chat serivce"));
        }
    }
}
