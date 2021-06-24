using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private InMemItemRepository items;
        public ItemsController(ILogger<ItemsController> logger)
        {
            _logger = logger;
            items = new InMemItemRepository();
        }
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            return items.GetItem();
        }
        [HttpGet("{Id}")]
        public Item GetItem(Guid Id)
        {
            return items.GetItem(Id);
        }
    }
}
