using Kognito.WebApi.Configuration;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

builder.Services.AddSwaggerConfiguration();

builder.Services.AddApiConfiguration(configuration);

builder.Services.AddIdentityConfiguration(configuration);

builder.Services.RegisterServices();

builder.Services.AddMediatR(typeof(Program));

var app = builder.Build();

app.UseApiConfiguration();

app.Run();