using Microsoft.EntityFrameworkCore;
using SaaS.Application.Interfaces;
using SaaS.Application;
using SaaS.Infrastructure.Persistence;
using SaaS.Infrastructure.Services;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using MediatR;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços do Swagger para documentar a API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona os Controllers
builder.Services.AddControllers();

// Adiciona o MediatR e os handlers (comandos e queries)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// Configuração dos serviços de Multi-Tenancy
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantService, TenantService>();

// Adiciona o DbContext usando o MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions => mySqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

// Registra o DbContext na interface
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<AppDbContext>());


var app = builder.Build();

// Configura o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    // Usa o Swagger em ambiente de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adiciona os endpoints dos Controllers ao pipeline
app.MapControllers();

app.Run();
