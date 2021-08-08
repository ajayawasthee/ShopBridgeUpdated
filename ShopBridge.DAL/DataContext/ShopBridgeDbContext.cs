using Microsoft.EntityFrameworkCore;
using ShopBridge.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.DAL.DataContext
{
    public class ShopBridgeDbContext:DbContext
    { 
        public ShopBridgeDbContext(DbContextOptions<ShopBridgeDbContext> options)
          : base(options)
        {

        }
        public DbSet<Inventory> Inventory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>().ToTable("TB_Inventory");
            
        }
    }
}
