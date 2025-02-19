using ECommerce.API.Products.Db;
using ECommerce.API.Products.Profile;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using AutoMapper;
using ECommerce.API.Products.Interfaces;
using ECommerce.API.Products.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ProductsDbContext>(options =>
{
    options.UseInMemoryDatabase("Products");
});
builder.Services.AddScoped<IProductsProvider, ProductsProvider>();

builder.Services.AddAutoMapper(typeof(ProductProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Products API")
            .WithTheme(ScalarTheme.Mars)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();