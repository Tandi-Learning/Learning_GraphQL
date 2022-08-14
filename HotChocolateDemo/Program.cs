using HotChocolateDemo.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices();

var app = builder.Build();

app.UseServices();

app.Run();
