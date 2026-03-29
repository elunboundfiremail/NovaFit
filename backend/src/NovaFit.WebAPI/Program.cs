using Microsoft.EntityFrameworkCore;
using NovaFit.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar DbContext con PostgreSQL
builder.Services.AddDbContext<NovaFitDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("NovaFitDb")));

// Agregar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS para desarrollo
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configurar pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
