using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IItemRepository
    {
        Task<Item> GetItemAsync(Guid Id);
        Task<IEnumerable<Item>> GetItemAsync();
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(Guid id);
    }
}