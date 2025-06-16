var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var postgres = builder.AddPostgres("scoutroutedb").WithPgAdmin().WithLifetime(ContainerLifetime.Persistent).WithDataVolume("scoutroutepg");
var scoutdb = postgres.AddDatabase("scoutroutepg", "scoutroute");

var mongo = builder.AddMongoDB("mongo").WithMongoExpress().WithLifetime(ContainerLifetime.Persistent);

var mongodb = mongo.AddDatabase("scoutroute");

var apiService = builder.AddProject<Projects.ScoutRoute_ApiService>("apiservice")
    .WithReference(mongodb)
    .WaitFor(mongodb)
    .WithReference(scoutdb)
    .WaitFor(scoutdb)
    .WithReference(cache);

builder.Build().Run();
