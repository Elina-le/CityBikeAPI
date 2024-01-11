using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CityBikeAPI.Models;
using CityBikeAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = String.Empty;

if (builder.Environment.IsDevelopment())
{
    connection = builder.Configuration.GetConnectionString("CityBikeDbConnection");
}
else
{
    string? keyVaultUri = builder.Configuration["AzureKeyVaultURI"];
    
    if (keyVaultUri != null)
    {
        var keyVaultEndpoint = new Uri(keyVaultUri);
        var secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());
        KeyVaultSecret kvs = secretClient.GetSecret("CityBikeConnectionStringAzure");
        connection = kvs.Value;
    }
}

builder.Services.AddDbContext<CityBikeDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<JourneyService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy",
    builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .WithExposedHeaders("X-Total-Count")
    .WithExposedHeaders("X-Total-Pages")
    .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
