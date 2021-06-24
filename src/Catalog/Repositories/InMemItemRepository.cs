using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Entities;

namespace Catalog.Repositories
{

    public class InMemItemRepository : IItemRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 10, CreatedDate = DateTimeOffset.Now },
            new Item { Id = Guid.NewGuid(), Name = "Sword", Price = 30, CreatedDate = DateTimeOffset.Now },
            new Item { Id = Guid.NewGuid(), Name = "Medicine", Price = 5, CreatedDate = DateTimeOffset.Now },
            new Item { Id = Guid.NewGuid(), Name = "Guard", Price = 8, CreatedDate = DateTimeOffset.Now },
            new Item { Id = Guid.NewGuid(), Name = "Knife", Price = 4, CreatedDate = DateTimeOffset.Now },
        };
        public Item GetItem(Guid Id)
        {
            return items.Where(x => x.Id == Id).SingleOrDefault();
        }
        public IEnumerable<Item> GetItem()
        {
            return items;
        }
    }
}