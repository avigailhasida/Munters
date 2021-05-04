using System;
using Giphy.Api.Model;
using Giphy.Api.Persistence;
using Giphy.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Giphy.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<GiphyClientOptions>(Configuration.GetSection(GiphyClientOptions.GiphyClient));

            services.AddControllers();
            services.AddMemoryCache();
            services.AddLogging();
            services.AddHttpClient(GiphyClient.Name, c =>
            {
                c.BaseAddress = new Uri("https://api.giphy.com/v1/gifs/");
            });
            services.AddSingleton<GiphyClient>();
            services.AddSingleton<CacheManager>();
            services.AddSingleton<TrendingService>();
            services.AddSingleton<SearchService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Giphy.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Giphy.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
