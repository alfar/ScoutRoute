using MongoDB.Bson.Serialization;
using Scalar.AspNetCore;
using ScoutRoute.ApiService.JsonConverters;
using ScoutRoute.Payments;
using ScoutRoute.Payments.Extensions;
using ScoutRoute.Shared.Extensions;
using ScoutRoute.Shared.ValueTypes.MoneyAmounts;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddPaymentServices();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<IPaymentsEntryPoint>();
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new MoneyJsonConverter());
});


builder.AddMongoDBClient("scoutroute", null, config =>
{

});

builder.AddRedisClient("cache");

var app = builder.Build();

app.UseScoutRouteDefaults();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.MapPaymentEndpoints();

app.MapDefaultEndpoints();

app.Run();
