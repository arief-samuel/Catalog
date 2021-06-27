using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
            await Task.FromResult(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);

            await Task.CompletedTask;

        }

        public async Task<Item> GetItemAsync(Guid Id)
        {
            var item = items.Where(x => x.Id == Id).SingleOrDefault();
            return await Task.FromResult(item);
        }
        public async Task<IEnumerable<Item>> GetItemAsync()
        {
            return await Task.FromResult(items);
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;

            await Task.CompletedTask;
        }
    }
}