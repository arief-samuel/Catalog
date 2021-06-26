using System;
using System.Collections.Generic;
using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemRepository : IItemRepository
    {
        private readonly IMongoCollection<Item> itemsCollections;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        //public IMongoCollection<Item> itemsCollections { get; } #readonly property
        // private readonly IMongoCollection<Item> itemsCollections; #readonly field
        // private IMongoCollection<Item> itemsCollections; #field
        // public IMongoCollection<Item> itemsCollections { get; private set; } #property
        // IMongoCollection<Item> itemsCollections = database.GetCollection<Item>("items");  # local
        // IMongoCollection<Item> itemsCollections #parameter

        public MongoDbItemRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase("Catalog");
            itemsCollections = database.GetCollection<Item>("items");
        }


        public void CreateItem(Item item)
        {
            itemsCollections.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            itemsCollections.DeleteOne(filter);
        }

        public Item GetItem(Guid Id)
        {
            var filter = filterBuilder.Eq(item => item.Id, Id);
            return itemsCollections.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItem()
        {
            return itemsCollections.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            itemsCollections.ReplaceOne(filter, item);
        }
    }
}