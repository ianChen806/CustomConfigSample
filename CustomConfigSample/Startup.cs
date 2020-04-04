using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;

namespace CustomConfigSample
{
    public static class MyConfigExtension
    {
        public static void GetMyConfig(this IServiceCollection services)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.Add(new MyConfigSource());

            var configurationRoot = configurationBuilder.Build();
            services.Configure<MyConfig>(configurationRoot);

            ChangeToken.OnChange(configurationRoot.GetReloadToken,
                                 () =>
                                 {
                                     Console.WriteLine(DateTime.Now.Ticks);
                                 });
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.GetMyConfig();

            services.AddControllers();
        }
    }
}