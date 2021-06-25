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
        public ItemDto GetItem(Guid Id)
        {
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
    }
}
