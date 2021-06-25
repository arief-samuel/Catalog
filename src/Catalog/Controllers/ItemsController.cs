using System.Linq;
using System;
using System.Collections.Generic;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Catalog.Dtos;

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
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItem().Select(item => item.AsDto());
            return items;
        }
        [HttpGet("{Id}")]
        public ActionResult<ItemDto> GetItem(Guid Id)
        {
            var existingItem = repository.GetItem(Id);
            if (existingItem is null)
                return NotFound();

            return repository.GetItem(Id).AsDto();
        }
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.Now
            };
            repository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new { Id = item.Id }, item);
        }
        [HttpPut("{Id}")]
        public ActionResult UpdateItem(Guid Id, UpdateItemDto itemDto)
        {
            var existingItem = repository.GetItem(Id);
            if (existingItem is null)
                return NotFound();

            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            repository.UpdateItem(updatedItem);
            return NoContent();


        }
        [HttpDelete("{Id}")]
        public ActionResult DeleteItem(Guid Id)
        {
            var existingItem = repository.GetItem(Id);
            if (existingItem is null)
                return NotFound();

            repository.DeleteItem(Id);

            return NoContent();
        }
    }
}
