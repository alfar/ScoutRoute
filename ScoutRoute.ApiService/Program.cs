using Scalar.AspNetCore;
using ScoutRoute.ApiService.JsonConverters;
using ScoutRoute.Payments;
using ScoutRoute.Payments.Extensions;
using ScoutRoute.Routes.Endpoints;
using ScoutRoute.Routes.Extensions;
using ScoutRoute.Routes.Repository.ReadModels;
using ScoutRoute.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddPaymentServices();
builder.Services.AddRouteServices();

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

builder.AddEventStoreClient("scoutevents");

builder.AddRedisClient("cache");

var app = builder.Build();

app.UseCors();

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

await app.Services.GetService<ProjectSubscriber>()!.Subscribe();

app.Run();
