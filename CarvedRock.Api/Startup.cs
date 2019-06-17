using CarvedRock.Api.Data;
using CarvedRock.Api.GraphQL;
using CarvedRock.Api.Repositories;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarvedRock.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<CarvedRockDbContext>(options =>
            {
                options.UseSqlite("Data Source=app.db");
            });
                // options.UseSqlServer(_config["ConnectionStrings:CarvedRock"]));
            services.AddScoped<ProductRepository>();
            services.AddScoped<ProductReviewRepository>();

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<CarvedRockSchema>();

            services.AddGraphQL(o =>
            {
                o.ExposeExceptions = false;
                o.ExposeExceptions = true;
            })
            .AddWebSockets()
            .AddGraphTypes(ServiceLifetime.Scoped);
        }

        public void Configure(IApplicationBuilder app, CarvedRockDbContext dbContext)
        {
            var graphqlEndpoint = "/graphql";

            app.UseWebSockets();
            app.UseGraphQLWebSockets<CarvedRockSchema>(graphqlEndpoint);
            app.UseGraphQL<CarvedRockSchema>(graphqlEndpoint);
            app.UseGraphiQLServer(new GraphiQLOptions
            {
                GraphiQLPath = "/graphiql",
                GraphQLEndPoint = graphqlEndpoint
            });
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions {
                Path = "/playground",
                GraphQLEndPoint = graphqlEndpoint
            });
            app.UseGraphQLVoyager(new GraphQLVoyagerOptions()
            {
                Path = "/voyager",
                GraphQLEndPoint = graphqlEndpoint
            });
            dbContext.Seed();
        }
    }
}