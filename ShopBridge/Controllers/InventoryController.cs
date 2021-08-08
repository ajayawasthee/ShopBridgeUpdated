using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.DAL;
using ShopBridge.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopBridge.Controllers
{
    [Route("api/Inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository inventoryReporsitory;

        public InventoryController(IInventoryRepository _inventoryReporsitory)
        {
            inventoryReporsitory = _inventoryReporsitory;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await inventoryReporsitory.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await inventoryReporsitory.GetByID(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Inventory item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            await inventoryReporsitory.Add(item);
            //return CreatedAtRoute("GetInventory", new { Controller = "Inventory", id = item.ID }, item);
            return Ok( item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Inventory item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var InventoryObj = await inventoryReporsitory.GetByID(id);
            if (InventoryObj == null)
            {
                return NotFound();
            }
            await inventoryReporsitory.EditInventory(item);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contactObj = await inventoryReporsitory.GetByID(id);
            if (contactObj == null)
            {
                return NotFound();
            }
            await inventoryReporsitory.DeleteInventory(id);
            return Ok();
        }
    }
}
