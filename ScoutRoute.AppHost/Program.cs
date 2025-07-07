using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
var elasticsearch = builder.AddElasticsearch("elasticsearch");

var apiService = builder.AddProject<Projects.ScoutRoute_ApiService>("apiservice")
    .WithReference(elasticsearch)
    .WithReference(cache);

builder.AddProject<Projects.ScoutRoute_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddNpmApp("frontend", "../ScoutRoute.Frontend/scout-route", "dev")
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints();

builder.Build().Run();
