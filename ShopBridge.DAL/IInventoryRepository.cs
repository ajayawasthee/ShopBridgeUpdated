using ShopBridge.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridge.DAL
{
    public interface IInventoryRepository
    {
        Task Add(Inventory inve);
        Task<IList<Inventory>> GetAll();
        Task<Inventory> GetByID(int id);
        Task EditInventory(Inventory inve);
        Task DeleteInventory(int id);
    }

   
}
