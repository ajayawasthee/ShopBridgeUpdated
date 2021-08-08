using Microsoft.EntityFrameworkCore;
using ShopBridge.DAL.DataContext;
using ShopBridge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.DAL
{
public  class InventoryRepository : IInventoryRepository
    {
        private readonly ShopBridgeDbContext context ;
        public InventoryRepository(ShopBridgeDbContext _context)
        {
            context = _context;
        }
        public async Task Add(Inventory inve)
        {
            await context.Inventory.AddAsync(inve);
            await context.SaveChangesAsync();
            //return inve.ID;
        }

        public async Task DeleteInventory(int id)
        {
            var item = await context.Inventory.FindAsync(id);
            if (item != null)
                context.Inventory.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task EditInventory(Inventory itemToUpdate)
        {
            var item = context.Inventory.Where(a => a.ID == itemToUpdate.ID).FirstOrDefault();
            if (item != null)
            {
                item.Name = itemToUpdate.Name;
                item.Description = itemToUpdate.Description;
                item.Price = itemToUpdate.Price;
            }
            context.Update(item);
            await context.SaveChangesAsync();
            // itemToUpdate.ID = itemToUpdate.ID;
            //if (itemToUpdate != null)
            //{
            //    itemToUpdate.Name = itemToUpdate.Name;
            //    itemToUpdate.Description = itemToUpdate.Description;
            //    itemToUpdate.Price = itemToUpdate.Price;
            //}
            //context.Inventory.Update(itemToUpdate);
            //context.Entry(itemToUpdate).State = EntityState.Modified;
            //await context.SaveChangesAsync();


            //var entity = new
            //{
            //    Name = itemToUpdate.Name,
            //    Description = itemToUpdate.Description,
            //    Price = itemToUpdate.Price
            //};
            ////context.Entry(itemToUpdate).State = EntityState.Detached;

            //this.context.Entry(itemToUpdate).CurrentValues.SetValues(entity);
            ////context.Inventory.Update(entity);
            //await context.SaveChangesAsync();
        }

        public async Task<IList<Inventory>> GetAll()
        {
           return await context.Inventory.ToListAsync();
        }

        public async Task<Inventory> GetByID(int id)
        {
            return await context.Inventory.FindAsync(id);
        }
    }
}
