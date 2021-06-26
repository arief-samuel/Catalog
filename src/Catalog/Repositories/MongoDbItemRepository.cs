using System;
using System.Collections.Generic;
using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemRepository : IItemRepository
    {
        private readonly IMongoCollection<Item> itemsCollections;

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
            throw new NotImplementedException();
        }

        public Item GetItem(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItem()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}