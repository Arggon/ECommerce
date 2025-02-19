using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Services;
using Polly;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();

builder.Services.AddHttpClient("OrdersService", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Services:Orders"]!);
});

builder.Services.AddHttpClient("ProductsService", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Services:Products"]!);
}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

builder.Services.AddHttpClient("CustomersService", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Services:Customers"]!);
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => 
    {
        options
            .WithTitle("Search API")
            .WithTheme(ScalarTheme.Mars)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();