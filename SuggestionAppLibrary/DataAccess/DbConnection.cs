using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace SuggestionAppLibrary.DataAccess;

public class DbConnection : IDbConnection
{
   private readonly IConfiguration _config;
   private readonly IMongoDatabase _db;
   private string _connectionId = "MongoDB";

   public string DbName { get; }
   public string CategoryCollectionName => "categories";
   public string StatusCollectionName => "statuses";
   public string UserCollectionName => "users";
   public string SuggestionCollectionName => "suggestions";

   public MongoClient Client { get; }
   public IMongoCollection<CategoryModel> CategoryCollection { get; }
   public IMongoCollection<StatusModel> StatusCollection { get; }
   public IMongoCollection<UserModel> UserCollection { get; }
   public IMongoCollection<SuggestionModel> SuggestionCollection { get; }


   public DbConnection(IConfiguration config)
   {
      _config = config;
      Client = new MongoClient(_config.GetConnectionString(_connectionId));
      DbName = _config["DatabaseName"];
      _db = Client.GetDatabase(DbName);

      CategoryCollection = _db.GetCollection<CategoryModel>(CategoryCollectionName);
      StatusCollection = _db.GetCollection<StatusModel> (StatusCollectionName);
      UserCollection = _db.GetCollection<UserModel>(UserCollectionName);
      SuggestionCollection = _db.GetCollection<SuggestionModel>(SuggestionCollectionName);
   }
}
