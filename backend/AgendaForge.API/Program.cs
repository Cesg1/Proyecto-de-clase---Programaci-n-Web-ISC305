using DotNetEnv;
using AgendaForge.Infrastructure;
using AgendaForge.Application;
using Scalar.AspNetCore; // 1. Importamos Scalar

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresConnection") 
                       ?? throw new InvalidOperationException("Falta la cadena de conexión.");

builder.Services.AddApplication();
// Llamamos al método que inyecta los repositorios y la base de datos desde Infrastructure
builder.Services.AddInfrastructure(connectionString);
builder.Services.AddControllers();

// 2. Generación nativa de OpenAPI (viene por defecto en .NET 10)
builder.Services.AddOpenApi(); 

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // 3. Genera el documento JSON con el contrato de la API
    app.MapOpenApi();
    
    // 4. Levanta la interfaz gráfica moderna consumiendo ese JSON
    app.MapScalarApiReference(); 
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();

