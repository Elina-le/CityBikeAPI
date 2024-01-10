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

var keyVaultEndpoint = new Uri(builder.Configuration["AzureKeyVaultURI"]);
var secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());

KeyVaultSecret kvs = secretClient.GetSecret("CityBikeConnectionStringAzure");
builder.Services.AddDbContext<CityBikeDbContext>(options => options.UseSqlServer(kvs.Value));


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
