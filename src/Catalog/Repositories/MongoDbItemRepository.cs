using System;
using System.Collections.Generic;
using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Catalog.Repositories
{
    public class MongoDbItemRepository : IItemRepository
    {
        private readonly IMongoCollection<Item> itemsCollections;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;
        #region auto generat field or property
        //public IMongoCollection<Item> itemsCollections { get; } #readonly property
        // private readonly IMongoCollection<Item> itemsCollections; #readonly field
        // private IMongoCollection<Item> itemsCollections; #field
        // public IMongoCollection<Item> itemsCollections { get; private set; } #property
        // IMongoCollection<Item> itemsCollections = database.GetCollection<Item>("items");  # local
        // IMongoCollection<Item> itemsCollections #parameter
        #endregion
        public MongoDbItemRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase("Catalog");
            itemsCollections = database.GetCollection<Item>("items");
        }


        public async Task CreateItemAsync(Item item)
        {
            await itemsCollections.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await itemsCollections.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid Id)
        {
            var filter = filterBuilder.Eq(item => item.Id, Id);
            return await itemsCollections.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemAsync()
        {
            return await itemsCollections.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            await itemsCollections.ReplaceOneAsync(filter, item);
        }
    }
}