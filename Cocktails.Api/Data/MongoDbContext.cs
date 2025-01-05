using System;
using Cocktails.Api.Dtos;
using MongoDB.Driver;

namespace Cocktails.Api.Data;

public class MongoDbContext
{

private readonly IMongoDatabase _database;

  public MongoDbContext() 
  {
    var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") 
    ?? throw new ArgumentNullException("Connection string not found in mongodb context");
    var databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");

    var client = new MongoClient(connectionString);
    _database = client.GetDatabase(databaseName);

  }

  public IMongoCollection<DrinkDto> Drinks => _database.GetCollection<DrinkDto>("Drinks");

  public IMongoCollection<ProfileDto> Profiles => _database.GetCollection<ProfileDto>("Profiles");
}