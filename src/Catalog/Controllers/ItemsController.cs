using System.Linq;
using System;
using System.Collections.Generic;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Catalog.Dtos;
using System.Threading.Tasks;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger;
        private IItemRepository repository;
        public ItemsController(ILogger<ItemsController> logger, IItemRepository repository)
        {
            _logger = logger;
            this.repository = repository;
        }
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemAsync())
                        .Select(item => item.AsDto());
            _logger.LogInformation($"{DateTime.Now} Retrived : {items.Count().ToString()}");

            return items;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid Id)
        {
            var existingItem = await repository.GetItemAsync(Id);
            if (existingItem is null)
                return NotFound();

            return existingItem.AsDto();
        }
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.Now
            };
            await repository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItemAsync), new { Id = item.Id }, item.AsDto());
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid Id, UpdateItemDto itemDto)
        {
            var existingItem = await repository.GetItemAsync(Id);
            if (existingItem is null)
                return NotFound();

            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            await repository.UpdateItemAsync(updatedItem);
            return NoContent();


        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid Id)
        {
            var existingItem = await repository.GetItemAsync(Id);
            if (existingItem is null)
                return NotFound();

            await repository.DeleteItemAsync(Id);

            return NoContent();
        }
    }
}
