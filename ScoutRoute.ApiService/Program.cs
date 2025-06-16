using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using ScoutRoute.ApiService.JsonConverters;
using ScoutRoute.Payments.Extensions;
using ScoutRoute.Routes.Endpoints;
using ScoutRoute.Routes.Extensions;
using ScoutRoute.Shared.Extensions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        options =>
        {
            options.Authority = "https://app-relwla431iy5.frontegg.com";
            options.Audience = "b5a7fe2c-20c5-407e-8773-b36fd03210d5";

            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });
builder.Services.AddAuthorization();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddPaymentServices();
builder.Services.AddRouteServices();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new MoneyJsonConverter());
});


builder.AddMongoDBClient("scoutroute", null, config =>
{

});

builder.AddEventStoreClient("scoutevents");

builder.AddRedisClient("cache");

builder.AddNpgsqlDataSource(connectionName: "scoutroutepg");

var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseScoutRouteDefaults();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.MapPaymentEndpoints();
app.MapRouteEndpoints();

app.MapDefaultEndpoints();

app.Run();
