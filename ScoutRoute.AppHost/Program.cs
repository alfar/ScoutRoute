var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
//var elasticsearch = builder.AddElasticsearch("elasticsearch");

var mongo = builder.AddMongoDB("mongo").WithMongoExpress().WithLifetime(ContainerLifetime.Persistent);

var mongodb = mongo.AddDatabase("scoutroute");

var eventstore = builder.AddEventStore("scoutevents");

var apiService = builder.AddProject<Projects.ScoutRoute_ApiService>("apiservice")
    .WithReference(mongodb)
    .WaitFor(mongodb)
    .WithReference(eventstore)
    .WaitFor(eventstore)
//    .WithReference(elasticsearch)
    .WithReference(cache);

/*builder.AddProject<Projects.ScoutRoute_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);
*/

builder.Build().Run();
