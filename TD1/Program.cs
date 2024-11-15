using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TD1.Models.DataManager;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure DbContext with PostgreSQL
builder.Services.AddDbContext<ProduitDbContext>(options =>
    options.UseNpgsql("Server=localhost;port=5432;Database=TD1; uid=postgres; password=postgres; "));

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://localhost:7091") // Remplace par les domaines autorisés
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Permet les requêtes avec authentification (si nécessaire)
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IDataRepository<Marque>, MarqueManager>();
builder.Services.AddScoped<IDataRepository<TypeProduit>, TypeproduitManager>();
builder.Services.AddScoped<ProduitManager>();
builder.Services.AddControllers()
          .AddJsonOptions(options =>
          {
              options.JsonSerializerOptions.IgnoreNullValues = true;
              options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
          });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS globally or for specific endpoints
app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
