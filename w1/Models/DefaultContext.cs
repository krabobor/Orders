using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace w1.Models
{
    public class DefaultContext : DbContext
    {
        public DefaultContext()
            : base("DefaultConnection") 
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Nomenclature> Goods { get; set; }
        public DbSet<Sales> Sales { get; set; }

    }
}