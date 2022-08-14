using HotChocolateDemo.Data;
using HotChocolateDemo.GraphQL;
using HotChocolateDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Extensions;

public static class ConfigureExtension
{
	public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
	{
		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		builder.Services.AddDbContext<FootballDbContext>(options => {
			options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
		});

		builder.Services
			.AddGraphQLServer()
			// .AddQueryType<FootballQuery>()
			.AddQueryType(q => q.Name("FootballQuery"))
			.AddType<PlayerQueryResolver>()
			.AddType<PositionQueryResolver>()
			.AddMutationType(q => q.Name("FootballMutation"))			
			.AddType<PlayerMutationResolver>();

		builder.Services.AddScoped<IPlayerService, PlayerService>();
		builder.Services.AddScoped<IPlayerService, PlayerService>();
		builder.Services.AddScoped<IPositionService, PositionService>();
		
		return builder;
	}

	public static WebApplication UseServices(this WebApplication app)
	{
		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.MapGraphQL("/football/graphql");
		
		return app;
	}
}
