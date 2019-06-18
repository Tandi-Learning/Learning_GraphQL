using System;
using CarvedRock.Web.Clients;
using CarvedRock.Web.HttpClients;
using GraphQL.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarvedRock.Web
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(t => new GraphQLClient(_config["CarvedRockApiUri"]));
            services.AddSingleton<ProductGraphClient>();
            services.AddHttpClient<ProductHttpClient>(client => 
                client.BaseAddress = new Uri(_config["CarvedRockApiUri"]));
            // using HttpClientFactory
            services.AddHttpClient("ProductHttpClient", client => {
                client.BaseAddress = new Uri(_config["CarvedRockApiUri"]);
            });
            services.AddTransient<ProductHttpClient2>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
