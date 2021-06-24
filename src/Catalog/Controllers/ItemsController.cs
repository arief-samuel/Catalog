using System;
using System.Collections.Generic;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public IEnumerable<Item> GetItems()
        {
            return repository.GetItem();
        }
        [HttpGet("{Id}")]
        public Item GetItem(Guid Id)
        {
            return repository.GetItem(Id);
        }
    }
}
