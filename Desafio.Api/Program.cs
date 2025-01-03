using Desafio.Api.Data;
using Desafio.Api.EndPoints;
using Desafio.Api.Handler;
using Desafio.Api.Services.ReceitaWS;
using Desafio.Core.Handler;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cnnStr = builder.
    Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x => x.UseMySql(cnnStr,ServerVersion.AutoDetect(cnnStr)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(y => y.FullName));
builder.Services.AddTransient<IReceitaWS, ReceitaWS>();
builder.Services.AddTransient<IContaHandler, ContaHandler>();
builder.Services.AddTransient<IMovimentoHandler, MovimentoHandler>();

var app = builder.Build();

app.MapEndPoints();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseFileServer();
}

app.UseHttpsRedirection();

app.Run();
